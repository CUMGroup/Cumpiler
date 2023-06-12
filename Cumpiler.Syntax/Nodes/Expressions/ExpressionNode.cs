using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Syntax.Symbols;

namespace Cumpiler.Syntax.Nodes.Expressions {
    internal abstract class ExpressionNode : Node {
        public virtual TypeSymbol Type { get; set; } = TypeSymbol.None;

        public ExpressionNode(TokenPos pos) : base(pos) {
            
        }
    }
}
