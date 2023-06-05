using Cumpiler.Lexer.SateMachines.Characters;

namespace Cumpiler.UnitTests.Lexer.StateMachines {
    public class StringLiteralMachineTest {

        [Fact]
        public void StringLiteral_ShouldAccept() {
            var machine = new StringLiteralMachine();

            machine.Process("\"'Hello' World\"");

            Assert.True(machine.IsInFinalState());
        }

        [Theory]
        [InlineData("\"\"")]
        [InlineData("\" \"")]
        public void StringLiteral_ShouldAccept_Empty(string input) {
            var machine = new StringLiteralMachine();

            machine.Process(input);

            Assert.True(machine.IsInFinalState());
        }

        [Fact]
        public void StringLiteral_ShouldNotAccept_WrongEscapeChars() {
            var machine = new StringLiteralMachine();

            machine.Process("\"\\lHello World\"");

            Assert.False(machine.IsInFinalState());
        }

        [Theory]
        [InlineData("\"\\t\"")]
        [InlineData("\"\\b\"")]
        [InlineData("\"\\n\"")]
        [InlineData("\"\\r\"")]
        [InlineData("\"\\f\"")]
        [InlineData("\"\\\'\"")]
        [InlineData("\"\\\"\"")]
        [InlineData("\"\\\\\"")]
        public void StringLiteral_ShouldAccept_EscapeCharacters(string input) {
            var machine = new StringLiteralMachine();

            machine.Process(input);

            Assert.True(machine.IsInFinalState());
        }
    }
}
