using Cumpiler.Lexer.Helpers;
using Cumpiler.Syntax.Nodes.Expressions.Arithmetic;
using Cumpiler.Syntax.Nodes.Expressions.Literals;
using Cumpiler.Syntax.Visitors.Abstract;

namespace Cumpiler.Syntax;
internal class AstEvalVisitor : INodeVisitor {

    public double Value { get; set; }

    public void Visit(TernaryOperatorNode node) {
        node.Expr.AcceptVisitor(this);
        if(Value > 0) {
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
        Value = node.Operation.GetTokenBinaryOperationFunc() (lhsVal, rhsVal);
    }

    public void Visit(UnaryOperatorNode node) {
        node.Arg.AcceptVisitor(this);
        Value = node.Operation.GetTokenUnaryOperationFunc() (Value);
    }

    public void Visit(LiteralNode node) {
        Value = node.Args;
    }
}
