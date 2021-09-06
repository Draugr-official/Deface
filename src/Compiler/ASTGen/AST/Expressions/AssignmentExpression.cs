using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Expressions
{
    /// <summary>
    /// C# assignment expression (E.g 'aNum = 42')
    /// </summary>
    class AssignmentExpression : Expression
    {
        public string Name { get; set; }

        public Expression Value { get; set; }

        public AssignmentInfix Infix { get; set; }


        public bool AccessingIndex { get; set; }

        public Expression Index { get; set; }
    }

    public enum AssignmentInfix
    {
        Equals,
        PlusEquals,
        MinusEquals,
        MultiplyEquals,
        DivideEquals,
    }
}
