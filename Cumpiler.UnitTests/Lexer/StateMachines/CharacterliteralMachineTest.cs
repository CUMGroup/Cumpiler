using Cumpiler.Lexer.SateMachines.Characters;

namespace Cumpiler.UnitTests.Lexer.StateMachines {
    public class CharacterliteralMachineTest {

        [Fact]
        public void CharacterLiteral_ShouldAccept() {
            var machine = new CharacterLiteralMachine();

            machine.Process("'H'");

            Assert.True(machine.IsInFinalState());
        }

        [Theory]
        [InlineData("''")]
        [InlineData("' '")]
        public void CharacterLiteral_ShouldAccept_Empty(string input) {
            var machine = new CharacterLiteralMachine();

            machine.Process(input);

            Assert.True(machine.IsInFinalState());
        }

        [Fact]
        public void CharacterLiteral_ShouldNotAccept_MultipleChars() {
            var machine = new CharacterLiteralMachine();

            machine.Process("'Hi'");

            Assert.False(machine.IsInFinalState());
        }

        [Fact]
        public void CharacterLiteral_ShouldNotAccept_MultipleEmptys() {
            var machine = new CharacterLiteralMachine();

            machine.Process("'  '");

            Assert.False(machine.IsInFinalState());
        }

        [Theory]
        [InlineData("'\\t'")]
        [InlineData("'\\b'")]
        [InlineData("'\\n'")]
        [InlineData("'\\r'")]
        [InlineData("'\\f'")]
        [InlineData("'\\\''")]
        [InlineData("'\\\"'")]
        [InlineData("'\\\\'")]
        public void CharacterLiteral_ShouldAccept_EscapeCharacters(string input) {
            var machine = new CharacterLiteralMachine();

            machine.Process(input);

            Assert.True(machine.IsInFinalState());
        }
    }
}
