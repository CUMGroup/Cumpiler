using Cumpiler.Lexer.SateMachines.Comments;

namespace Cumpiler.UnitTests.Lexer.StateMachines {
    public class LineCommentMachineTest {

        [Theory]
        [InlineData("//\n")]
        [InlineData("//asdwd$%wölkfnsfö\n")]
        [InlineData("//   \n")]
        [InlineData("// asdw dw\n")]
        public void Machine_ShouldAccept_Comment(string input) {
            var machine = new LineCommentMachine();

            machine.Process(input);

            Assert.True(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldNotAccept_OneSlash() {
            var machine = new LineCommentMachine();

            machine.Process("/ dawdw\n");

            Assert.False(machine.IsInFinalState());
        }
    }
}
