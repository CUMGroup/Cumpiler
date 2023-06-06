using Cumpiler.Lexer.Common.Abstract;
using Cumpiler.Lexer.Common.Tokens;

namespace Cumpiler.Lexer.SateMachines.Comments {
    internal class LineCommentMachine : StateMachine {
        public override TokenType GetTokenType() {
            return TokenType.LINECOMMENT;
        }

        protected override void InitStateTable() {
            _start = AddState(new State());
            var oneSlash = AddState(new State());
            var innerComment = AddState(new State());
            var end = AddState(new State(true));

            _start
                .AddTransition(oneSlash, '/');

            oneSlash
                .AddTransition(innerComment, '/');

            innerComment
                .AddTransitionRange(innerComment, ' ', '~')
                .AddTransition(innerComment, 'ä', 'ö', 'ü', 'ß')
                .AddTransition(end, '\n', '\0');
        }
    }
}
