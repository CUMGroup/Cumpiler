using Cumpiler.Syntax.Nodes.Expressions.Arithmetic;
using Cumpiler.Syntax.Nodes.Expressions.Literals;
using Cumpiler.Syntax.Operators;
using Cumpiler.Syntax.Visitors.Abstract;

namespace Cumpiler.Syntax;
internal class AstEvalVisitor : INodeVisitor {

    public object Value { get; set; } = 0;

    public void Visit(TernaryOperatorNode node) {
        node.Expr.AcceptVisitor(this);
        if((bool)Value) {
            node.ExprTrue.AcceptVisitor(this);
        }else {
            node.ExprFalse.AcceptVisitor(this);
        }
    }

    public void Visit(BinaryOperatorNode node) {
        node.Lhs.AcceptVisitor(this);
        var lhsVal = Value;
        node.Rhs.AcceptVisitor(this);
        var rhsVal = Value;
        Value = BinaryOperators.GetOperation(node.Operation, node.Lhs.Type, node.Rhs.Type) ((object)lhsVal, (object)rhsVal);
    }

    public void Visit(UnaryOperatorNode node) {
        node.Arg.AcceptVisitor(this);
        Value = UnaryOperators.GetOperation(node.Operation, node.Arg.Type) (Value);
    }

    public void Visit(LiteralNode node) {
        Value = node.Args;
    }
}
