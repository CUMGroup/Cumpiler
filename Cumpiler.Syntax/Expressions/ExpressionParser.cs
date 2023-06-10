
using Cumpiler.Lexer.Common.Interfaces;
using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Lexer.Helpers;
using Cumpiler.Syntax.Nodes.Expressions;
using Cumpiler.Syntax.Nodes.Expressions.Arithmetic;
using Cumpiler.Syntax.Nodes.Expressions.Literals;

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
        private ExpressionNode ParseOperator(Func<ExpressionNode> nextOperator, Func<TokenType, bool> validToken) {
            ExpressionNode lhs = nextOperator();

            while (true) {
                bool nextValid = validToken(_lexer.LookAhead.Type);

                if (!nextValid)
                    return lhs;

                var op = _lexer.Advance();

                ExpressionNode rhs = nextOperator();
                lhs = new BinaryOperatorNode(lhs, rhs, op.Type);
            }
        }

        private Func<TokenType,bool> BuildTokenTypeAcceptFunction(params TokenType[] acceptedTokens) {
            return (t) => acceptedTokens.Contains(t);
        }

        public ExpressionNode ParseExpression() {
            return ParseQuestionMarkExpr();
        }

        private ExpressionNode ParseQuestionMarkExpr() {
            // equalityExpr (QUESTIONMARK equalityExpr DOUBLECOLON equalityExpr)?
            var lhs = ParseEqualityExpr();
            if(_lexer.Accept(TokenType.QUESTIONMARK)) {
                var trueExpr = ParseEqualityExpr();
                _lexer.Expect(TokenType.DOUBLECOLON);
                var falseExpr = ParseEqualityExpr();
                lhs = new TernaryOperatorNode(lhs, trueExpr, falseExpr);
            }
            return lhs;
        }

        private ExpressionNode ParseEqualityExpr() {
            // logicOrExpr (EQUAL logicOrExpr)?
            var lhs = ParseLogicOrExpr();
            var op = _lexer.LookAhead.Type;
            if(_lexer.Accept(TokenType.EQUAL) || _lexer.Accept(TokenType.NOTEQUAL)) {
                var rhs = ParseLogicOrExpr();
                lhs = new BinaryOperatorNode(lhs, rhs, op);
            }
            return lhs;
        }

        private ExpressionNode ParseLogicOrExpr() {
            return ParseOperator(ParseLogicAndExpr, BuildTokenTypeAcceptFunction(TokenType.OR));
        }

        private ExpressionNode ParseLogicAndExpr() {
            return ParseOperator(ParseBitOrExpr, BuildTokenTypeAcceptFunction(TokenType.AND));
        }

        private ExpressionNode ParseBitOrExpr() {
            return ParseOperator(ParseBitAndExpr, BuildTokenTypeAcceptFunction(TokenType.BITOR));
        }

        private ExpressionNode ParseBitAndExpr() {
            return ParseOperator(ParseCompareExpr, BuildTokenTypeAcceptFunction(TokenType.BITAND, TokenType.BITXOR));
        }

        private ExpressionNode ParseCompareExpr() {
            // shiftExpr ((GREATER | GREATEREQUAL | LESS | LESSEQUAL) shiftExpr)?
            var lhs = ParseBitshiftExpr();
            var op = _lexer.LookAhead.Type;
            if(op is TokenType.GREATER or TokenType.GREATEREQUAL or TokenType.LESS or TokenType.LESSEQUAL) {
                _lexer.Advance();
                var rhs = ParseBitshiftExpr();
                lhs = new BinaryOperatorNode(lhs, rhs, op);
            }
            return lhs;
        }

        private ExpressionNode ParseBitshiftExpr() {
            return ParseOperator(ParseAdditionExpr, BuildTokenTypeAcceptFunction(TokenType.SHIFTLEFT, TokenType.SHIFTRIGHT));
        }

        private ExpressionNode ParseAdditionExpr() {
            return ParseOperator(ParseMutliplicationExpr, BuildTokenTypeAcceptFunction(TokenType.PLUS, TokenType.MINUS));
        }

        private ExpressionNode ParseMutliplicationExpr() {
            return ParseOperator(ParseUnaryExpr, BuildTokenTypeAcceptFunction(TokenType.MUL, TokenType.DIV));
        }

        private ExpressionNode ParseUnaryExpr() {
            // (PLUS | MINUS | NOT | COMPLEMENT)? literalExpr
            if(_lexer.Accept(TokenType.PLUS)) {
                // nop
                return ParseLiteralExpr();
            }else if(_lexer.LookAhead.Type is TokenType.MINUS or TokenType.NOT or TokenType.BITCOMPLEMENT) {
                var op = _lexer.Advance();
                var rhs = ParseLiteralExpr();
                return new UnaryOperatorNode(rhs, op.Type);
            }
            return ParseLiteralExpr();
        }

        private ExpressionNode ParseLiteralExpr() {
            if(_lexer.LookAhead.IsNumber()) {
                var num = _lexer.Advance();
                return new LiteralNode(double.Parse(num.Value));
            }else if(_lexer.Accept(TokenType.LPAREN)) {
                var expr = ParseExpression();
                _lexer.Expect(TokenType.RPAREN);
                return expr;
            }
            throw new NotImplementedException("Vars not implemented yet!");
        }

    }
}
