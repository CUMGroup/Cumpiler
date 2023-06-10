using Cumpiler.Lexer.SateMachines.Numbers;
using Cumpiler.Lexer.SateMachines;
using Cumpiler.Lexer.SateMachines.Characters;
using Cumpiler.Lexer.Common.Tokens;
using Cumpiler.Lexer.SateMachines.Comments;

namespace Cumpiler.Lexer {
    internal partial class Lexer {

        internal void AddLexerMachines() {
            AddMachine(new IntegerMachine());
            AddMachine(new DoubleMachine());
            AddMachine(new FloatMachine());
            AddMachine(new StringLiteralMachine());
            AddMachine(new CharacterLiteralMachine());
            AddKeywordMachine("*", TokenType.MUL);
            AddKeywordMachine("/", TokenType.DIV);
            AddKeywordMachine("+", TokenType.PLUS);
            AddKeywordMachine("-", TokenType.MINUS);
            AddKeywordMachine("%", TokenType.MOD);
            AddKeywordMachine("&", TokenType.BITAND);
            AddKeywordMachine("|", TokenType.BITOR);
            AddKeywordMachine("^", TokenType.BITXOR);
            AddKeywordMachine("~", TokenType.BITCOMPLEMENT);
            AddKeywordMachine("<<", TokenType.SHIFTLEFT);
            AddKeywordMachine(">>", TokenType.SHIFTRIGHT);
            AddKeywordMachine("==", TokenType.EQUAL);
            AddKeywordMachine("!=", TokenType.NOTEQUAL);
            AddKeywordMachine("<", TokenType.LESS);
            AddKeywordMachine(">", TokenType.GREATER);
            AddKeywordMachine("<=", TokenType.LESSEQUAL);
            AddKeywordMachine(">=", TokenType.GREATEREQUAL);
            AddKeywordMachine("!", TokenType.NOT);
            AddKeywordMachine("&&", TokenType.AND);
            AddKeywordMachine("||", TokenType.OR);
            AddKeywordMachine("?", TokenType.QUESTIONMARK);
            AddKeywordMachine(":", TokenType.DOUBLECOLON);
            AddKeywordMachine("(", TokenType.LPAREN);
            AddKeywordMachine(")", TokenType.RPAREN);
            AddKeywordMachine("{", TokenType.LBRACE);
            AddKeywordMachine("}", TokenType.RBRACE);
            AddKeywordMachine("[", TokenType.LBRACKET);
            AddKeywordMachine("]", TokenType.RBRACKET);
            AddMachine(new LineCommentMachine());
            AddMachine(new WhitespaceMachine());
            AddKeywordMachine(";", TokenType.SEMICOLON);
            AddKeywordMachine(",", TokenType.COMMA);
            AddKeywordMachine("=", TokenType.ASSIGN);
            
            AddKeywordMachine("print", TokenType.PRINT);
            AddKeywordMachine("var", TokenType.VAR);
            AddKeywordMachine("const", TokenType.CONST);
            AddKeywordMachine("if", TokenType.IF);
            AddKeywordMachine("else", TokenType.ELSE);
            AddKeywordMachine("while", TokenType.WHILE);
            AddKeywordMachine("do", TokenType.DO);
            AddKeywordMachine("for", TokenType.FOR);
            AddKeywordMachine("break", TokenType.BREAK);
            AddKeywordMachine("switch", TokenType.SWITCH);
            AddKeywordMachine("case", TokenType.CASE);
            AddKeywordMachine("fun", TokenType.FUN);
            AddKeywordMachine("return", TokenType.RETURN);
            AddKeywordMachine("default", TokenType.DEFAULT);

            AddKeywordMachine("true", TokenType.TRUE);
            AddKeywordMachine("false", TokenType.FALSE);

            AddMachine(new IdentifierMachine());
        }
    }
}
