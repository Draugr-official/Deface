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

namespace Compilerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string Code = "class Program\n{\nvoid Main()\n{\nConsole.WriteLine(\"wdjhuw\", Convert.ToInt32(\"raaa\"));\n}\n}";
            List<LexToken> Lexemes = Lexer.Execute(Code);
            PrintTokens(Lexemes);

            Console.WriteLine("\n\n---- AST ----\n");

            List<CsClass> Classes = AstGen.Generate(Lexemes);
            new CodeGen(new Context()).Generate(Classes);


            //foreach(CsClass Class in Classes)
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

            Console.ReadLine();
        }

        static void PrintTokens(List<LexToken> Tokens)
        {
            for(int i = 0; i < Tokens.Count; i++)
            {
                if(Tokens[i].Lexeme == Lexemes.Compound || Tokens[i].Lexeme == Lexemes.Parentheses)
                {
                    Console.WriteLine(Tokens[i].Lexeme + "\n{");
                    PrintTokens(Tokens[i].Children);
                    Console.WriteLine("}");
                }
                else
                {
                    Console.WriteLine($"Token: {Tokens[i].Lexeme} | Value: {Tokens[i].Value};");
                }
            }
        }
    }
}
