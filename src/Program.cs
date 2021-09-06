//#define debugenabled

using DefaceCompiler.Compiler.ASTGen;
using DefaceCompiler.Compiler.ASTGen.AST;
using DefaceCompiler.Compiler.ASTGen.AST.Statements;
using DefaceCompiler.Compiler.Lexer;
using DefaceCompiler.Compiler.Lexer.Objects;
using DefaceCompiler.Compiler.Preprocessor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
#if debugenabled
            try
            {
#endif
                string Code = File.ReadAllText("Tests\\Ackermann.cs");
                CodeLexer lexer = new CodeLexer(Code);
                LexTokenList lexTokens = lexer.Analyze();

                CPreprocessor preProcessor = new CPreprocessor(lexTokens);
                lexTokens = preProcessor.Process();

                Console.WriteLine("LexTokens (post-processed):");
                foreach (LexToken lexToken in lexTokens)
                {
                    Console.WriteLine(lexToken.Kind.ToString() + " " + (lexToken.Value == null ? "" : lexToken.Value.ToString()));
                }

                Console.WriteLine("------\nGenerating AST..");
                
                AstGenerator astGenerator = new AstGenerator(lexTokens);
                StatementList statements = astGenerator.Generate();

                Console.WriteLine("Generated AST!");

                Console.WriteLine(View.ASTView.GenerateStatements(statements));
#if debugenabled
        }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
#endif

            Console.ReadLine();
        }
    }
}
