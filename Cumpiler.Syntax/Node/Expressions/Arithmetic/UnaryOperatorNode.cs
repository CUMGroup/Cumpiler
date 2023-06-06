
using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Syntax.Visitors.Abstract;

namespace Cumpiler.Syntax.Node.Expressions.Arithmetic {
    internal class UnaryOperatorNode : ExpressionNode {

        public required Node Arg { get; set; }

        public required TokenType Operation { get; set; }

        public UnaryOperatorNode() {}

        public UnaryOperatorNode(Node arg, TokenType op) {
            Arg = arg;
            Operation = op;
        }

        public override void AcceptVisitor(INodeVisitor visitor, bool before) {
            if(before)
                visitor.VisitUnaryOperator(this);

            Arg.AcceptVisitor(visitor, before);

            if (!before)
                visitor.VisitUnaryOperator(this);
        }
    }
}
