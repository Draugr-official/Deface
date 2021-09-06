using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Expressions
{
    /// <summary>
    /// C# decrement expression (E.g 'a--')
    /// </summary>
    class DecrementExpression : Expression
    {
        public string Name { get; set; }

        public FixType FixType { get; set; }
    }
}
