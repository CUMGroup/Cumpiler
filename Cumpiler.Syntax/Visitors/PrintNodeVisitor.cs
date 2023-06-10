using Cumpiler.Syntax.Nodes.Expressions.Arithmetic;
using Cumpiler.Syntax.Nodes.Expressions.Literals;
using Cumpiler.Syntax.Visitors.Abstract;

namespace Cumpiler.Syntax;
internal class PrintNodeVisitor : INodeVisitor {

    private int _indentationLevel = 0;
    private TextWriter _outStream;

    public PrintNodeVisitor(TextWriter outStream) {
        _outStream = outStream;
    }

    // Cleanup
    ~PrintNodeVisitor() {
        _outStream?.Dispose();
    }

    private string GetIndentation() {
        return new string('\t', _indentationLevel);
    }

    private void WriteLine(string line) {
        _outStream.WriteLine(GetIndentation() + line);
    }

    public void Visit(TernaryOperatorNode node) {
        WriteLine("QUESTIONMARK");
        ++_indentationLevel;
        node.Expr.AcceptVisitor(this);
        node.ExprTrue.AcceptVisitor(this);
        node.ExprFalse.AcceptVisitor(this);
        --_indentationLevel;
    }

    public void Visit(BinaryOperatorNode node) {
        WriteLine(node.Operation.ToString());
        ++_indentationLevel;
        node.Lhs.AcceptVisitor(this);
        node.Rhs.AcceptVisitor(this);
        --_indentationLevel;
    }

    public void Visit(UnaryOperatorNode node) {
        WriteLine(node.Operation.ToString());
        ++_indentationLevel;
        node.Arg.AcceptVisitor(this);
        --_indentationLevel;
    }

    public void Visit(LiteralNode node) {
        WriteLine(node.Args.ToString());
    }
}
