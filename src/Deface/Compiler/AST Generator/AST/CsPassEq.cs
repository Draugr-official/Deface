using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator.AST
{
    /// <summary>
    /// C# temporary binary eq
    /// </summary>
    class CsPassEq : CsAst
    {
        public CsPassEq() =>
            AstKind = CsAstKind.PassEq;
    }
}
