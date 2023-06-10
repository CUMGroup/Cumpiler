using Cumpiler.Lexer.Common.Interfaces;
using Cumpiler.Lexer;
using Cumpiler.Syntax.Expressions;
using Cumpiler.Syntax;

namespace Cumpiler.UnitTests.Parser.Visitor {
    public class EvalVisitorTest {

        [Fact]
        public void Eval_Correct() {
            string input = """
        2 * (2+2) > 3 && 2+2 == 3-3 ? 1 / 0 : (2+2)
        """;
            ILexer lexer = LexerFactory.CreateLexer(input);
            ExpressionParser parser = new ExpressionParser(lexer);

            var expr = parser.ParseExpression();

            var evalVisitor = new AstEvalVisitor();
            expr.AcceptVisitor(evalVisitor);

            Assert.Equal(4, evalVisitor.Value);
        }

        [Fact]
        public void Eval_Parans() {
            string input = """
             -3 * (2+ 3 * (7 - 5 * (2 / 3)))
        """;
            ILexer lexer = LexerFactory.CreateLexer(input);
            ExpressionParser parser = new ExpressionParser(lexer);

            var expr = parser.ParseExpression();

            var evalVisitor = new AstEvalVisitor();
            expr.AcceptVisitor(evalVisitor);

            Assert.Equal(-39, evalVisitor.Value);
        }

    }
}
