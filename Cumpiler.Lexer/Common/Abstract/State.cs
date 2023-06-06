namespace Cumpiler.Lexer.Common.Abstract {
    internal class State {

        public bool IsFinalState { get; private init; }

        private readonly Dictionary<char, State> _transitions;

        public State() : this(false) { }


        public State(bool isFinalState) {
            this.IsFinalState = isFinalState;
            this._transitions = new Dictionary<char, State>();
        }

        public State AddTransition(State targetState, char terminal) {
            _transitions.Add(terminal, targetState);
            return this;
        }

        public State AddTransition(State targetState, params char[] terminals) {
            foreach(var term in terminals) {
                AddTransition(targetState, term);
            }
            return this;
        }

        public State AddTransitionRange(State targetState, char first, char last) {
            if(first > last) {
                throw new ArgumentException("First cannot be bigger than last");
            }
            for(char i = first; i <= last; ++i) {
                AddTransition(targetState, i);
            }
            return this;
        }

        public State? GetTransition(char terminal) {
            _transitions.TryGetValue(terminal, out State? transition);
            return transition;
        }
    }
}
