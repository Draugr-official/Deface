using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.Code_Generator.Props
{
    /// <summary>
    /// x86 instruction
    /// </summary>
    class x86Instruction
    {
        /// <summary>
        /// Gets / sets the mnemonic of the instruction
        /// </summary>
        public x86Mnemonics Mnemonic { get; set; }

        /// <summary>
        /// Gets / sets the first operand of the instruction
        /// </summary>
        public object Operand1 { get; set; }

        /// <summary>
        /// Gets / sets the second operand of the instruction
        /// </summary>
        public object Operand2 { get; set; }
    }
}
