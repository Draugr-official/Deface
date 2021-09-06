using DefaceCompiler.Compiler.ASTGen.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Statements
{
    /// <summary>
    /// C# assignment statement (E.g 'aNum = 42')
    /// </summary>
    class AssignmentStatement : Statement
    {
        public string Name { get; set; }

        public Expression Value { get; set; }


        public bool AccessingIndex { get; set; }

        public Expression Index { get; set; }
    }
}
