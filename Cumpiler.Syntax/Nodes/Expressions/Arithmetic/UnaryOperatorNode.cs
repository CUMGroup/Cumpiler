
using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Syntax.Visitors.Abstract;

namespace Cumpiler.Syntax.Nodes.Expressions.Arithmetic {
    internal class UnaryOperatorNode : ExpressionNode {

        public ExpressionNode Arg { get; set; }
        public TokenType Operation { get; set; }

        public object? ContentValue { get; set; }

        public UnaryOperatorNode(ExpressionNode arg, TokenType op, TokenPos pos) : this(arg, op, null, pos) {}

        public UnaryOperatorNode(ExpressionNode arg, TokenType op, object? val, TokenPos pos) : base(pos) {
            Arg = arg;
            Operation = op;
            ContentValue = val;
        }

        public override void AcceptVisitor(INodeVisitor visitor) {
            visitor.Visit(this);
        }
    }
}
