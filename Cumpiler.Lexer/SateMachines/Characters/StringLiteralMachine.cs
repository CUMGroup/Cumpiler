using Cumpiler.Lexer.Common.Abstract;
using Cumpiler.Lexer.Common.Tokens;

namespace Cumpiler.Lexer.SateMachines.Characters {
    internal class StringLiteralMachine : StateMachine {

        public override TokenType GetTokenType() {
            return TokenType.STRING;            
        }

        protected override void InitStateTable() {
            _start = new State("start", false);
            var innerState = AddState(new State("inner", false));
            var escapedState = AddState(new State("escaped", false));
            var endState = AddState(new State("end", true));

            _start.AddTransition(innerState, '\"');

            innerState
                .AddTransitionRange(innerState, ' ', '!')
                .AddTransitionRange(innerState, '#', '[')
                .AddTransitionRange(innerState, ']', '~')
                .AddTransition(escapedState, '\\')
                .AddTransition(endState, '\"');

            escapedState
                .AddTransition(innerState, 't', 'b', 'n', 'r', 'f', '\'', '"', '\\');
        }
    }
}
