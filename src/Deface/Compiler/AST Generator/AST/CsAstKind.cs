using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator.AST
{
    /// <summary>
    /// C# ast kinds
    /// </summary>
    public enum CsAstKind
    {
        /// <summary>
        /// C# ast kind 'call'
        /// </summary>
        Call,

        /// <summary>
        /// C# ast kind 'singular data' (e.g '"abc"', '123')
        /// </summary>
        SingularData,

        /// <summary>
        /// C# ast kind 'conditional statement' (e.g 'if(true){}')
        /// </summary>
        ConditionalStatement,

        /// <summary>
        /// C# ast kind 'pass eq' (e.g '==')
        /// </summary>
        PassEq,

        /// <summary>
        /// C# ast kind 'binary expression' (e.g '3 == 4')
        /// </summary>
        BinaryExpr,
    }
}
