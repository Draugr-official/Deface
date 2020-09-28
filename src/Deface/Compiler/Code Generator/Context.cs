using Compilerator.Deface.Compiler.Code_Generator.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.Code_Generator
{
    /// <summary>
    /// x86 application context
    /// </summary>
    class Context
    {
        /// <summary>
        /// Text section containing x86 instructions
        /// </summary>
        public List<x86Instruction> SectionText { get; set; }

        /// <summary>
        /// Data section containing initialized data
        /// </summary>
        public List<x86DataDeclaration_INI> SectionData { get; set; }

        /// <summary>
        /// Bss section containing uninitialized data
        /// </summary>
        public List<x86DataDeclaration_UNI> SectionBss { get; set; }

        /// <summary>
        /// Context constructor
        /// </summary>
        public Context()
        {
            SectionText = new List<x86Instruction>();
            SectionData = new List<x86DataDeclaration_INI>();
            SectionBss  = new List<x86DataDeclaration_UNI>();
        }
    }
}
