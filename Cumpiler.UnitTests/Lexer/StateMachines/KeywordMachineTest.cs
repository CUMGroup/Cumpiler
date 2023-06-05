using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Lexer.SateMachines;

namespace Cumpiler.UnitTests.Lexer.StateMachines {
    public class KeywordMachineTest {

        [Fact]
        public void Keywords_Should_Accept() {
            var machine = new KeywordMachine("ABCDEFG", TokenType.IDENTIFIER);
            machine.Process("ABCDEFG");

            Assert.True(machine.IsInFinalState());
        }

        [Theory]
        [InlineData("ABCDEF")]
        [InlineData("ABCDEFGH")]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("AGWDWDS")]
        public void Keywords_ShouldNot_Accept(string input) {
            var machine = new KeywordMachine("ABCDEFG", TokenType.IDENTIFIER);
            machine.Process(input);

            Assert.False(machine.IsInFinalState());
        }
    }
}
