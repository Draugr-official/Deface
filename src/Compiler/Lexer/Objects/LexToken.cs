using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.Lexer.Objects
{
    class LexToken
    {
        /// <summary>
        /// Gets / sets the kind of the lex token
        /// </summary>
        public LexKind Kind { get; set; }

        /// <summary>
        /// Gets / sets the value of the lex token
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Line info
        /// </summary>
        public int Line { get; set; }
    }
}
