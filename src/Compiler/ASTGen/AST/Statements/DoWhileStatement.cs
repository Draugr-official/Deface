using DefaceCompiler.Compiler.ASTGen.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Statements
{
    /// <summary>
    /// C# do-while statement (E.g 'do { ... } while(3 == 4)')
    /// </summary>
    class DoWhileStatement : Statement
    {
        public Expression Condition { get; set; }

        public Statement Body { get; set; }
    }
}
