using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator.AST
{
    class CsClass : CsAst
    {
        /// <summary>
        /// Gets / sets the name of the class
        /// </summary>
        public string Name { get; set; }   

        /// <summary>
        /// Gets / sets the methods of the class
        /// </summary>
        public List<CsMethod> Methods { get; set; }
    }
}
