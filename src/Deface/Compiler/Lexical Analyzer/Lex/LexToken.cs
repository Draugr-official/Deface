using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler
{
    /// <summary>
    /// Standard lextoken struct
    /// </summary>
    public struct LexToken
    {
        /// <summary>
        /// Gets / sets the lexkind of the lextoken
        /// </summary>
        public LexKinds LexKind { get; set; }

        /// <summary>
        /// Gets / sets the value of the lextoken (e.g "content here")
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// If available, gets / sets the children of the token
        /// </summary>
        public List<LexToken> Children { get; set; }

        /// <summary>
        /// Gets / sets the line associated with the lextoken
        /// </summary>
        public int Line { get; set; }
    }
}
