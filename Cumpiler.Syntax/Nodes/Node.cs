using Cumpiler.Syntax.Visitors.Abstract;

namespace Cumpiler.Syntax.Nodes {
    internal abstract class Node {

        public abstract void AcceptVisitor(INodeVisitor visitor);
    }
}
