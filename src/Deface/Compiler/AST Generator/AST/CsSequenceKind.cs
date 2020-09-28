using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator.AST
{
    /// <summary>
    /// Sequentual C# kinds (e.g 'string', 'array' ..)
    /// </summary>
    public enum CsSequenceKind : byte
    {
        /// <summary>
        /// C# kind 'string'
        /// </summary>
        String,

        /// <summary>
        /// C# kind 'array'
        /// </summary>
        Array,

        /// <summary>
        /// C# kind 'list'
        /// </summary>
        List
    }
}
