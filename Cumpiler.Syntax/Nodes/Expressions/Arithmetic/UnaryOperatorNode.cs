
using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Syntax.Visitors.Abstract;

namespace Cumpiler.Syntax.Nodes.Expressions.Arithmetic {
    internal class UnaryOperatorNode : ExpressionNode {

        public ExpressionNode Arg { get; set; }
        public TokenType Operation { get; set; }

        public object? ContentValue { get; set; }

        public UnaryOperatorNode(ExpressionNode arg, TokenType op) : this(arg, op, null) {}

        public UnaryOperatorNode(ExpressionNode arg, TokenType op, object? val) {
            Arg = arg;
            Operation = op;
            ContentValue = val;
        }

        public override void AcceptVisitor(INodeVisitor visitor) {
            visitor.Visit(this);
        }
    }
}
