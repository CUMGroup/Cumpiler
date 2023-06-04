using System.Runtime.Serialization;

namespace Cumpiler.Lexer.Common.Exceptions {
    public class CompilerException : Exception {
        public CompilerException() {
        }

        public CompilerException(string? message) : base(message) {
        }

        public CompilerException(string? message, Exception? innerException) : base(message, innerException) {
        }

        protected CompilerException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
