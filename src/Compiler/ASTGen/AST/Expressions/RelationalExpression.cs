using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Expressions
{
    /// <summary>
    /// C# relational expression (E.g '3 == 4')
    /// </summary>
    class RelationalExpression : Expression
    {
        public Expression Left { get; set; }

        public RelationalInfix Infix { get; set; }

        public Expression Right { get; set; }
    }

    /// <summary>
    /// Relational infix operators
    /// </summary>
    public enum RelationalInfix
    {
        /// <summary>
        /// E.g '=='
        /// </summary>
        Equals,

        /// <summary>
        /// E.g '>'
        /// </summary>
        Bigger,

        /// <summary>
        /// E.g '<'
        /// </summary>
        Smaller,

        /// <summary>
        /// E.g '>='
        /// </summary>
        EqualsBigger,

        /// <summary>
        /// E'g '<='
        /// </summary>
        EqualsSmaller
    }
}
