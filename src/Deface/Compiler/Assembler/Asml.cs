using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.Assembler
{
    class Asml
    {
        /// <summary>
        /// Assembles the x86 asm and returns the path to the object file
        /// </summary>
        /// <param name="Assembly"></param>
        /// <returns></returns>
        public static string Assemble(string Assembly)
        {
            if (!Directory.Exists("obj"))
                Directory.CreateDirectory("obj");

            File.WriteAllText("main.asm", Assembly);

            Process psi = new Process();
            psi.StartInfo.UseShellExecute = false;
            psi.StartInfo.RedirectStandardInput = true;
            psi.StartInfo.RedirectStandardOutput = true;
            psi.StartInfo.Arguments = "-f bin main.asm -o obj\\main.obj";
            psi.StartInfo.FileName = "nasm.exe";
            psi.Start();

            //psi.StandardInput.WriteLine($"nasm -f bin main.asm -o main.obj");
            //psi.StandardInput.Flush();
            //psi.StandardInput.Close();
            psi.WaitForExit();
            Console.WriteLine(psi.StandardOutput.ReadToEnd());

            return "obj\\main.obj";
            //for(; ; )
            //{
            //    await Task.Delay(500);
            //    if(File.Exists("obj\\main.obj"))
            //}

        }
    }
}
