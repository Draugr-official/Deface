using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator.AST
{
    class CsField : CsAst
    {
        /// <summary>
        /// Gets / sets the name of the field
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets / sets the primitive type of the field
        /// </summary>
        public CsPrimitiveKind PrimitiveType { get; set; }
    }
}
