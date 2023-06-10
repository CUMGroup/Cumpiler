using Cumpiler.Syntax.Nodes.Expressions;
using Cumpiler.Syntax.Visitors.Abstract;

namespace Cumpiler.Syntax;
internal class TernaryOperatorNode : ExpressionNode {

    public ExpressionNode Expr { get; init; }
    public ExpressionNode ExprTrue { get; init; }
    public ExpressionNode ExprFalse { get; init; }

    public TernaryOperatorNode(ExpressionNode expr, ExpressionNode exprTrue, ExpressionNode exprFalse) {
        Expr = expr;
        ExprTrue = exprTrue;
        ExprFalse = exprFalse;
    }

    public override void AcceptVisitor(INodeVisitor visitor) {
        visitor.Visit(this);
    }
}
