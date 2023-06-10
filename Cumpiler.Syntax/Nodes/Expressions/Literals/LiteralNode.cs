using Cumpiler.Syntax.Visitors.Abstract;

namespace Cumpiler.Syntax.Nodes.Expressions.Literals {
    internal class LiteralNode : ExpressionNode {

        public double Args { get; init; }

        public LiteralNode(double args) {
            Args = args;
        }

        public override void AcceptVisitor(INodeVisitor visitor) {
            visitor.Visit(this);
        }
    }
}
