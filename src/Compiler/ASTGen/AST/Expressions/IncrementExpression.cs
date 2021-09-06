using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Expressions
{
    /// <summary>
    /// C# increment expression (E.g 'a++')
    /// </summary>
    class IncrementExpression : Expression
    {
        public string Name { get; set; }
        
        public FixType FixType { get; set; } 
    }

    public enum FixType
    {
        /// <summary>
        /// E.g '++a'
        /// </summary>
        PreFix,

        /// <summary>
        /// E.g 'a++'
        /// </summary>
        PostFix
    }
}
