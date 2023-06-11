
namespace Cumpiler.Syntax.Symbols {
    internal class TypeSymbol : Symbol {

        public static readonly TypeSymbol None = new TypeSymbol("?None");

        public static readonly TypeSymbol Error = new TypeSymbol("?Error");
        public static readonly TypeSymbol Bool = new TypeSymbol("bool");
        public static readonly TypeSymbol Int = new TypeSymbol("int");
        public static readonly TypeSymbol Double = new TypeSymbol("double");
        public static readonly TypeSymbol Float = new TypeSymbol("float");
        public static readonly TypeSymbol String = new TypeSymbol("string");
        public static readonly TypeSymbol Char = new TypeSymbol("char");


        private TypeSymbol(string name) 
            : base(name) {}

        public override SymbolKind Kind => SymbolKind.TYPE;
    }
}
