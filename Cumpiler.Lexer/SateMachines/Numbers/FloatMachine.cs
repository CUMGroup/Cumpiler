using Cumpiler.Lexer.Common.Abstract;
using Cumpiler.Lexer.Common.Tokens;

namespace Cumpiler.Lexer.SateMachines.Numbers {
    internal class FloatMachine : StateMachine {

        public override TokenType GetTokenType() {
            return TokenType.FLOAT;
        }

        protected override void InitStateTable() {
            /* ACCEPTS:
             * 123_123_332.232_321e2_2f
             * 2f
             * .23
             * .23f
             * 2e-2
             * 2e+2
             */

            _start = AddState(new State());
            var beforeCommaNumberState = AddState(new State(true));
            var beforeCommaUnderscoreState = AddState(new State());

            var commaState = AddState(new State());

            var afterCommaNumberState = AddState(new State(true));
            var afterCommaUnderscoreState = AddState(new State());

            var exponentState = AddState(new State());
            var exponentSignState = AddState(new State());
            var exponentNumberState = AddState(new State(true));
            var exponentUnderscoreState = AddState(new State());

            var dPostFixState = AddState(new State(true));

            _start
                .AddTransitionRange(beforeCommaNumberState, '0', '9')
                .AddTransition(commaState, '.');

            beforeCommaNumberState
                .AddTransitionRange(beforeCommaNumberState, '0', '9')
                .AddTransition(beforeCommaUnderscoreState, '_')
                .AddTransition(commaState, '.')
                .AddTransition(exponentState, 'e', 'E')
                .AddTransition(dPostFixState, 'f', 'F');

            beforeCommaUnderscoreState
                .AddTransitionRange(beforeCommaNumberState, '0', '9');

            commaState
                .AddTransitionRange(afterCommaNumberState, '0', '9');

            afterCommaNumberState
                .AddTransitionRange(afterCommaNumberState, '0', '9')
                .AddTransition(afterCommaUnderscoreState, '_')
                .AddTransition(exponentState, 'e', 'E')
                .AddTransition(dPostFixState, 'f', 'F');

            afterCommaUnderscoreState
                .AddTransitionRange(afterCommaNumberState, '0', '9');

            exponentState
                .AddTransition(exponentSignState, '-', '+')
                .AddTransitionRange(exponentNumberState, '0', '9');

            exponentSignState
                .AddTransitionRange(exponentNumberState, '0', '9');

            exponentNumberState
                .AddTransitionRange(exponentNumberState, '0', '9')
                .AddTransition(exponentUnderscoreState, '_')
                .AddTransition(dPostFixState, 'f', 'F');

            exponentUnderscoreState
                .AddTransitionRange(exponentNumberState, '0', '9');

        }
    }
}
