
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

        /** <summary>
        * Generic Parser Method for an expr of form<br></br> 'expr: <paramref name="nextOperator"/> (VALIDTOKEN <paramref name="nextOperator"/>)*'
        * </summary>
        * <param name="nextOperator">Next operator group method in the order of operations</param>
        * <param name="tokenToOperator">Mapping of the operator tokens in the group to specific functions</param>
        * <returns></returns> **/
        private ExpressionNode ParseChainedOperator(Func<ExpressionNode> nextOperator, Func<TokenType, bool> validToken) {
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

        /** <summary>
        * Generic Parser Method for an expr of form<br></br> 'expr: <paramref name="nextOperator"/> (VALIDTOKEN <paramref name="nextOperator"/>)?'
        * </summary>
        * <param name="nextOperator">Next operator group method in the order of operations</param>
        * <param name="tokenToOperator">Mapping of the operator tokens in the group to specific functions</param>
        * <returns></returns> **/
        private ExpressionNode ParseSingleOperator(Func<ExpressionNode> nextOperator, Func<TokenType, bool> validToken) {
            ExpressionNode lhs = nextOperator();
            if(validToken(_lexer.LookAhead.Type)) {
                var op = _lexer.Advance();
                var rhs = nextOperator();
                lhs = new BinaryOperatorNode(lhs, rhs, op.Type);
            }
            return lhs;
        }

        private Func<TokenType,bool> BuildTokenTypeAcceptFunction(params TokenType[] acceptedTokens) {
            return (t) => acceptedTokens.Contains(t);
        }


        public ExpressionNode ParseExpression() {
            // expr: questionMarkExpr
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
            return ParseSingleOperator(ParseLogicOrExpr, BuildTokenTypeAcceptFunction(TokenType.EQUAL, TokenType.NOTEQUAL));
        }

        private ExpressionNode ParseLogicOrExpr() {
            // logicAndExpr (LOGICOR logicAndExpr)*
            return ParseChainedOperator(ParseLogicAndExpr, BuildTokenTypeAcceptFunction(TokenType.OR));
        }

        private ExpressionNode ParseLogicAndExpr() {
            // bitOrExpr (LOGICAND bitOrExpr)*
            return ParseChainedOperator(ParseBitOrExpr, BuildTokenTypeAcceptFunction(TokenType.AND));
        }

        private ExpressionNode ParseBitOrExpr() {
            // bitAndExpr (BITOR bitAndExpr)*
            return ParseChainedOperator(ParseBitAndExpr, BuildTokenTypeAcceptFunction(TokenType.BITOR));
        }

        private ExpressionNode ParseBitAndExpr() {
            // compareExpr ((BITAND | BITXOR) compareExpr)*
            return ParseChainedOperator(ParseCompareExpr, BuildTokenTypeAcceptFunction(TokenType.BITAND, TokenType.BITXOR));
        }

        private ExpressionNode ParseCompareExpr() {
            // shiftExpr ((GREATER | GREATEREQUAL | LESS | LESSEQUAL) shiftExpr)?
            return ParseSingleOperator(ParseBitshiftExpr, BuildTokenTypeAcceptFunction(TokenType.GREATER, TokenType.GREATEREQUAL, TokenType.LESS, TokenType.LESSEQUAL));
        }

        private ExpressionNode ParseBitshiftExpr() {
            // addExpr ((SHIFTLEFT | SHIFTRIGHT) addExpr)*
            return ParseChainedOperator(ParseAdditionExpr, BuildTokenTypeAcceptFunction(TokenType.SHIFTLEFT, TokenType.SHIFTRIGHT));
        }

        private ExpressionNode ParseAdditionExpr() {
            // mulExpr ((PLUS | MINUS) mulExpr)*
            return ParseChainedOperator(ParseMutliplicationExpr, BuildTokenTypeAcceptFunction(TokenType.PLUS, TokenType.MINUS));
        }

        private ExpressionNode ParseMutliplicationExpr() {
            // unaryExpr ((MUL | DIV) unaryExpr)*
            return ParseChainedOperator(ParseUnaryExpr, BuildTokenTypeAcceptFunction(TokenType.MUL, TokenType.DIV));
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
            // TODO: Refactor for types and vars
            if(_lexer.LookAhead.IsNumber()) {
                var num = _lexer.Advance();
                return new LiteralNode(double.Parse(num.Value!));
            }else if(_lexer.Accept(TokenType.LPAREN)) {
                var expr = ParseExpression();
                _lexer.Expect(TokenType.RPAREN);
                return expr;
            }
            throw new NotImplementedException("Vars not implemented yet!");
        }

    }
}
