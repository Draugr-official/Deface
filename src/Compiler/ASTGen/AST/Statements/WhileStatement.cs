using DefaceCompiler.Compiler.ASTGen.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Statements
{
    /// <summary>
    /// C# while loop statement (E.g 'while(3 == 4) { ... }')
    /// </summary>
    class WhileStatement : Statement
    {
        public Expression Condition { get; set; }

        public Statement Body { get; set; }
    }
}
