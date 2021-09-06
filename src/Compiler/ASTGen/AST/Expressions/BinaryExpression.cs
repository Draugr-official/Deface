using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Expressions
{
    class BinaryExpression : Expression
    {
        public Expression Left { get; set; }

        public Expression Right { get; set; }

        public BinaryInfix Operator { get; set; }
    }

    public enum BinaryInfix
    {
        Add = '+',
        Sub = '-',
        Mul = '*',
        Div = '/'
    }
}
