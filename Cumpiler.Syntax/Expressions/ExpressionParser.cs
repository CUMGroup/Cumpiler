
using Cumpiler.Lexer.Common.Interfaces;
using Cumpiler.Lexer.Common.Tokens;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace Cumpiler.Syntax.Expressions {
    internal class ExpressionParser {

        private readonly ILexer _lexer;

        public ExpressionParser(ILexer lexer) {
            _lexer = lexer;
        }

        /// <summary>
        /// Generic Parser Method for the different operators in the order of operations
        /// </summary>
        /// <param name="nextOperator">Next operator group method in the order of operations</param>
        /// <param name="tokenToOperator">Mapping of the operator tokens in the group to specific functions</param>
        /// <returns></returns>
        private Node ParseOperator(Func<Node> nextOperator, Func<TokenType, Func<double, double, double>> tokenToOperator) {
            Node lhs = nextOperator();

            while (true) {
                Func<double, double, double> op = tokenToOperator(lexer.CurrentToken.Type);

                if (op == null)
                    return lhs;

                lexer.NextToken();

                Node rhs = nextOperator();
                lhs = new NodeBinary(lhs, rhs, op);
            }
        }

    }
}
