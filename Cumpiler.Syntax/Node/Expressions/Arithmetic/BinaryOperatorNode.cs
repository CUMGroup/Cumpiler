using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Syntax.Visitors.Abstract;

namespace Cumpiler.Syntax.Node.Expressions.Arithmetic {
    internal class BinaryOperatorNode : ExpressionNode {

        public required Node Lhs { get; init; }
        public required Node Rhs { get; init; }

        public required TokenType Operation { get; init; }

        public BinaryOperatorNode() {}

        public BinaryOperatorNode(Node lhs, Node rhs, TokenType op) {
            Lhs = lhs;
            Rhs = rhs;
            Operation = op;
        }

        public override void AcceptVisitor(INodeVisitor visitor, bool before) {
            if(before) {
                visitor.VisitBinaryOperator(this);
            }

            Lhs.AcceptVisitor(visitor, before);
            Rhs.AcceptVisitor(visitor, before);

            if(!before) {
                visitor.VisitBinaryOperator(this);
            }
        }
    }
}
