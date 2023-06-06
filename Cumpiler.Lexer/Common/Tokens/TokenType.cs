namespace Cumpiler.Lexer.Common.Tokens {
    public enum TokenType {
        EOF,
        IDENTIFIER,
        
        // Numbers
        INTEGER,
        FLOAT,
        DOUBLE,

        // Strings
        STRING,
        CHAR,

        // Bools
        TRUE,
        FALSE,

        // ()[]{}
        LPAREN,
        RPAREN,
        LBRACKET,
        RBRACKET,
        LBRACE,
        RBRACE,

        // Operators
        PLUS,
        MINUS,
        MUL,
        DIV,
        MOD,

        // Logic operators
        EQUAL,
        LESS,
        GREATER,
        LESSEQUAL,
        GREATEREQUAL,
        NOT,
        AND,
        OR,
        QUESTIONMARK,
        DOUBLECOLON,

        // Bit operators
        BITAND,
        BITOR,
        SHIFTLEFT,
        SHIFTRIGHT,

        // Other Operators
        ASSIGN,

        // Keywords
        VAR,
        CONST,
        PRINT,

        // Structures
        IF,
        ELSE,
        WHILE,
        DO,
        FOR,
        BREAK,
        SWITCH,
        CASE,

        FUN,
        RETURN,
        
        // Ignored text
        LINECOMMENT,
        MULTILINECOMMENT,
        WHITESPACE,

        // Misc
        SEMICOLON,
        COMMA,

    }
}
