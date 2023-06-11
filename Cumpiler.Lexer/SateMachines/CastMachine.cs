using Cumpiler.Lexer.Common.Abstract;
using Cumpiler.Lexer.Common.Tokens;

namespace Cumpiler.Lexer.SateMachines {
    internal class CastMachine : StateMachine {
        public override TokenType GetTokenType() {
            return TokenType.CAST;
        }

        protected override void InitStateTable() {
            _start = AddState(new State());
            var paranOpen = AddState(new State());
            var endStateIdent = AddState(new State());
            var endState = AddState(new State(true));

            _start.AddTransition(paranOpen, '(');

            paranOpen
                .AddTransition(endStateIdent, '_')
                .AddTransitionRange(endStateIdent, 'a', 'z')
                .AddTransitionRange(endStateIdent, 'A', 'Z');

            endStateIdent
                .AddTransition(endStateIdent, '_')
                .AddTransitionRange(endStateIdent, 'a', 'z')
                .AddTransitionRange(endStateIdent, 'A', 'Z')
                .AddTransitionRange(endStateIdent, '0', '9')
                .AddTransition(endState, ')');
        }
    }
}
