using DefaceCompiler.Compiler.ASTGen.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Statements
{
    /// <summary>
    /// C# if conditional statement (E.g 'if(3 == 4) { ... }')
    /// </summary>
    class IfStatement : Statement
    {
        public Expression Condition { get; set; }

        public Statement TrueStatement { get; set; }

        public Statement FalseStatement { get; set; }

        #nullable enable
        public Statement? ElseClause { get; set; }
        #nullable disable
    }
}
