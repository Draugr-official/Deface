using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator.AST
{
    /// <summary>
    /// C# call to method
    /// </summary>
    class CsCall : CsAst
    {
        /// <summary>
        /// Gets / sets the name of the method
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets / sets the arguments of the method
        /// </summary>
        public List<CsData> Arguments { get; set; }

        public CsCall() =>
            AstKind = CsAstKind.Call;
    }
}
