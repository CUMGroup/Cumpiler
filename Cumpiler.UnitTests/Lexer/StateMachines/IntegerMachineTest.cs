using Cumpiler.Lexer.SateMachines.Numbers;

namespace Cumpiler.UnitTests.Lexer.StateMachines {
    public class IntegerMachineTest {

        [Theory]
        [InlineData("0")]
        [InlineData("1")]
        [InlineData("01")]
        [InlineData("0923")]
        [InlineData("90")]
        [InlineData("1234567890")]
        public void Machine_ShouldAccept_DecimalInteger(string input) {
            var machine = new IntegerMachine();

            machine.Process(input);

            Assert.True(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldAccept_DecimalNumber_WithUnderscores() {
            var machine = new IntegerMachine();

            machine.Process("123_234_4512344_2");

            Assert.True(machine.IsInFinalState());
        }

        [Theory]
        [InlineData("")]
        [InlineData("Hello World")]
        public void Machine_ShouldNotAccept_NonNumber(string input) {
            var machine = new IntegerMachine();

            machine.Process(input);

            Assert.False(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldNotAccept_Decimal_WithUnderscoreAtEnd() {
            var machine = new IntegerMachine();

            machine.Process("1234_");

            Assert.False(machine.IsInFinalState());
        }

        [Theory]
        [InlineData("0b0101")]
        [InlineData("0b0")]
        [InlineData("0b11111111111")]
        [InlineData("0b0000000000")]
        public void Machine_ShouldAccept_BinaryNumber(string input) {
            var machine = new IntegerMachine();

            machine.Process(input);

            Assert.True(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldAccept_BinaryNumber_WithUnderscore() {
            var machine = new IntegerMachine();

            machine.Process("0b10_10010_1000");

            Assert.True(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldNotAccept_BinaryNumber_WithUnderscoreAtEnd() {
            var machine = new IntegerMachine();

            machine.Process("0b10_10010_1000_");

            Assert.False(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldNotAccept_NonBinaryNumber() {
            var machine = new IntegerMachine();

            machine.Process("0b120");

            Assert.False(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldNotAccept_NoBinaryNumber() {
            var machine = new IntegerMachine();

            machine.Process("0b");

            Assert.False(machine.IsInFinalState());
        }


        [Theory]
        [InlineData("0x1230")]
        [InlineData("0x000")]
        [InlineData("0x0123456789ABCDEF0")]
        public void Machine_ShouldAccept_HexNumber(string input) {
            var machine = new IntegerMachine();

            machine.Process(input);

            Assert.True(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldNotAccept_NonHexNumber() {
            var machine = new IntegerMachine();

            machine.Process("0x01Z");

            Assert.False(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldNotAccept_NoHexNumber() {
            var machine = new IntegerMachine();

            machine.Process("0x");

            Assert.False(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldAccept_HexNumber_WithUnderscore() {
            var machine = new IntegerMachine();

            machine.Process("0x10_10010_1000");

            Assert.True(machine.IsInFinalState());
        }

        [Fact]
        public void Machine_ShouldNotAccept_HexNumber_WithUnderscoreAtEnd() {
            var machine = new IntegerMachine();

            machine.Process("0x10_10010_1000_");

            Assert.False(machine.IsInFinalState());
        }
    }
}
