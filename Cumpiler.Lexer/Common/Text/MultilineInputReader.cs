using System.Text;

namespace Cumpiler.Lexer.Common.Text {
    public class MultilineInputReader {

        private string[] _lines;
        public int LinePos { get; private set; }
        public int ColumnPos { get; private set; }

        public MultilineInputReader(string input) : this(input.Replace("\r\n", "\n").Split("\n")) {}

        public MultilineInputReader(string[] lines) {
            _lines = lines.Select(x => x + "\n").ToArray();
        }

        public bool IsEmpty() => LinePos >= _lines.Length;

        public char CurrectChar() => IsEmpty() ? '\0' : _lines[LinePos][ColumnPos];

        public string CurrentLine() => IsEmpty() ? "" : _lines[LinePos];

        public void Advance() {
            if(IsEmpty()) 
                return;

            ++ColumnPos;

            if(ColumnPos >= CurrentLine().Length) {
                ++LinePos;
                ColumnPos = 0;
            }
        }

        public string GetRemaining() {
            var remainingSb = new StringBuilder();

            if(LinePos < _lines.Length) {
                remainingSb.Append(_lines[LinePos].Substring(ColumnPos));
            }
            for(int curLine = LinePos + 1; curLine < _lines.Length; ++curLine) {
                remainingSb.Append(_lines[curLine]);
            }
            return remainingSb.ToString();
        }

        public string AdvanceAndGet(int cnt) {
            var sb = new StringBuilder();
            for(int i = 0; i < cnt; ++i) {
                sb.Append(CurrectChar());
                Advance();
            }
            return sb.ToString();
        }

        public string GetMarkedCodeSnippetAtCurrentPos() {
            return GetMarkedCodeSnippet(LinePos, ColumnPos, LinePos, ColumnPos + 1);
        }

        public string GetMarkedCodeSnippet(int startLine, int startCol, int endLine, int endCol) {
            if(startLine >= _lines.Length) {
                return "<EOF>";
            }
            var codeSnippetSb = new StringBuilder();

            for(int curLine = startLine; curLine <= endLine; ++curLine) {
                string line = _lines[curLine];
                codeSnippetSb.Append(line);

                for(int curCol = 0; curCol < line.Length; ++curCol) {
                    bool beforeBegin = (curLine == startLine && curCol < startCol);
                    bool afterEnd = (curLine == endLine && curCol >= endCol);
                    if(beforeBegin || afterEnd) {
                        codeSnippetSb.Append(' '); 
                    }else {
                        codeSnippetSb.Append('^');
                    }
                }
                codeSnippetSb.Append('\n');
            }
            return codeSnippetSb.ToString();
        }
    }
}
