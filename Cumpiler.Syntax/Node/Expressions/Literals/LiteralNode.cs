using Cumpiler.Syntax.Visitors.Abstract;

namespace Cumpiler.Syntax.Node.Expressions.Literals {
    internal class LiteralNode : ExpressionNode {




        public override void AcceptVisitor(INodeVisitor visitor, bool before) {
            if (before)
                visitor.VisitLiteralNode(this);



            if (!before)
                visitor.VisitLiteralNode(this);
        }
    }
}
