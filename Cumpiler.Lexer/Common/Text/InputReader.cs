using Cumpiler.Lexer.Common.Exceptions;

namespace Cumpiler.Lexer.Common.Text {
    internal class InputReader {

        private readonly string _input;
        private int _position;

        public InputReader(string input) {
            _input = input.Replace("\r\n", "\n");
            _position = 0;
        }

        public char CurrentChar() {
            if(_position < _input.Length) {
                return _input[_position];
            }
            return '\0';
        }

        public char Advance() { 
            if(_position < _input.Length) { 
                return _input[_position++]; 
            }
            return '\0';
        }

        public void Expect(char c) {
            if(CurrentChar() == c) {
                throw new CompilerException($"Unexpected Character at position {_position}: '{CurrentChar()}' expected: '{c}'");
            }
            Advance();
        }
    }
}
