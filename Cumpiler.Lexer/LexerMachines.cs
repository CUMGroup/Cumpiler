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
            AddKeywordMachine("<<", TokenType.SHIFTLEFT);
            AddKeywordMachine(">>", TokenType.SHIFTRIGHT);
            AddKeywordMachine("==", TokenType.EQUAL);
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

            AddKeywordMachine("PRINT", TokenType.PRINT);
            AddKeywordMachine("VAR", TokenType.VAR);
            AddKeywordMachine("CONST", TokenType.CONST);
            AddKeywordMachine("IF", TokenType.IF);
            AddKeywordMachine("ELSE", TokenType.ELSE);
            AddKeywordMachine("WHILE", TokenType.WHILE);
            AddKeywordMachine("DO", TokenType.DO);
            AddKeywordMachine("FOR", TokenType.FOR);
            AddKeywordMachine("BREAK", TokenType.BREAK);
            AddKeywordMachine("SWITCH", TokenType.SWITCH);
            AddKeywordMachine("CASE", TokenType.CASE);
            AddKeywordMachine("FUN", TokenType.FUN);
            AddKeywordMachine("RETURN", TokenType.RETURN);
            AddKeywordMachine("DEFAULT", TokenType.DEFAULT);

            AddKeywordMachine("TRUE", TokenType.TRUE);
            AddKeywordMachine("FALSE", TokenType.FALSE);

            AddMachine(new IdentifierMachine());
        }
    }
}
