using Cumpiler.Lexer.Common.Tokens;

namespace Cumpiler.Lexer.Helpers;

public static class TokenOperators {

    public static Func<double, double, double> GetTokenBinaryOperationFunc(this TokenType t) {
        return t switch {
            TokenType.AND => (double a, double b) => a > 0 && b > 0 ? 1 : 0,
            TokenType.BITAND => (double a, double b) => (int)a & (int)b,
            TokenType.BITOR => (double a, double b) => (int)a | (int)b,
            TokenType.BITXOR => (double a, double b) => (int)a ^ (int)b,
            TokenType.DIV => (double a, double b) => a / b,
            TokenType.EQUAL => (double a, double b) => a == b ? 1 : 0,
            TokenType.GREATER => (double a, double b) => a > b ? 1 : 0,
            TokenType.GREATEREQUAL => (double a, double b) => a >= b ? 1 : 0,
            TokenType.LESS => (double a, double b) => a < b ? 1 : 0,
            TokenType.LESSEQUAL => (double a, double b) => a <= b ? 1 : 0,
            TokenType.MINUS => (double a, double b) => a - b,
            TokenType.MOD => (double a, double b) => a % b,
            TokenType.MUL => (double a, double b) => a * b,
            TokenType.NOTEQUAL => (double a, double b) => a != b ? 1 : 0,
            TokenType.OR => (double a, double b) => a > 0 || b > 0 ? 1 : 0,
            TokenType.PLUS => (double a, double b) => a + b,
            TokenType.SHIFTLEFT => (double a, double b) => (int)a << (int)b,
            TokenType.SHIFTRIGHT => (double a, double b) => (int)a >> (int)b,
            _ => throw new ArgumentException(t + " is not a binary operator")
        };
    }

    public static Func<double, double> GetTokenUnaryOperationFunc(this TokenType t) {
        return t switch {
            TokenType.PLUS => (double a) => a,
            TokenType.MINUS => (double a) => -a,
            TokenType.NOT => (double a) => a > 0 ? 0 : 1,
            TokenType.BITCOMPLEMENT => (double a) => ~(int)a,
            _ => throw new ArgumentException(t + " is not a unary operator")
        };
    }
}
