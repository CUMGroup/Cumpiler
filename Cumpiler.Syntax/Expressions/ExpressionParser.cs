﻿
using Cumpiler.Lexer.Common.Interfaces;
using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Lexer.Helpers;
using Cumpiler.Syntax.Nodes.Expressions;
using Cumpiler.Syntax.Nodes.Expressions.Arithmetic;
using Cumpiler.Syntax.Nodes.Expressions.Literals;
using System;
using System.Globalization;

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
                lhs = new BinaryOperatorNode(lhs, rhs, op.Type, op.Position);
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
                lhs = new BinaryOperatorNode(lhs, rhs, op.Type, op.Position);
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
            var lhs = ParseLogicOrExpr();
            var token = _lexer.LookAhead;
            if(_lexer.Accept(TokenType.QUESTIONMARK)) {
                var trueExpr = ParseLogicOrExpr();
                _lexer.Expect(TokenType.DOUBLECOLON);
                var falseExpr = ParseLogicOrExpr();
                lhs = new TernaryOperatorNode(lhs, trueExpr, falseExpr, token.Position);
            }
            return lhs;
        }

        private ExpressionNode ParseLogicOrExpr() {
            // logicAndExpr (LOGICOR logicAndExpr)*
            return ParseChainedOperator(ParseLogicAndExpr, BuildTokenTypeAcceptFunction(TokenType.OR));
        }

        private ExpressionNode ParseLogicAndExpr() {
            // bitOrExpr (LOGICAND bitOrExpr)*
            return ParseChainedOperator(ParseEqualityExpr, BuildTokenTypeAcceptFunction(TokenType.AND));
        }

        private ExpressionNode ParseEqualityExpr() {
            // logicOrExpr (EQUAL logicOrExpr)?
            return ParseSingleOperator(ParseBitOrExpr, BuildTokenTypeAcceptFunction(TokenType.EQUAL, TokenType.NOTEQUAL));
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
            // (PLUS | MINUS | NOT | COMPLEMENT | CAST)? literalExpr
            if(_lexer.Accept(TokenType.PLUS)) {
                // nop
                return ParseLiteralExpr();
            }else if(_lexer.LookAhead.Type is TokenType.MINUS or TokenType.NOT or TokenType.BITCOMPLEMENT) {
                var op = _lexer.Advance();
                var rhs = ParseLiteralExpr();
                return new UnaryOperatorNode(rhs, op.Type, op.Position);
            }else if(_lexer.LookAhead.Type is TokenType.CAST) {
                var op = _lexer.Advance();
                var rhs = ParseLiteralExpr();
                return new UnaryOperatorNode(rhs, op.Type, op.Value![1..^1], op.Position);
            }
            return ParseLiteralExpr();
        }

        private ExpressionNode ParseLiteralExpr() {
            // TODO: Refactor for types and vars
            var num = _lexer.LookAhead;
            if (_lexer.Accept(TokenType.INTEGER)) {
                return new LiteralNode(int.Parse(num.Value!), num.Position);
            } else if (_lexer.Accept(TokenType.DOUBLE)) {
                return new LiteralNode(double.Parse(num.Value!, CultureInfo.InvariantCulture), num.Position);
            } else if (_lexer.Accept(TokenType.FLOAT)) {
                return new LiteralNode(float.Parse(num.Value!, CultureInfo.InvariantCulture), num.Position);
            } else if (_lexer.Accept(TokenType.CHAR)) {
                return new LiteralNode(char.Parse(num.Value![1..^1]), num.Position);
            } else if (_lexer.Accept(TokenType.STRING)) { 
                return new LiteralNode(num.Value![1..^1], num.Position);
            } else if(_lexer.Accept(TokenType.LPAREN)) {
                var expr = ParseExpression();
                _lexer.Expect(TokenType.RPAREN);
                return expr;
            }else if(_lexer.LookAhead.Type is TokenType.TRUE or TokenType.FALSE) {
                var lit = _lexer.Advance();
                return new LiteralNode(bool.Parse(lit.Value!), lit.Position);
            }
            throw new NotImplementedException("Vars not implemented yet!");
        }

    }
}
