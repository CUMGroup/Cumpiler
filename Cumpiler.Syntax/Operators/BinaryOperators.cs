using Cumpiler.Lexer.Common.Exceptions;
using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Syntax.Symbols;

namespace Cumpiler.Syntax.Operators {
    internal static class BinaryOperators {

        public static TypeSymbol GetTypeFromOperation(TokenType op, TypeSymbol lhs, TypeSymbol rhs) {
            switch(op) {
                case TokenType.AND:
                case TokenType.OR:
                    if (AcceptedType(lhs, rhs, TypeSymbol.Bool))
                        return TypeSymbol.Bool;
                    break;
                case TokenType.BITOR:
                case TokenType.BITXOR:
                case TokenType.BITAND:
                case TokenType.SHIFTLEFT:
                case TokenType.SHIFTRIGHT:
                    if (AcceptedType(lhs, rhs, TypeSymbol.Int))
                        return TypeSymbol.Bool;
                    break;
                case TokenType.PLUS:
                    if(lhs == TypeSymbol.String || rhs == TypeSymbol.String)
                        return TypeSymbol.String;
                    goto case TokenType.MINUS; // Fallthrough
                case TokenType.MINUS:
                    if(AcceptedType(lhs, rhs, TypeSymbol.Int, TypeSymbol.Char))
                        return TypeSymbol.Int;
                    if (AcceptedType(lhs, rhs, TypeSymbol.Double, TypeSymbol.Char))
                        return TypeSymbol.Double;
                    if (AcceptedType(lhs, rhs, TypeSymbol.Float, TypeSymbol.Char))
                        return TypeSymbol.Float;
                    goto case TokenType.DIV; // Fallthrough
                case TokenType.DIV:
                case TokenType.MOD:
                case TokenType.MUL:
                    if (AcceptedType(lhs, rhs, TypeSymbol.Int, TypeSymbol.Double))
                        return TypeSymbol.Double;
                    if (AcceptedType(lhs, rhs, TypeSymbol.Int, TypeSymbol.Float))
                        return TypeSymbol.Float;
                    if (AcceptedType(lhs, rhs, TypeSymbol.Int))
                        return TypeSymbol.Int;
                    break;
                case TokenType.EQUAL:
                case TokenType.NOTEQUAL:
                    if(lhs == rhs)
                        return TypeSymbol.Bool;
                    break;
                case TokenType.GREATER:
                case TokenType.GREATEREQUAL:
                case TokenType.LESS:
                case TokenType.LESSEQUAL:
                    if (AcceptedType(lhs, rhs, TypeSymbol.Int, TypeSymbol.Double))
                        return TypeSymbol.Bool;
                    if (AcceptedType(lhs, rhs, TypeSymbol.Int, TypeSymbol.Float))
                        return TypeSymbol.Bool;
                    if (AcceptedType(lhs, rhs, TypeSymbol.Char, TypeSymbol.Double))
                        return TypeSymbol.Bool;
                    if (AcceptedType(lhs, rhs, TypeSymbol.Char, TypeSymbol.Float))
                        return TypeSymbol.Bool;
                    if (AcceptedType(lhs, rhs, TypeSymbol.Char, TypeSymbol.Int))
                        return TypeSymbol.Bool;
                    if (AcceptedType(lhs, rhs, TypeSymbol.Int))
                        return TypeSymbol.Bool;
                    if (AcceptedType(lhs, rhs, TypeSymbol.Double))
                        return TypeSymbol.Bool;
                    if (AcceptedType(lhs, rhs, TypeSymbol.Float))
                        return TypeSymbol.Bool;
                    if (AcceptedType(lhs, rhs, TypeSymbol.Char))
                        return TypeSymbol.Bool;
                    break;
                default:
                    throw new CompilerException($"Unexpected binary operator {op}");
            }
            throw new CompilerException($"Unexpected types {lhs}, {rhs} for operator {op}");
        }

        public static Func<object, object, object> GetOperation(TokenType op, TypeSymbol lhs, TypeSymbol rhs) {
            switch (op) {
                case TokenType.AND:
                case TokenType.OR:
                    if (AcceptedType(lhs, rhs, TypeSymbol.Bool))
                        return op == TokenType.AND ? (a,b) => (bool)a && (bool)b : (a, b) => (bool)a || (bool)b;
                    break;
                case TokenType.BITOR:
                    if (AcceptedType(lhs, rhs, TypeSymbol.Int))
                        return (a, b) => (int)a | (int)b;
                    break;
                case TokenType.BITXOR:
                    if (AcceptedType(lhs, rhs, TypeSymbol.Int))
                        return (a, b) => (int)a ^ (int)b;
                    break;
                case TokenType.BITAND:
                    if (AcceptedType(lhs, rhs, TypeSymbol.Int))
                        return (a, b) => (int)a & (int)b;
                    break;
                case TokenType.SHIFTLEFT:
                    if (AcceptedType(lhs, rhs, TypeSymbol.Int))
                        return (a, b) => (int)a << (int)b;
                    break;
                case TokenType.SHIFTRIGHT:
                    if (AcceptedType(lhs, rhs, TypeSymbol.Int))
                        return (a, b) => (int)a >> (int)b;
                    break;

                case TokenType.PLUS:
                    return GetPlusOperation(lhs, rhs);
                case TokenType.MINUS:
                    return GetMinusOperation(lhs, rhs);
                case TokenType.DIV:
                case TokenType.MOD:
                case TokenType.MUL:
                    return GetDotOperation(op, lhs, rhs);
                case TokenType.EQUAL:
                    return (a, b) => a == b;
                case TokenType.NOTEQUAL:
                    return (a, b) => a != b;
                case TokenType.GREATER:
                case TokenType.GREATEREQUAL:
                case TokenType.LESS:
                case TokenType.LESSEQUAL:
                    return GetCompareOperation(op, lhs, rhs);
                default:
                    throw new CompilerException($"Unexpected binary operator {op}");
            }
            throw new CompilerException($"Unexpected types {lhs}, {rhs} for operator {op}");
        }

        private static bool AcceptedType(TypeSymbol tsL, TypeSymbol tsR, TypeSymbol exp) {
            return AcceptedType(tsL, tsR, exp, exp);
        }

        private static bool AcceptedType(TypeSymbol tsL, TypeSymbol tsR, TypeSymbol exp1, TypeSymbol exp2) {
            return (tsL == exp1 && tsR == exp2) || (tsL == exp2 && tsR == exp1);
        }

        private static Func<object, object, object> GetCompareOperation(TokenType op, TypeSymbol lhs, TypeSymbol rhs, bool firstIteration = true) {
            switch(op) {
                case TokenType.LESS:
                    // same op
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Int)
                        return (a, b) => (int)a < (int)b;
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Double)
                        return (a, b) => (double)a < (double)b;
                    if (lhs == TypeSymbol.Float && rhs == TypeSymbol.Float)
                        return (a, b) => (float)a < (float)b;
                    if (lhs == TypeSymbol.Char && rhs == TypeSymbol.Char)
                        return (a, b) => (char)a < (char)b;

                    // first int
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Double)
                        return (a, b) => (int)a < (double)b;
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Float)
                        return (a, b) => (int)a < (float)b;
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Char)
                        return (a, b) => (int)a < (char)b;

                    // first double
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Float)
                        return (a, b) => (double)a < (float)b;
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Char)
                        return (a, b) => (double)a < (char)b;

                    // first float
                    if (lhs == TypeSymbol.Float && rhs == TypeSymbol.Char)
                        return (a, b) => (float)a < (char)b;
                    break;
                case TokenType.LESSEQUAL:
                    // same op
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Int)
                        return (a, b) => (int)a <= (int)b;
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Double)
                        return (a, b) => (double)a <= (double)b;
                    if (lhs == TypeSymbol.Float && rhs == TypeSymbol.Float)
                        return (a, b) => (float)a <= (float)b;
                    if (lhs == TypeSymbol.Char && rhs == TypeSymbol.Char)
                        return (a, b) => (char)a <= (char)b;

                    // first int
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Double)
                        return (a, b) => (int)a <= (double)b;
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Float)
                        return (a, b) => (int)a <= (float)b;
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Char)
                        return (a, b) => (int)a <= (char)b;

                    // first double
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Float)
                        return (a, b) => (double)a <= (float)b;
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Char)
                        return (a, b) => (double)a <= (char)b;

                    // first float
                    if (lhs == TypeSymbol.Float && rhs == TypeSymbol.Char)
                        return (a, b) => (float)a <= (char)b;
                    break;
                case TokenType.GREATER:
                    // same op
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Int)
                        return (a, b) => (int)a > (int)b;
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Double)
                        return (a, b) => (double)a > (double)b;
                    if (lhs == TypeSymbol.Float && rhs == TypeSymbol.Float)
                        return (a, b) => (float)a > (float)b;
                    if (lhs == TypeSymbol.Char && rhs == TypeSymbol.Char)
                        return (a, b) => (char)a > (char)b;

                    // first int
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Double)
                        return (a, b) => (int)a > (double)b;
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Float)
                        return (a, b) => (int)a > (float)b;
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Char)
                        return (a, b) => (int)a > (char)b;

                    // first double
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Float)
                        return (a, b) => (double)a > (float)b;
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Char)
                        return (a, b) => (double)a > (char)b;

                    // first float
                    if (lhs == TypeSymbol.Float && rhs == TypeSymbol.Char)
                        return (a, b) => (float)a > (char)b;
                    break;
                case TokenType.GREATEREQUAL:
                    // same op
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Int)
                        return (a, b) => (int)a >= (int)b;
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Double)
                        return (a, b) => (double)a >= (double)b;
                    if (lhs == TypeSymbol.Float && rhs == TypeSymbol.Float)
                        return (a, b) => (float)a >= (float)b;
                    if (lhs == TypeSymbol.Char && rhs == TypeSymbol.Char)
                        return (a, b) => (char)a >= (char)b;

                    // first int
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Double)
                        return (a, b) => (int)a >= (double)b;
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Float)
                        return (a, b) => (int)a >= (float)b;
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Char)
                        return (a, b) => (int)a >= (char)b;

                    // first double
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Float)
                        return (a, b) => (double)a >= (float)b;
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Char)
                        return (a, b) => (double)a >= (char)b;

                    // first float
                    if (lhs == TypeSymbol.Float && rhs == TypeSymbol.Char)
                        return (a, b) => (float)a >= (char)b;
                    break;

                default:
                    throw new CompilerException($"Unknown compare operation {op}");
            }

            if (!firstIteration)
                throw new CompilerException($"Unknown types {rhs}, {lhs} for operation {op}");
            return (a, b) => GetCompareOperation(op, lhs, rhs, false)(b, a);
        }

        private static Func<object, object, object> GetPlusOperation(TypeSymbol lhs, TypeSymbol rhs, bool firstIteration = true) {
            // same op
            if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Int)
                return (a, b) => (int)a + (int)b;
            if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Double)
                return (a, b) => (double)a + (double)b;
            if (lhs == TypeSymbol.Float && rhs == TypeSymbol.Float)
                return (a, b) => (float)a + (float)b;
            if (lhs == TypeSymbol.Char && rhs == TypeSymbol.Char)
                return (a, b) => (char)a + (char)b;
            if (lhs == TypeSymbol.String && rhs == TypeSymbol.String)
                return (a, b) => (string)a + (string)b;

            // first int
            if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Double)
                return (a, b) => (int)a + (double)b;
            if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Float)
                return (a, b) => (int)a + (float)b;
            if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Char)
                return (a, b) => (int)a + (char)b;
            if (lhs == TypeSymbol.Int && rhs == TypeSymbol.String)
                return (a, b) => (int)a + (string)b;

            // first double
            if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Float)
                return (a, b) => (double)a + (float)b;
            if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Char)
                return (a, b) => (double)a + (char)b;
            if (lhs == TypeSymbol.Double && rhs == TypeSymbol.String)
                return (a, b) => (double)a + (string)b;

            // first float
            if (lhs == TypeSymbol.Float && rhs == TypeSymbol.Char)
                return (a, b) => (float)a + (char)b;
            if (lhs == TypeSymbol.Float && rhs == TypeSymbol.String)
                return (a, b) => (float)a + (string)b;

            // first char
            if (lhs == TypeSymbol.Char && rhs == TypeSymbol.String)
                return (a, b) => (char)a + (string)b;

            if (!firstIteration)
                throw new CompilerException($"Unknown types {rhs}, {lhs} for operation PLUS");
            return (a,b) => GetPlusOperation(lhs,rhs, false)(b, a);
        }

        private static Func<object, object, object> GetMinusOperation(TypeSymbol lhs, TypeSymbol rhs, bool firstIteration = true) {
            // same op
            if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Int)
                return (a, b) => (int)a - (int)b;
            if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Double)
                return (a, b) => (double)a - (double)b;
            if (lhs == TypeSymbol.Float && rhs == TypeSymbol.Float)
                return (a, b) => (float)a - (float)b;
            if (lhs == TypeSymbol.Char && rhs == TypeSymbol.Char)
                return (a, b) => (char)a - (char)b;

            // first int
            if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Double)
                return (a, b) => (int)a - (double)b;
            if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Float)
                return (a, b) => (int)a - (float)b;
            if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Char)
                return (a, b) => (int)a - (char)b;

            // first double
            if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Float)
                return (a, b) => (double)a - (float)b;
            if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Char)
                return (a, b) => (double)a - (char)b;

            // first float
            if (lhs == TypeSymbol.Float && rhs == TypeSymbol.Char)
                return (a, b) => (float)a - (char)b;

            if (!firstIteration)
                throw new CompilerException($"Unknown types {rhs}, {lhs} for operation MINUS");
            return (a, b) => GetMinusOperation(lhs, rhs, false)(b, a);
        }

        private static Func<object, object, object> GetDotOperation(TokenType op, TypeSymbol lhs, TypeSymbol rhs, bool firstIteration = true) {
            switch(op) {
                case TokenType.DIV:
                    // same op
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Int)
                        return (a, b) => (int)a / (int)b;
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Double)
                        return (a, b) => (double)a / (double)b;
                    if (lhs == TypeSymbol.Float && rhs == TypeSymbol.Float)
                        return (a, b) => (float)a / (float)b;

                    // first int
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Double)
                        return (a, b) => (int)a / (double)b;
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Float)
                        return (a, b) => (int)a / (float)b;

                    // first double
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Float)
                        return (a, b) => (double)a / (float)b;
                    break;
                case TokenType.MUL:
                    // same op
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Int)
                        return (a, b) => (int)a * (int)b;
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Double)
                        return (a, b) => (double)a * (double)b;
                    if (lhs == TypeSymbol.Float && rhs == TypeSymbol.Float)
                        return (a, b) => (float)a * (float)b;

                    // first int
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Double)
                        return (a, b) => (int)a * (double)b;
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Float)
                        return (a, b) => (int)a * (float)b;

                    // first double
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Float)
                        return (a, b) => (double)a * (float)b;
                    break;
                case TokenType.MOD:
                    // same op
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Int)
                        return (a, b) => (int)a % (int)b;
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Double)
                        return (a, b) => (double)a % (double)b;
                    if (lhs == TypeSymbol.Float && rhs == TypeSymbol.Float)
                        return (a, b) => (float)a % (float)b;

                    // first int
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Double)
                        return (a, b) => (int)a % (double)b;
                    if (lhs == TypeSymbol.Int && rhs == TypeSymbol.Float)
                        return (a, b) => (int)a % (float)b;

                    // first double
                    if (lhs == TypeSymbol.Double && rhs == TypeSymbol.Float)
                        return (a, b) => (double)a % (float)b;
                    break;

                default:
                    throw new CompilerException($"Unknown dot operation {op}");
            }

            if (!firstIteration)
                throw new CompilerException($"Unknown types {rhs}, {lhs} for operation {op}");
            return (a, b) => GetDotOperation(op, lhs, rhs, false)(b, a);
        }

    }
}
