
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

        internal TypeSymbol(string name) 
            : base(name) {}

        public static TypeSymbol? TypeExists(string name) {
            if (name == None.Name)
                return None;
            if (name == Error.Name)
                return Error;
            if (name == Bool.Name)
                return Bool;
            if (name == Int.Name)
                return Int;
            if (name == Double.Name)
                return Double;
            if (name == Float.Name)
                return Float;
            if (name == String.Name)
                return String;
            if (name == Char.Name)
                return Char;

            return null;
        }

        public override SymbolKind Kind => SymbolKind.TYPE;
    }
}
