namespace Cumpiler.Lexer.Common.Tokens {

    public readonly struct TokenPos {
        public int FirstLine { get; init; }
        public int LastLine { get; init; }

        public int FirstCol { get; init; }
        public int LastCol { get; init; }

    }

    public class Token {

        public required TokenType Type {  get; init; }

        public string? Value { get; init; }

        public required TokenPos Position { get; init; }


        public override string ToString() {
            return $"{Type} {Value}";
        }

        public static Token EOF() {
            return EOF(0, 0);
        }

        public static Token EOF(int linePos, int colPos) {
            return new Token {
                Type = TokenType.EOF,
                Value = string.Empty,
                Position = new TokenPos {
                    FirstCol = colPos,
                    FirstLine = linePos,
                    LastCol = colPos,
                    LastLine = linePos,
                }
            };
        }
    }
}
