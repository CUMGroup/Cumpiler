
using Cumpiler.Lexer.SateMachines;

namespace Cumpiler.UnitTests.Lexer.StateMachines {
    public class WhitespaceMachineTest {

        [Theory]
        [InlineData(" ")]
        [InlineData("       ")]
        [InlineData(" \n\r\t")]
        public void Machine_ShouldAccept_WhitespaceVariants(string input) {
            var machine = new WhitespaceMachine();

            machine.Process(input);

            Assert.True(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldNotAccept_Text() {
            var machine = new WhitespaceMachine();

            machine.Process(" Hello World ");

            Assert.False(machine.IsInFinalState());
        }
    }
}
