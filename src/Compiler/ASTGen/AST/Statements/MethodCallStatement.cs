using DefaceCompiler.Compiler.ASTGen.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Statements
{
    class MethodCallStatement : Statement
    {
        public string Name { get; set; }

        public ExpressionList Arguments { get; set; }
    }
}
