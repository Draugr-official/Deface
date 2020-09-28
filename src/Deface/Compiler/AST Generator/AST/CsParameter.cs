using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator.AST
{
    /// <summary>
    /// C# method parameter
    /// </summary>
    public struct CsParameter
    {
        /// <summary>
        /// Gets / sets the name of the parameter
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets / sets the default value of the parameter
        /// </summary>
        public object InitValue { get; set; }

        /// <summary>
        /// Gets / sets the primitive type of the parameter
        /// </summary>
        public CsPrimitiveKind PrimitiveType { get; set; }
    }
}
