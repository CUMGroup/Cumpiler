using Cumpiler.Lexer.Common.Exceptions;
using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Syntax.Symbols;
using Cumpiler.Syntax.Visitors.Abstract;

namespace Cumpiler.Syntax.Nodes.Expressions.Literals {
    internal class LiteralNode : ExpressionNode {

        public object Args { get; init; }

        public LiteralNode(object args, TokenPos pos) : base(pos) {
            if (args.GetType() == typeof(int)) {
                Type = TypeSymbol.Int;
            } else if (args.GetType() == typeof(double)) {
                Type = TypeSymbol.Double;
            } else if (args.GetType() == typeof(float)) {
                Type = TypeSymbol.Float;
            } else if (args.GetType() == typeof(string)) {
                Type = TypeSymbol.String;
            } else if (args.GetType() == typeof(char)) {
                Type = TypeSymbol.Char;
            } else if(args.GetType() == typeof(bool)) {
                Type = TypeSymbol.Bool;
            }else {
                throw new CompilerException($"Unexpected literal {args} of type {args.GetType()}");
            }
            Args = args;
        }

        public override void AcceptVisitor(INodeVisitor visitor) {
            visitor.Visit(this);
        }
    }
}
