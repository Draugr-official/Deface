using DefaceCompiler.Compiler.ASTGen.AST.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Expressions
{
    /// <summary>
    /// Local declaration expression (E.g 'string shortString = "Hello"')
    /// </summary>
    class LocalDeclarationExpression : Expression
    {
        public string Name { get; set; }

        public Expression Value { get; set; }

        public DataType DataType { get; set; }
    }
}
