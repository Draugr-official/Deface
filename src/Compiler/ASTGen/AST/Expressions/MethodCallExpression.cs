using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Expressions
{
    class MethodCallExpression : Expression
    {
        public string Name { get; set; }

        public ExpressionList Arguments { get; set; }
    }
}
