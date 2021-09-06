using DefaceCompiler.Compiler.ASTGen.AST.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Expressions
{
    class ConstantExpression : Expression
    {
        public object Value { get; set; }

        public DataType Type { get; set; }
    }
}
