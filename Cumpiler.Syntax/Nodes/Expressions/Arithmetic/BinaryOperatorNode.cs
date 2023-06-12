using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Syntax.Visitors.Abstract;

namespace Cumpiler.Syntax.Nodes.Expressions.Arithmetic {
    internal class BinaryOperatorNode : ExpressionNode {

        public ExpressionNode Lhs { get; init; }
        public ExpressionNode Rhs { get; init; }

        public TokenType Operation { get; init; }

        public BinaryOperatorNode(ExpressionNode lhs, ExpressionNode rhs, TokenType op, TokenPos pos) : base(pos) {
            Lhs = lhs;
            Rhs = rhs;
            Operation = op;
        }

        public override void AcceptVisitor(INodeVisitor visitor) {
            visitor.Visit(this);
        }
    }
}
