using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Statements
{
    class CompoundStatement : Statement
    {
        public StatementList Statements { get; set; }
    }
}
