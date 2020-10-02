using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator.AST
{
    /// <summary>
    /// C# binary expression
    /// </summary>
    class CsBinaryExpr : CsAst
    {
        /// <summary>
        /// Gets / sets the left side of the binary expression
        /// </summary>
        public CsAst Left { get; set; }

        /// <summary>
        /// Gets / sets the right side of the binary expression
        /// </summary>
        public CsAst Right { get; set; }

        public CsBinaryExpr() =>
            AstKind = CsAstKind.BinaryExpr;
    }
}
