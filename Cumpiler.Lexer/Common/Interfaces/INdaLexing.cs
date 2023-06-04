using Cumpiler.Lexer.Common.Abstract;
using Cumpiler.Lexer.Common.Tokens;

namespace Cumpiler.Lexer.Common.Interfaces {
    internal interface INdaLexing {

        void AddKeywordMachine(string keyword, TokenType type);

        void AddMachine(StateMachine machine);


        void InitMachines(string input);

        Token NextWord();

    }
}
