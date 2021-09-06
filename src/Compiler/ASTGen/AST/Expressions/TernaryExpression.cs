using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Expressions
{
    /// <summary>
    /// C# ternary expression (E.g '3 == 4 ? "Equal" : "Not equal"')
    /// </summary>
    class TernaryExpression : Expression
    {
        public Expression Condition { get; set; }

        public Expression TrueCase { get; set; }

        public Expression FalseCase { get; set; }
    }
}
