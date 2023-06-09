﻿using Cumpiler.Lexer.Common.Exceptions;
using Cumpiler.Lexer.Diagnostics;
using Cumpiler.Syntax.Nodes.Expressions.Arithmetic;
using Cumpiler.Syntax.Nodes.Expressions.Literals;
using Cumpiler.Syntax.Operators;
using Cumpiler.Syntax.Symbols;
using Cumpiler.Syntax.Visitors.Abstract;

namespace Cumpiler.Syntax.Visitors {
    internal class AstTypeCheckVisitor : INodeVisitor {
        public void Visit(TernaryOperatorNode node) {
            node.Expr.AcceptVisitor(this);
            node.ExprTrue.AcceptVisitor(this);
            node.ExprFalse.AcceptVisitor(this);

            if(node.Expr.Type == TypeSymbol.Error || node.ExprTrue.Type == TypeSymbol.Error || node.ExprFalse.Type == TypeSymbol.Error) {
                node.Type = TypeSymbol.Error;
            }

            if(node.Expr.Type != TypeSymbol.Bool) {
                node.Type = TypeSymbol.Error;
                DiagnosticsBag.ThrowCompilerException("Expression is not of type Bool", "Condition must evaluate to Bool", node.Expr.TokenPos);
            }

            if(node.ExprTrue.Type != node.ExprFalse.Type) {
                node.Type = TypeSymbol.Error;
                DiagnosticsBag.ThrowCompilerException($"Found types {node.ExprTrue.Type}, {node.ExprFalse.Type}", "Types in ternary expression must match!", node.TokenPos);
            }

            node.Type = node.ExprTrue.Type;
        }

        public void Visit(BinaryOperatorNode node) {
            node.Lhs.AcceptVisitor(this);
            node.Rhs.AcceptVisitor(this);

            if (node.Lhs.Type == TypeSymbol.Error || node.Rhs.Type == TypeSymbol.Error) {
                node.Type = TypeSymbol.Error;
            }
            try {
                node.Type = BinaryOperators.GetTypeFromOperation(node.Operation, node.Lhs.Type, node.Rhs.Type);
            } catch(CompilerException ex) {
                DiagnosticsBag.ThrowCompilerException(ex.Message, null, node.TokenPos);
            }

        }

        public void Visit(UnaryOperatorNode node) {
            node.Arg.AcceptVisitor(this);
            if(node.Arg.Type == TypeSymbol.Error) {
                node.Type = TypeSymbol.Error;
            }
            try {
                node.Type = UnaryOperators.GetTypeFromOperation(node.Operation, node.ContentValue, node.Arg.Type);
            } catch(CompilerException ex) {
                DiagnosticsBag.ThrowCompilerException(ex.Message, null, node.TokenPos);
            }
        }

        public void Visit(LiteralNode node) {
            if(node.Type == TypeSymbol.None || node.Type == TypeSymbol.Error) {
                node.Type = TypeSymbol.Error;
                DiagnosticsBag.ThrowCompilerException("Cannot access type of literal expression", "Literal must have a type! Did the TypeCheck fail?", node.TokenPos);
            }
        }
    }
}
