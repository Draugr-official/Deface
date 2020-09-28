using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator.AST
{
    class CsData : CsAst
    {
        /// <summary>
        /// Gets / sets the type associated with the data
        /// </summary>
        public CsType Type { get; set; }

        /// <summary>
        /// Gets / sets the data property
        /// </summary>
        public object Data { get; set; }

        public CsData() =>
            AstKind = CsAstKind.SingularData;
    }
}
