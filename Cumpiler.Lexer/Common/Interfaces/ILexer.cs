using Cumpiler.Lexer.Common.Exceptions;
using Cumpiler.Lexer.Common.Tokens;

namespace Cumpiler.Lexer.Common.Interfaces {
    public interface ILexer {

        void Init(string input);

        Token LookAhead { get; }

        Token Advance();

        Token Expect(TokenType type);

        bool Accept(TokenType type);

        void ThrowCompilerException(string reason, string? expected);

        CompilerException CreateCompilerException(string reason, string? expected);
    }
}
