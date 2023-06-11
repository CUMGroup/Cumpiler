using Cumpiler.Lexer.Common.Interfaces;
using Cumpiler.Lexer;
using Cumpiler.Syntax.Expressions;
using Cumpiler.Syntax;
using Cumpiler.Syntax.Visitors;
using Cumpiler.Lexer.Common.Exceptions;

namespace Cumpiler.UnitTests.Parser.Visitor {
    public class EvalVisitorTest {

        [Fact]
        public void Eval_Correct() {
            string input = """
        2 * (2+2) > 3 && 2+2 == 3-3 ? 1 / 0 : (2+2)
        """;
            var val = Eval(input);

            Assert.Equal(4, val);
        }

        [Fact]
        public void Eval_Parans() {
            string input = """
             -3 * (2+ 3 * (7 - 5 * (2.0 / 3)))
        """;
            var val = Eval(input);

            Assert.Equal(-39d, val);
        }


        [Fact]
        public void Eval_Cast() {
            string input = """
             (bool)2 && true
        """;
            var val = Eval(input);

            Assert.Equal(true, val);
        }

        [Fact]
        public void Eval_invalidOperation() {
            string input = "2+3-\"a\"";

            Assert.Throws<CompilerException>(() => Eval(input));
        }

        private object Eval(string input) {
            ILexer lexer = LexerFactory.CreateLexer(input);
            ExpressionParser parser = new ExpressionParser(lexer);

            var expr = parser.ParseExpression();
            var typeVisitor = new AstTypeCheckVisitor();
            expr.AcceptVisitor(typeVisitor);
            var evalVisitor = new AstEvalVisitor();
            expr.AcceptVisitor(evalVisitor);

            return evalVisitor.Value;
        }

    }
}
