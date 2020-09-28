using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator.AST
{
    class CsType
    {
        /// <summary>
        /// Gets / sets the TypeToken of the type
        /// </summary>
        public CsTypeKind TypeKind { get; set; }

        /// <summary>
        /// If available, gets / sets the primitive kind of the type
        /// </summary>
        public CsPrimitiveKind PrimitiveKind { get; set; }

        /// <summary>
        /// If available, gets / sets the sequentual kind of the type
        /// </summary>
        public CsSequenceKind SequenceKind { get; set; }
    }
}
