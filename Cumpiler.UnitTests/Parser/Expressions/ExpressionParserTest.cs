using Cumpiler.Lexer;
using Cumpiler.Lexer.Common.Exceptions;
using Cumpiler.Lexer.Common.Interfaces;
using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Syntax;
using Cumpiler.Syntax.Expressions;
using Cumpiler.Syntax.Nodes.Expressions.Arithmetic;
using Cumpiler.Syntax.Nodes.Expressions.Literals;
using System.Text;
using Xunit.Abstractions;

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
        Assert.Equal(TokenType.AND, lhs.Operation);
        Assert.IsType<BinaryOperatorNode>(lhs.Lhs);
        Assert.IsType<BinaryOperatorNode>(lhs.Rhs);

        var rhs = (BinaryOperatorNode)lhs.Rhs;
        Assert.Equal(TokenType.EQUAL, rhs.Operation);
        Assert.IsType<LiteralNode>(rhs.Lhs);
        Assert.IsType<LiteralNode>(rhs.Rhs);
        Assert.Equal(2, ((LiteralNode)rhs.Lhs).Args);
        Assert.Equal(3, ((LiteralNode)rhs.Rhs).Args);

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

    [Fact]
    public void Parse_PunktVorStrich() {
        string input = """
        2 + 2 * 2
        """;

        var lexer = LexerFactory.CreateLexer(input);
        var parser = new ExpressionParser(lexer);

        var expr = parser.ParseExpression();

        Assert.IsType<BinaryOperatorNode>(expr);
        var bin = (BinaryOperatorNode)expr;

        Assert.IsType<LiteralNode>(bin.Lhs);
        Assert.Equal(2, ((LiteralNode)bin.Lhs).Args);

        Assert.IsType<BinaryOperatorNode>(bin.Rhs);
        bin = (BinaryOperatorNode)bin.Rhs;

        Assert.IsType<LiteralNode>(bin.Lhs);
        Assert.Equal(2, ((LiteralNode)bin.Lhs).Args);
        Assert.IsType<LiteralNode>(bin.Rhs);
        Assert.Equal(2, ((LiteralNode)bin.Rhs).Args);
    }

    [Fact]
    public void Parser_ShouldThrow_OnWrongParans() {

        var input = """
            2 + (2+3
            """;

        var lexer = LexerFactory.CreateLexer(input);
        var parser = new ExpressionParser(lexer);

        Assert.Throws<CompilerException>(() => parser.ParseExpression());
    }
}

