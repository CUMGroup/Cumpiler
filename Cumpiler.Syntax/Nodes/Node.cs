using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Syntax.Visitors.Abstract;

namespace Cumpiler.Syntax.Nodes {
    internal abstract class Node {

        public abstract void AcceptVisitor(INodeVisitor visitor);
        public TokenPos TokenPos { get; init; }

        public Node(TokenPos pos) {
            TokenPos = pos;
        }
    }
}
