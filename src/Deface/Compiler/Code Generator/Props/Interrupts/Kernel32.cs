using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.Code_Generator.Props.Interrupts
{
    /// <summary>
    /// Kernel32.dll api functions
    /// </summary>
    public enum Kernel32
    {
        /// <summary>
        /// kernel32 function 'system exit', 1
        /// </summary>
        sys_exit = 1,

        /// <summary>
        /// kernel32 function 'system write', 4
        /// </summary>
        sys_write = 4
    }
}
