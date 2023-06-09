﻿namespace Cumpiler.Lexer.Common.Tokens {
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

        CAST,

        // Logic operators
        EQUAL,
        NOTEQUAL,
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
        BITXOR,
        BITCOMPLEMENT,
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
        DEFAULT,

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
