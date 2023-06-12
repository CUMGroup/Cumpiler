
using Cumpiler.Lexer.Common.Exceptions;
using Cumpiler.Lexer.Common.Text;
using Cumpiler.Lexer.Common.Tokens;
using System.Diagnostics.CodeAnalysis;

namespace Cumpiler.Lexer.Diagnostics {
    public static class DiagnosticsBag {

        public static MultilineInputReader? Input { get; set; }

        [DoesNotReturn]
        public static void ThrowCompilerException(string reason, string? expected, TokenPos? pos = null) {
            throw CreateCompilerException(reason, expected, pos);
        }

        public static CompilerException CreateCompilerException(string reason, string? expected, TokenPos? pos) {
            if (Input is null) {
                throw new ArgumentNullException(nameof(Input), "Diagnostic Input is not defined!");
            }
            string reasonMsg;
            if (pos is null) {
                reasonMsg = $"{reason}\nat line {Input?.LinePos ?? 0}\n{Input?.GetMarkedCodeSnippetAtCurrentPos() ?? ""}";
            }else {
                reasonMsg = $"{reason}\nat line {pos.Value.FirstLine}\n{Input?.GetMarkedCodeSnippet(pos.Value.FirstLine, pos.Value.FirstCol, pos.Value.LastLine, pos.Value.LastCol + 1) ?? ""}";
            }
            string expectedMsg = !string.IsNullOrWhiteSpace(expected) ? "Expected: " + expected : string.Empty;
            return new CompilerException($"{reasonMsg}\n{expectedMsg}");
        }

    }
}
