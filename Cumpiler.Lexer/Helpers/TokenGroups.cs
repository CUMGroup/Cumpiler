using Cumpiler.Lexer.Common.Tokens;

namespace Cumpiler.Lexer.Helpers {
    public static class TokenGroups {

        public static bool IsNumber(this Token token) {
            return token.Type is TokenType.INTEGER or TokenType.FLOAT or TokenType.DOUBLE;
        }

        public static bool IsString(this Token token) {
            return token.Type is TokenType.STRING or TokenType.CHAR;
        }

        public static bool IsBool(this Token token) {
            return token.Type is TokenType.TRUE or TokenType.FALSE;
        }

        public static bool IsSkipable(this Token token) {
            return token.Type is TokenType.WHITESPACE or TokenType.LINECOMMENT or TokenType.MULTILINECOMMENT;
        }
    }
}
