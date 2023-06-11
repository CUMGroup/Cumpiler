using Cumpiler.Lexer.Common.Exceptions;
using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Syntax.Symbols;

namespace Cumpiler.Syntax.Operators {
    internal static class UnaryOperators {

        public static TypeSymbol GetTypeFromOperation(TokenType op, object? content, TypeSymbol arg) {
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
                case TokenType.CAST:
                    if (content is null)
                        break;
                    var type = TypeSymbol.TypeExists((string)content);
                    if (type is null)
                        break;

                    return type;
                default:
                    throw new CompilerException($"Unexpected unary operator {op}");
            }
            throw new CompilerException($"Unexpected type {arg} for operator {op}");
        }

        public static Func<object, object> GetOperation(TokenType op, object? content, TypeSymbol arg) {
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
                case TokenType.CAST:
                    if (content is null)
                        break;
                    var type = TypeSymbol.TypeExists((string)content);
                    if (type is null)
                        break;

                    if (type == TypeSymbol.Bool)
                        return (a) => Convert.ChangeType(a,typeof(bool));
                    if (type == TypeSymbol.Char)
                        return (a) => Convert.ChangeType(a, typeof(char));
                    if (type == TypeSymbol.Double)
                        return (a) => Convert.ChangeType(a, typeof(double));
                    if (type == TypeSymbol.Float)
                        return (a) => Convert.ChangeType(a, typeof(float));
                    if (type == TypeSymbol.Int)
                        return (a) => Convert.ChangeType(a, typeof(int));
                    if (type == TypeSymbol.String)
                        return (a) => Convert.ChangeType(a, typeof(string));
                    break;
                default:
                    throw new CompilerException($"Unexpected unary operator {op}");
            }
            throw new CompilerException($"Unexpected type {arg} for operator {op}");
        }
    }
}
