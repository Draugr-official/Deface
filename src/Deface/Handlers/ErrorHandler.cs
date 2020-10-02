using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Handlers
{
    class ErrorHandler
    {
        public static void ThrowException(string msg, string ErrorCode, string line)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"@ Line {line} [{ErrorCode}] : {msg}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
