using DefaceCompiler.Compiler.ASTGen.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Statements
{
    class ClassDeclarationStatement : Statement
    {
        public string Name { get; set; }
        
        public StatementList Body { get; set; }

        public ExpressionList Attributes { get; set; }
    }
}
