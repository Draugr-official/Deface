using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator.AST
{
    /// <summary>
    /// Primitive C# kinds (e.g 'int', 'bool')
    /// </summary>
    public enum CsPrimitiveKind : byte
    {
        /// <summary>
        /// C# kind 'void'
        /// </summary>
        Void,

        /// <summary>
        /// C# kind 'char'
        /// </summary>
        Char,

        /// <summary>
        /// C# logical 'boolean'
        /// </summary>
        Bool,

        /// <summary>
        /// C# integral 'byte'
        /// </summary>
        Byte,

        /// <summary>
        /// C# integral 'sbyte'
        /// </summary>
        Sbyte,

        /// <summary>
        /// C# integral 'short'
        /// </summary>
        Short,

        /// <summary>
        /// C# integral 'unsigned short'
        /// </summary>
        Ushort,

        /// <summary>
        /// C# integral 'int'
        /// </summary>
        Int,

        /// <summary>
        /// C# integral 'unsigned int'
        /// </summary>
        Uint,

        /// <summary>
        /// C# integral 'long'
        /// </summary>
        Long,

        /// <summary>
        /// C# integral 'unsigned long'
        /// </summary>
        Ulong,

        /// <summary>
        /// C# floating point 'float'
        /// </summary>
        Float,

        /// <summary>
        /// C# floating point 'double'
        /// </summary>
        Double,

        /// <summary>
        /// C# numeric 'decimal'
        /// </summary>
        Decimal,
    }
}
