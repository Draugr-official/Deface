using Compilerator.Deface.Compiler.AST_Generator.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator
{
    class IParseHelper
    {
        /// <summary>
        /// Parses the arguments of a method into a data list
        /// </summary>
        /// <param name="Lexemes"></param>
        /// <returns></returns>
        public static List<CsData> ParseArguments(List<LexToken> Lexemes)
        {
            return new List<CsData>();
        }
    }
}
