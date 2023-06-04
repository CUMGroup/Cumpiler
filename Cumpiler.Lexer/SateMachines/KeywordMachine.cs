using Cumpiler.Lexer.Common.Abstract;
using Cumpiler.Lexer.Common.Tokens;

namespace Cumpiler.Lexer.SateMachines {
    internal class KeywordMachine : StateMachine {

        public KeywordMachine(string keyword, TokenType type) {
            
        }

        public override TokenType GetTokenType() {
            throw new NotImplementedException();
        }

        protected override State GetStartState() {
            throw new NotImplementedException();
        }

        protected override void InitStateTable() {
            throw new NotImplementedException();
        }
    }
}
