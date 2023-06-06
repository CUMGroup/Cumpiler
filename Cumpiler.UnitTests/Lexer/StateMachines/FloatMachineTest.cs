using Cumpiler.Lexer.SateMachines.Numbers;

namespace Cumpiler.UnitTests.Lexer.StateMachines {
    public class FloatMachineTest {

        [Theory]
        [InlineData("0")]
        [InlineData("1232323")]
        [InlineData("2323_23232_2323")]
        [InlineData("223.2123")]
        [InlineData("2_2.2_2_2")]
        [InlineData("2e4")]
        [InlineData("2e+4")]
        [InlineData("2e-4")]
        [InlineData("2.3e4")]
        [InlineData("2.3e4_4")]
        [InlineData(".3")]
        [InlineData("2f")]
        [InlineData("2.2f")]
        [InlineData(".2F")]
        [InlineData("2.2e+2f")]
        public void Machine_ShouldAccept_ValidDouble(string input) {
            var machine = new FloatMachine();

            machine.Process(input);

            Assert.True(machine.IsInFinalState());
        }

        [Theory]
        [InlineData("2_")]
        [InlineData("2_.2")]
        [InlineData("2.2_")]
        [InlineData("2.2e2_")]
        public void Machine_ShouldNotAccept_UnderscoreAtEnd(string input) {
            var machine = new FloatMachine();

            machine.Process(input);

            Assert.False(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldNotAccept_CommaAtEnd() {
            var machine = new FloatMachine();

            machine.Process("2.");

            Assert.False(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldNotAccept_ExpAtEnd() {
            var machine = new FloatMachine();

            machine.Process("2.2e");

            Assert.False(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldNotAccept_ExpSignAtEnd() {
            var machine = new FloatMachine();

            machine.Process("2.2e+");

            Assert.False(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldNotAccept_DoubleSignExponent() {
            var machine = new FloatMachine();

            machine.Process("2.2e2-2-2");

            Assert.False(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldNotAccept_DoubleComma() {
            var machine = new FloatMachine();

            machine.Process("2.2.2");

            Assert.False(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldNotAccept_Double() {
            var machine = new FloatMachine();

            machine.Process("2d");

            Assert.False(machine.IsInFinalState());
        }
    }
}
