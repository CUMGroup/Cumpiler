using Cumpiler.Lexer.Common.Interfaces;

namespace Cumpiler.Syntax {
    internal partial class Parser {

        private readonly ILexer _lexer;

        public Parser(ILexer input) {
            _lexer = input;
        }

        
    }
}
