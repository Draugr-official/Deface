using DefaceCompiler.Compiler.ASTGen.AST.Expressions;
using DefaceCompiler.Compiler.ASTGen.AST.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Statements
{
    /// <summary>
    /// Local declaration expression (E.g 'string shortString = "Hello"')
    /// </summary>
    class LocalDeclarationStatement : Statement
    {
        public string Name { get; set; }

        public Expression Value { get; set; }

        public DataType DataType { get; set; }
    }
}
