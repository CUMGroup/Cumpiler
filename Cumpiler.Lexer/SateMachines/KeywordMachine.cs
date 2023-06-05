using Cumpiler.Lexer.Common.Abstract;
using Cumpiler.Lexer.Common.Tokens;

namespace Cumpiler.Lexer.SateMachines {
    internal class KeywordMachine : StateMachine {

        private readonly string _keyword;
        private readonly TokenType _type;

        public KeywordMachine(string keyword, TokenType type) {
            _keyword = keyword;
            _type = type;
        }

        public override TokenType GetTokenType() {
            return _type;
        }

        protected override void InitStateTable() {
            _start = new State("start", false);
            var currentState = _start;
            for(int i = 0; i < _keyword.Length; ++i) {
                var name = _keyword[..(i + 1)];
                var state = new State(name, i == _keyword.Length - 1);
                currentState.AddTransition(state, _keyword[i]);
                currentState = state;
                AddState(state);
            }
        }
    }
}
