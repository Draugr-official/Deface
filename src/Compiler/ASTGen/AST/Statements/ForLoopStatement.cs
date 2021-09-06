using DefaceCompiler.Compiler.ASTGen.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Statements
{
    /// <summary>
    /// C# for loop statement (E.g 'for(int i = 0; i < 3; i++) { ... }')
    /// </summary>
    class ForLoopStatement : Statement
    {
        public Expression Initializer { get; set; }

        public Expression Condition { get; set; }

        public Expression Counter { get; set; }

        public Statement Body { get; set; }
    }
}
