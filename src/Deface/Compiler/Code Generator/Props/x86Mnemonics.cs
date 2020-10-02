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
        /// x86 mnemonic 'mov', move
        /// </summary>
        mov,

        /// <summary>
        /// x86 mnemonic 'call'
        /// </summary>
        call,

        /// <summary>
        /// x86 mnemonic 'int', interrupt
        /// </summary>
        _int,

        /// <summary>
        /// x86 mnemonic 'lea', load effective address
        /// </summary>
        lea,

        /// <summary>
        /// x86 mnemonic 'je', jump if equals
        /// </summary>
        je,

        /// <summary>
        /// x86 mnemonic 'jne', jump if not equals
        /// </summary>
        jne,

        /// <summary>
        /// x86 mnemonic 'rep'
        /// </summary>
        rep,

        /// <summary>
        /// x86 mnemonic 'add', addition
        /// </summary>
        add,

        /// <summary>
        /// x86 mnemonic 'sub', subtraction
        /// </summary>
        sub,

        /// <summary>
        /// Non-mnemonic label declaration
        /// </summary>
        label,
    }
}
