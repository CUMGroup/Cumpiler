using Cumpiler.Syntax.Visitors.Abstract;

namespace Cumpiler.Syntax.Node {
    internal abstract class Node {

        public abstract void AcceptVisitor(INodeVisitor visitor, bool before);
    }
}
