using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.Code_Generator.Props
{
    /// <summary>
    /// x86 data declaration, uninitialized
    /// </summary>
    class x86DataDeclaration_UNI
    {
        /// <summary>
        /// Gets / sets the name of the declaration
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets / sets the define directive of the declaration
        /// </summary>
        public x86ReserveDirectives ReserveDirective { get; set; }

        /// <summary>
        /// Gets / sets the amount of bytes (Size * define directive byte count)
        /// </summary>
        public int Size { get; set; }
    }
}
