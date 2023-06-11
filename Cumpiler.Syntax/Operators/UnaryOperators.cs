using Cumpiler.Lexer.Common.Exceptions;
using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Syntax.Symbols;

namespace Cumpiler.Syntax.Operators {
    internal static class UnaryOperators {

        public static TypeSymbol GetTypeFromOperation(TokenType op, TypeSymbol arg) {
            switch(op) {
                case TokenType.PLUS:
                    if (arg == TypeSymbol.Double ||
                        arg == TypeSymbol.Float ||
                        arg == TypeSymbol.Int)
                        return arg;
                    break;
                case TokenType.MINUS:
                    if (arg == TypeSymbol.Double ||
                        arg == TypeSymbol.Float ||
                        arg == TypeSymbol.Int)
                        return arg;
                    break;
                case TokenType.NOT:
                    if (arg == TypeSymbol.Bool)
                        return arg;
                    break;
                case TokenType.BITCOMPLEMENT:
                    if (arg == TypeSymbol.Int)
                        return arg;
                    break;
                default:
                    throw new CompilerException($"Unexpected unary operator {op}");
            }
            throw new CompilerException($"Unexpected type {arg} for operator {op}");
        }

        public static Func<object, object> GetOperation(TokenType op, TypeSymbol arg) {
            switch(op) {
                case TokenType.PLUS:
                    if (arg == TypeSymbol.Double ||
                        arg == TypeSymbol.Float ||
                        arg == TypeSymbol.Int)
                        return (a) => a;
                    break;
                case TokenType.MINUS:
                    if (arg == TypeSymbol.Double)
                        return (a) => -(double)a;
                    if (arg == TypeSymbol.Float)
                        return (a) => -(float)a;
                    if (arg == TypeSymbol.Int)
                        return (a) => -(int)a;
                    break;
                case TokenType.NOT:
                    if (arg == TypeSymbol.Bool)
                        return (a) => !(bool)a;
                    break;
                case TokenType.BITCOMPLEMENT:
                    if (arg == TypeSymbol.Int)
                        return (a) => ~(int)a;
                    break;
                default:
                    throw new CompilerException($"Unexpected unary operator {op}");
            }
            throw new CompilerException($"Unexpected type {arg} for operator {op}");
        }
    }
}
