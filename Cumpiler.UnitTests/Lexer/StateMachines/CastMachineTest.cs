
using Cumpiler.Lexer.SateMachines;

namespace Cumpiler.UnitTests.Lexer.StateMachines {
    public class CastMachineTest {

        [Fact]
        public void Cast_ShouldAccept() {
            var machine = new CastMachine();
            machine.Process("(asd0123_4)");

            Assert.True(machine.IsInFinalState());
        }

        [Fact]
        public void Cast_ShouldNotAccept_WithMissingParanR() {
            var machine = new CastMachine();
            machine.Process("(asd0123_4");

            Assert.False(machine.IsInFinalState());
        }

        [Fact]
        public void Cast_ShouldNotAccept_WithMissingParanL() {
            var machine = new CastMachine();
            machine.Process("asd0123_4)");

            Assert.False(machine.IsInFinalState());
        }
    }
}
