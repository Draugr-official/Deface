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
        SingularData
    }
}
