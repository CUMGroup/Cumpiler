using Cumpiler.Syntax.Nodes.Expressions.Arithmetic;
using Cumpiler.Syntax.Nodes.Expressions.Literals;

namespace Cumpiler.Syntax.Visitors.Abstract {
    internal interface INodeVisitor {

        void Visit(TernaryOperatorNode node);
        void Visit(BinaryOperatorNode node);

        void Visit(UnaryOperatorNode node);

        void Visit(LiteralNode node);
    }
}
