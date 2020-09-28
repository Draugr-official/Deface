using Compilerator.Deface.Compiler.AST_Generator.AST;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator
{
    class CsPrimitiveValidator
    {
        /// <summary>
        /// Validates the numeric argument as string and returns given primitive kind
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static CsPrimitiveKind Integral(string Value)
        {
            /* If the value is kind 'signed byte' */
            if (sbyte.TryParse(Value, out sbyte KindSbyte))     return CsPrimitiveKind.Sbyte;

            /* If the value is kind 'byte' */
            if (byte.TryParse(Value, out byte KindByte))        return CsPrimitiveKind.Byte;

            /* If the value is kind 'short' */
            if (short.TryParse(Value, out short KindShort))     return CsPrimitiveKind.Short;

            /* If the value is kind 'unsigned short' */
            if (ushort.TryParse(Value, out ushort KindUshort))  return CsPrimitiveKind.Ushort;

            /* If the value is kind 'int' */
            if (int.TryParse(Value, out int KindInt))           return CsPrimitiveKind.Int;

            /* If the value is kind 'unsigned int' */
            if (uint.TryParse(Value, out uint KindUint))        return CsPrimitiveKind.Uint;

            /* If the value is kind 'int' */
            if (long.TryParse(Value, out long KindLong))        return CsPrimitiveKind.Long;

            /* If the value is kind 'unsigned int' */
            if (ulong.TryParse(Value, out ulong KindUlong))     return CsPrimitiveKind.Ulong;


            throw new InvalidDataException("Tried converting a non-full numeral to a numeric primitive kind");
        }
    }
}
