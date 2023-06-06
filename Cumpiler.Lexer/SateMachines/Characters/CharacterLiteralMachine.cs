using Cumpiler.Lexer.Common.Abstract;
using Cumpiler.Lexer.Common.Tokens;

namespace Cumpiler.Lexer.SateMachines.Characters {
    internal class CharacterLiteralMachine : StateMachine {


        public override TokenType GetTokenType() {
            return TokenType.CHAR;
        }

        protected override void InitStateTable() {
            _start = new State();
            var innerState = AddState(new State());
            var escapedState = AddState(new State());
            var afterCharState = AddState(new State());
            var endState = AddState(new State(true));

            _start.AddTransition(innerState, '\'');

            innerState
                .AddTransitionRange(afterCharState, ' ', '&')
                .AddTransitionRange(afterCharState, '(', '[')
                .AddTransitionRange(afterCharState, ']', '~')
                .AddTransition(escapedState, '\\')
                .AddTransition(endState, '\'');

            escapedState
                .AddTransition(afterCharState, 't', 'b', 'n', 'r', 'f', '\'', '"', '\\');

            afterCharState
                .AddTransition(endState, '\'');
        }
    }
}
