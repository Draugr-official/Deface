using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.Lexical_Analyzer.Lex
{
    class IValidator
    {
        /// <summary>
        /// Determines if a string only contains numbers
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool IsNumeral(string Value)
        {
            for(int i = 0; i < Value.Length; i++)
            {
                if (Value[i] < '0' || Value[i] > '9')
                    return false;
            }
            return true;
        }
    }
}
