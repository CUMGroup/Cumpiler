using Cumpiler.Lexer.SateMachines;

namespace Cumpiler.UnitTests.Lexer.StateMachines {
    public class IdentifierMachineTest {

        [Theory]
        [InlineData("_")]
        [InlineData("asd_d02_2")]
        [InlineData("AdwdASdwXACwcw123")]
        public void Machine_ShouldAccept_IdentifierName(string input) {
            var machine = new IdentifierMachine();

            machine.Process(input);

            Assert.True(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldNotAccept_NumberAsFirst() {
            var machine = new IdentifierMachine();

            machine.Process("0ad_wd");

            Assert.False(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldNotAccept_SpecialChar() {
            var machine = new IdentifierMachine();

            machine.Process("$ad_wd");

            Assert.False(machine.IsInFinalState());
        }
    }
}
