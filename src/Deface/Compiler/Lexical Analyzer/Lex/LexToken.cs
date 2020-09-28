using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler
{
    /// <summary>
    /// Standard lexeme struct
    /// </summary>
    public struct LexToken
    {
        /// <summary>
        /// Gets / sets the lextoken of the lexeme
        /// </summary>
        public Lexemes Lexeme { get; set; }

        /// <summary>
        /// Gets / sets the value of the lexeme (e.g "content here")
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// If available, gets / sets the children of the token
        /// </summary>
        public List<LexToken> Children { get; set; }

        /// <summary>
        /// Gets / sets the line associated with the lexeme
        /// </summary>
        public int Line { get; set; }
    }
}
