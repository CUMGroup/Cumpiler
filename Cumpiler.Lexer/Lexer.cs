
using Cumpiler.Lexer.Common.Abstract;
using Cumpiler.Lexer.Common.Exceptions;
using Cumpiler.Lexer.Common.Info;
using Cumpiler.Lexer.Common.Interfaces;
using Cumpiler.Lexer.Common.Text;
using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Lexer.Diagnostics;
using Cumpiler.Lexer.Helpers;
using Cumpiler.Lexer.SateMachines;
using System.Diagnostics.CodeAnalysis;

namespace Cumpiler.Lexer
{

    internal partial class Lexer : ILexer, INdaLexing {

        private readonly List<MachineInfo> _machines;
        private MultilineInputReader? _input;
        private Token? _currentToken;

        public Lexer() {
            _machines = new List<MachineInfo>();
            AddLexerMachines();
        }

        #region Lexer

        public Token LookAhead => _currentToken ?? Token.EOF();

        public bool Accept(TokenType type) {
            if (type == LookAhead.Type) {
                Advance();
                return true;
            }
            return false;
        }

        public Token Advance() {
            Token oldToken = LookAhead;
            do {
                _currentToken = NextWord();
            } while (_currentToken.IsSkipable());
            return oldToken;
        }

        public Token Expect(TokenType type) {
            if(type != LookAhead.Type) {
                ThrowCompilerException($"Unexpected Token {_currentToken}", type.ToString());
            }
            return Advance();
        }

        public List<Token> AdvanceTillEOF() {
            var tokens = new List<Token>();
            do {
                tokens.Add(Advance());
            } while (tokens.Last()?.Type is not (null or TokenType.EOF));
            return tokens;
        }

        public void Init(string input) {
            _input = new MultilineInputReader(input);
            DiagnosticsBag.Input = _input;
            _currentToken = null;
            Advance();
        }

        [DoesNotReturn]
        public void ThrowCompilerException(string reason, string? expected) {
            DiagnosticsBag.ThrowCompilerException(reason, expected);
        }

        public CompilerException CreateCompilerException(string reason, string? expected) {
            return DiagnosticsBag.CreateCompilerException(reason, expected, null);
        }

        #endregion

        #region NDA

        public void AddKeywordMachine(string keyword, TokenType type) {
            _machines.Add(new KeywordMachine(keyword, type));
        }

        public void InitMachines(string input) {
            foreach (var machine in _machines) {
                machine.Init(input);
            }
        }

        public Token NextWord() {
            if(_input is null) {
                throw new NullReferenceException("Input Reader is null");
            }
            if(_input.IsEmpty()) {
                return Token.EOF(_input.LinePos, _input.ColumnPos);
            }

            InitMachines(_input.GetRemaining());
            StepMachinesWhileActive();

            var bestMatch = GetBestMatch();

            if (bestMatch is null) {
                ThrowCompilerException($"Unexpected Token {_currentToken?.Type.ToString() ?? ""}", null);
            }


            int firstLine = _input.LinePos;
            int firstCol = _input.ColumnPos;
            string nextWord = _input.AdvanceAndGet(bestMatch.AcceptPos);
            return new Token {
                Position = new TokenPos {
                    FirstLine = firstLine,
                    FirstCol = firstCol,
                    LastLine = _input.LinePos,
                    LastCol = _input.ColumnPos
                },
                Type = bestMatch.Machine.GetTokenType(),
                Value = nextWord
            };
        }

        private void StepMachinesWhileActive() {
            int curPos = 0;
            bool active;
            do {
                active = false;
                foreach (var machine in _machines) {
                    if (machine.Machine.IsFinished()) {
                        continue;
                    }
                    active = true;
                    machine.Machine.Step();

                    if (machine.Machine.IsInFinalState()) {
                        machine.AcceptPos = curPos + 1;
                    }
                }
                ++curPos;
            } while (active);
        }

        private MachineInfo? GetBestMatch() {
            MachineInfo? bestMatch = null;
            foreach (var machine in _machines) {
                if (machine.AcceptPos > (bestMatch?.AcceptPos ?? -1)) {
                    bestMatch = machine;
                }
            }
            return bestMatch;
        }

        public void AddMachine(StateMachine machine) {
            _machines.Add(machine);
        }


        #endregion
    }
}
