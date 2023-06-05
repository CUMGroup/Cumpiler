
using Cumpiler.Lexer.Common.Info;
using Cumpiler.Lexer.Common.Text;
using Cumpiler.Lexer.Common.Tokens;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Cumpiler.Lexer.Common.Abstract {
    internal abstract class StateMachine {

        private readonly List<State> _stateList;

        private InputReader? _inputReader;
        public State? CurrentState { get; private set; }

        protected State? _start;

        public StateMachine() {
            _stateList = new List<State>();
        }

        protected abstract void InitStateTable();
        public abstract TokenType GetTokenType();

        protected State GetStartState() {
            return _start ?? throw new NullReferenceException("_start State not defined!");
        }

        protected State AddState(State state) {
            _stateList.Add(state);
            return state;
        }

        protected void AddState(params State[] states) {
            foreach(var state in states) {
                AddState(state);
            }
        }

        public void Init(string input) {
            InitStateTable();
            this._inputReader = new InputReader(input);
            CurrentState = GetStartState();
        }

        public void Step() {
            if(_inputReader == null) {
                throw new ArgumentException("Init Method was not called before stepping the StateMachine!");
            }
            if(CurrentState == null) { // Error State
                return;
            }
            var currentChar = _inputReader.CurrentChar();
            _inputReader.Advance();

            CurrentState = CurrentState.GetTransition(currentChar);
        }

        public void Process(string input) {
            Init(input);

            while(!IsFinished()) {
                Step();
            }
        }

        public bool IsInFinalState() {
            return CurrentState != null && CurrentState.IsFinalState;
        }

        public bool IsFinished() {
            return CurrentState == null || _inputReader == null || _inputReader.CurrentChar() == '\0';
        }
    }
}
