using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilerator.Deface.Compiler;
using Compilerator.Deface.Compiler.AST_Generator;
using Compilerator.Deface.Compiler.AST_Generator.AST;
using Compilerator.Deface.Compiler.Code_Generator;
using Compilerator.Deface.Compiler.Lexical_Analyzer;
using Compilerator.Deface.Compiler.PreProc;
using Compilerator.Deface.Compiler.Assembler;

namespace Compilerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string Code = "class Program\n{\n\tvoid Main()\n\t{\n\t\tif(\"hi\" == \"hello\")\n\t\t{\n\t\t\tConsole.WriteLine(\"the same\");\n\t\t}\n\t\tConsole.WriteLine(\"not the same\");\n\t}\n}";
            Console.WriteLine(Code/* + "\n----------\n"*/);
            List<LexToken> LexTokens = Lexer.Execute(Code);
            LexTokens = Preprocessor.Execute(LexTokens);

            // PrintTokens(LexTokens);

            //Console.WriteLine("\n\n---- AST ----\n");

            List<CsClass> Classes = AstGen.Generate(LexTokens);


            //foreach (CsClass Class in Classes)
            //{
            //    Console.WriteLine($"Class {Class.Name}:");
            //    foreach (CsMethod Method in Class.Methods)
            //    {
            //        Console.WriteLine($"Method {Method.Name}:");
            //        foreach (CsAst Node in Method.Body)
            //        {
            //            Console.WriteLine(Node.AstKind);
            //        }
            //    }
            //}

            Console.WriteLine("\n\n---- Code gen ----\n");

            Context context = new CodeGen(new Context()).Generate(Classes);
            string Result = context.ToString();
            Console.WriteLine(Result);

            Console.WriteLine("\n\n---- Assembler ----\n");
            Asml.Assemble(Result);

            Console.ReadLine();
        }

        static void PrintTokens(List<LexToken> Tokens)
        {
            for(int i = 0; i < Tokens.Count; i++)
            {
                if(Tokens[i].LexKind == LexKinds.Compound || Tokens[i].LexKind == LexKinds.Parentheses)
                {
                    Console.WriteLine(Tokens[i].LexKind + "\n{");
                    PrintTokens(Tokens[i].Children);
                    Console.WriteLine("}");
                }
                else
                {
                    Console.WriteLine($"Token: {Tokens[i].LexKind} | Value: {Tokens[i].Value};");
                }
            }
        }
    }
}
