using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.Code_Generator
{
    /// <summary>
    /// Conversion of C# data to x86 data
    /// </summary>
    class IConverter
    {
        /// <summary>
        /// Converts a method to interrupt information, returns 'file descriptor, function index'
        /// </summary>
        /// <param name="MethodName"></param>
        /// <returns></returns>
        public static (int, int) MemberToInterrupt(string MemberName)
        {
            string[] Info = MemberName.Split('.');
            switch(Info[0])
            {
                case "Console":
                    {
                        switch(Info[1])
                        {
                            case "WriteLine":
                                {
                                    return ((int)Props.Interrupts.FileDescriptors.stdout, 
                                            (int)Props.Interrupts.Kernel32.sys_write);
                                }
                        }
                        break;
                    }
            }
            throw new Exceptions.MemberNotFound();
        }
    }

}
