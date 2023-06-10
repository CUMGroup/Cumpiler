using Cumpiler.Lexer;
using Cumpiler.Lexer.Common.Interfaces;
using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Syntax;
using Cumpiler.Syntax.Expressions;
using Cumpiler.Syntax.Nodes.Expressions.Arithmetic;
using Cumpiler.Syntax.Nodes.Expressions.Literals;

namespace Cumpiler.UnitTests.Parser.Expressions;
public class ExpressionParserTest {

    [Fact]
    public void ParseExpression_ShouldParseComplex() {
        string input = """
        2 * (2+2) > 3 && 2 == 3 ? 2 << 2 : (2+2)
        """;
        ILexer lexer = LexerFactory.CreateLexer(input);
        ExpressionParser parser = new ExpressionParser(lexer);
        var expr = parser.ParseExpression();

        Assert.IsType<TernaryOperatorNode>(expr);
        Assert.IsType<BinaryOperatorNode>(((TernaryOperatorNode)expr).Expr);

        var lhs = (BinaryOperatorNode)((TernaryOperatorNode)expr).Expr;
        Assert.Equal(TokenType.EQUAL, lhs.Operation);
        Assert.IsType<LiteralNode>(lhs.Rhs);
        Assert.Equal(3, ((LiteralNode)lhs.Rhs).Args);

        Assert.IsType<BinaryOperatorNode>(lhs.Lhs);
        lhs = (BinaryOperatorNode)lhs.Lhs;
        Assert.Equal(TokenType.AND, lhs.Operation);
        Assert.IsType<LiteralNode>(lhs.Rhs);
        Assert.Equal(2, ((LiteralNode)lhs.Rhs).Args);

        Assert.IsType<BinaryOperatorNode>(lhs.Lhs);
        lhs = (BinaryOperatorNode)lhs.Lhs;
        Assert.Equal(TokenType.GREATER, lhs.Operation);
        Assert.IsType<LiteralNode>(lhs.Rhs);
        Assert.Equal(3, ((LiteralNode)lhs.Rhs).Args);

        Assert.IsType<BinaryOperatorNode>(lhs.Lhs);
        lhs = (BinaryOperatorNode)lhs.Lhs;
        Assert.Equal(TokenType.MUL, lhs.Operation);
        Assert.IsType<LiteralNode>(lhs.Lhs);
        Assert.Equal(2, ((LiteralNode)lhs.Lhs).Args);

        Assert.IsType<BinaryOperatorNode>(lhs.Rhs);
        lhs = (BinaryOperatorNode)lhs.Rhs;
        Assert.Equal(TokenType.PLUS, lhs.Operation);
        Assert.IsType<LiteralNode>(lhs.Lhs);
        Assert.IsType<LiteralNode>(lhs.Rhs);

        Assert.Equal(2, ((LiteralNode)lhs.Lhs).Args);
        Assert.Equal(2, ((LiteralNode)lhs.Rhs).Args);
    }
}
