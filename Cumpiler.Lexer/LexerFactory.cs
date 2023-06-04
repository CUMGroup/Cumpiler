
using Cumpiler.Lexer.Common.Interfaces;

namespace Cumpiler.Lexer {
    public static class LexerFactory {

        public static ILexer CreateLexer(string input) {
            var lex = new Lexer();
            lex.Init(input);
            return lex;
        }
    }
}
