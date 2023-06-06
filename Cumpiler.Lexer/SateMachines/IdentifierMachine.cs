using Cumpiler.Lexer.Common.Abstract;
using Cumpiler.Lexer.Common.Tokens;

namespace Cumpiler.Lexer.SateMachines {
    internal class IdentifierMachine : StateMachine {

        public override TokenType GetTokenType() {
            return TokenType.IDENTIFIER;
        }

        protected override void InitStateTable() {

            _start = AddState(new State());
            var endState = AddState(new State(true));

            _start
                .AddTransition(endState, '_')
                .AddTransitionRange(endState, 'a', 'z')
                .AddTransitionRange(endState, 'A', 'Z');

            endState
                .AddTransition(endState, '_')
                .AddTransitionRange(endState, 'a', 'z')
                .AddTransitionRange(endState, 'A', 'Z')
                .AddTransitionRange(endState, '0', '9');
        }
    }
}
