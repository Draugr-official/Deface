using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.Code_Generator.Props
{
    /// <summary>
    /// x86 asm mnemonics
    /// </summary>
    public enum x86Mnemonics
    {
        /// <summary>
        /// x86 mnemonic 'mov'
        /// </summary>
        mov,

        /// <summary>
        /// x86 mnemonic 'call'
        /// </summary>
        call,

        /// <summary>
        /// x86 mnemonic 'int'
        /// </summary>
        _int,

        /// <summary>
        /// x86 mnemonic 'add'
        /// </summary>
        add,

        /// <summary>
        /// x86 mnemonic 'sub'
        /// </summary>
        sub,

        /// <summary>
        /// Non-mnemonic label declaration
        /// </summary>
        label,
    }
}
