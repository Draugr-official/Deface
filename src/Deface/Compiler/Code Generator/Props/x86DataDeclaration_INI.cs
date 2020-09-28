using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.Code_Generator.Props
{
    /// <summary>
    /// x86 data declaration, initialized
    /// </summary>
    class x86DataDeclaration_INI
    {
        /// <summary>
        /// Gets / sets the label of the declaration
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets / sets the define directive of the declaration
        /// </summary>
        public x86DefineDirectives DefineDerective { get; set; }

        /// <summary>
        /// Gets / sets the initial value of the declaration
        /// </summary>
        public object InitialValue { get; set; }
    }
}
