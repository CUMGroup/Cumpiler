
namespace Cumpiler.Syntax.Symbols {
    internal abstract class Symbol {

        public string Name { get; }

        public abstract SymbolKind Kind { get; }

        public override string ToString() => Name;

        private protected Symbol(string name) {
            Name = name;
        }
    }
}
