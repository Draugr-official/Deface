using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator.AST
{
    /// <summary>
    /// C# conditional statement
    /// </summary>
    class CsConditionalSt : CsAst
    {
        /// <summary>
        /// Gets / sets the test of the conditional
        /// </summary>
        public List<CsAst> Test { get; set; }

        /// <summary>
        /// Gets / sets the ast within the true case
        /// </summary>
        public List<CsAst> TrueCase { get; set; }

        /// <summary>
        /// If available, gets / sets the ast within the false case
        /// </summary>
        public List<CsAst> FalseCase { get; set; }

        public CsConditionalSt() =>
            AstKind = CsAstKind.ConditionalStatement;
    }
}
