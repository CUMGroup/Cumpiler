
using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Syntax.Visitors.Abstract;

namespace Cumpiler.Syntax.Nodes.Expressions.Arithmetic {
    internal class UnaryOperatorNode : ExpressionNode {

        public ExpressionNode Arg { get; set; }

        public TokenType Operation { get; set; }

        public UnaryOperatorNode(ExpressionNode arg, TokenType op) {
            Arg = arg;
            Operation = op;
        }

        public override void AcceptVisitor(INodeVisitor visitor) {
            visitor.Visit(this);
        }
    }
}
