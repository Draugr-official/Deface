using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.Code_Generator.Props
{
    /// <summary>
    /// x86 reserve directives for uninitialized data
    /// </summary>
    public enum x86ReserveDirectives
    {
        /// <summary>
        /// x86 'reserve byte', 1 byte
        /// </summary>
        resb,

        /// <summary>
        /// x86 'reserve word', 2 byte
        /// </summary>
        resw,

        /// <summary>
        /// x86 'reserve dword', 4 byte
        /// </summary>
        resd,

        /// <summary>
        /// x86 'reserve qword', 8 byte
        /// </summary>
        resq,

        /// <summary>
        /// x86 'reserve ten bytes', 10 byte
        /// </summary>
        rest,
    }
}
