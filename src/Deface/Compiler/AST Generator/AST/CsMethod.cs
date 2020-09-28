using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator.AST
{
    /// <summary>
    /// C# method declaration
    /// </summary>
    class CsMethod : CsAst
    {
        /// <summary>
        /// Gets / sets the name of the method
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets / sets the parameters of the method
        /// </summary>
        public List<CsParameter> Parameters { get; set; }

        /// <summary>
        /// Gets / sets the body of the method
        /// </summary>
        public List<CsAst> Body { get; set; }
    }
}
