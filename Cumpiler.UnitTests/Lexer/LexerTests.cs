﻿
using Cumpiler.Lexer;
using Cumpiler.Lexer.Common.Tokens;

namespace Cumpiler.UnitTests.Lexer {
    public class LexerTests {

        [Fact]
        public void Lexer_MatchesKeywordsBeforeIdentifier() {
            var lexer = LexerFactory.CreateLexer("IF FUN heLoWorld wasGeht 1");

            var tokens = lexer.AdvanceTillEOF();

            var expected = new List<TokenType> {
                TokenType.IF, TokenType.FUN, TokenType.IDENTIFIER, TokenType.IDENTIFIER, TokenType.INTEGER, TokenType.EOF
            };

            Assert.Equal(tokens.Select(e => e.Type).ToList(), expected);
        }

    }
}
