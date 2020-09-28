using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator.AST
{
    /// <summary>
    /// C# type kinds
    /// </summary>
    public enum CsTypeKind
    {
        /// <summary>
        /// Primitive type (e.g 'int', 'bool')
        /// </summary>
        Primitive,

        /// <summary>
        /// Pointer type (e.g 'int*')
        /// </summary>
        Pointer,

        /// <summary>
        /// Array type (e.g 'int[4]', 'string')
        /// </summary>
        Sequence,

        /// <summary>
        /// Enum type (e.g 'enum.predef')
        /// </summary>
        Enum,

        /// <summary>
        /// Runtime type (e.g 'Convert.ToInt32("123")')
        /// </summary>
        Runtime
    }
}
