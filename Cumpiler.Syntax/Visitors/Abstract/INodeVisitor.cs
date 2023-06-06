using Cumpiler.Syntax.Node.Expressions.Arithmetic;
using Cumpiler.Syntax.Node.Expressions.Literals;

namespace Cumpiler.Syntax.Visitors.Abstract {
    internal interface INodeVisitor {

        void VisitBinaryOperator(BinaryOperatorNode node);

        void VisitUnaryOperator(UnaryOperatorNode node);

        void VisitLiteralNode(LiteralNode node);
    }
}
