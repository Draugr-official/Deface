using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.Code_Generator.Props
{
    /// <summary>
    /// x86 data encoding
    /// </summary>
    public enum x86Encoding
    {
        /// <summary>
        /// x86 default encoding (e.g '1', '2' .. )
        /// </summary>
        Default,

        /// <summary>
        /// x86 binary encoding (e.g '111010b')
        /// </summary>
        Binary,

        /// <summary>
        /// x86 hexadecimal encoding (e.g 'A4h', '0xA4')
        /// </summary>
        Hexadecimal,

        /// <summary>
        /// x86 octal encoding (e.g '17o')
        /// </summary>
        Octal,

        /// <summary>
        /// x86 ascii encoding (e.g '"Hello"')
        /// </summary>
        ASCII
    }
}
