using Cumpiler.Lexer.Common.Abstract;
using Cumpiler.Lexer.Common.Tokens;

namespace Cumpiler.Lexer.SateMachines.Numbers {
    internal class IntegerMachine : StateMachine {

        public override TokenType GetTokenType() {
            return TokenType.INTEGER;
        }

        protected override void InitStateTable() {
            /* Accepts:
             * 0
             * 00012345
             * 12343_100_000
             * 
             * 0xABCDEF001023
             * 0xABC_DEF
             * 0b01001001010
             * 0b010_1011
             * 
             * Does not accept:
             * 0x
             * 0b
             * 1234_
             * 0b1234
             * 0x12354Y
             * 1235Y
             */

            _start = AddState(new State("start", false));
            var decimalSystemNumberState = AddState(new State("decimalNumber", true));
            var decimalUnderscoreState = AddState(new State("decimalUnderscore", false));
            var firstZeroState = AddState(new State("firstZero", true));

            var binaryNumberFirstState = AddState(new State("binaryNumberFirst", false));
            var binaryNumberState = AddState(new State("binaryNumber", true));
            var binaryUnderscoreState = AddState(new State("binaryUnderscore", false));

            var hexNumberFirstState = AddState(new State("hexNumberFirst", false));
            var hexNumberState = AddState(new State("hexNumber", true));
            var hexNumberUnderscoreState = AddState(new State("hexNumberUnderscore", false));

            _start
                .AddTransitionRange(decimalSystemNumberState, '1', '9')
                .AddTransition(firstZeroState, '0');

            decimalSystemNumberState
                .AddTransitionRange(decimalSystemNumberState, '0', '9')
                .AddTransition(decimalUnderscoreState, '_');

            decimalUnderscoreState
                .AddTransitionRange(decimalSystemNumberState, '0', '9');

            firstZeroState
                .AddTransitionRange(decimalSystemNumberState, '0', '9')
                .AddTransition(binaryNumberFirstState, 'b', 'B')
                .AddTransition(hexNumberFirstState, 'x', 'X');

            binaryNumberFirstState
                .AddTransition(binaryNumberState, '0', '1');

            binaryNumberState
                .AddTransition(binaryNumberState, '0', '1')
                .AddTransition(binaryUnderscoreState, '_');

            binaryUnderscoreState
                .AddTransition(binaryNumberState, '0', '1');

            hexNumberFirstState
                .AddTransitionRange(hexNumberState, '0', '9')
                .AddTransitionRange(hexNumberState, 'A', 'F')
                .AddTransitionRange(hexNumberState, 'a', 'f');

            hexNumberState
                .AddTransitionRange(hexNumberState, '0', '9')
                .AddTransitionRange(hexNumberState, 'A', 'F')
                .AddTransitionRange(hexNumberState, 'a', 'f')
                .AddTransition(hexNumberUnderscoreState, '_');

            hexNumberUnderscoreState
                .AddTransitionRange(hexNumberState, '0', '9')
                .AddTransitionRange(hexNumberState, 'A', 'F')
                .AddTransitionRange(hexNumberState, 'a', 'f');

        }
    }
}
