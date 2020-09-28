using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilerator.Deface.Compiler.AST_Generator.AST;
using Compilerator.Deface.Compiler.Lexical_Analyzer;
using Compilerator.Deface.Compiler.Handlers;
using Compilerator.Deface.Compiler.AST_Generator.Utils;

namespace Compilerator.Deface.Compiler.AST_Generator
{
    class AstGen
    {
        static bool IsCall(LexToken lx1, LexToken lx2) => 
            lx1.Lexeme == Lexemes.Identifier && lx2.Lexeme == Lexemes.Parentheses;

        /// <summary>
        /// Parses a stream of lexical tokens into a hierarchical, abstract syntax tree representation
        /// </summary>
        /// <param name="LexTokens"></param>
        /// <returns></returns>
        public static List<CsClass> Generate(List<LexToken> LexTokens) =>
            ParseClasses(LexTokens);

        /// <summary>
        /// Converts an array of lexemes into a abstract syntax tree
        /// </summary>
        /// <param name="LexTokens"></param>
        /// <returns></returns>
        static List<CsAst> ParseBody(List<LexToken> LexTokens)
        {
            List<CsAst> AST = new List<CsAst>();
            int lc = LexTokens.Count;

            for (int i = 0; i < lc; i++)
            {
                /* Logical syntax */
                if (lc > i + 1 && 
                   IsCall(LexTokens[i], LexTokens[i + 1]))
                {
                    AST.Add(new CsCall()
                    {
                        Name = LexTokens[i].Value,
                        Arguments = CsArgumentParser.Parse(ParseBody(LexTokens[i + 1].Children))
                    });
                    i++;
                    continue;
                }


                /* If no logical syntax can be found, move on to constants */
                if(LexTokens[i].Lexeme == Lexemes.String)
                {
                    AST.Add(new CsData()
                    {
                        AstKind = CsAstKind.SingularData,
                        Type = new CsType()
                        {
                            TypeKind = CsTypeKind.Sequence,
                            SequenceKind = CsSequenceKind.String
                        },
                        Data = LexTokens[i].Value
                    });
                    continue;
                }
            }

            return AST;
        }

        /// <summary>
        /// Parses out the methods from a token stream
        /// </summary>
        /// <param name="LexTokens"></param>
        /// <returns></returns>
        static List<CsMethod> ParseMethods(List<LexToken> LexTokens)
        {
            List<CsMethod> Methods = new List<CsMethod>();

            for(int i = 0; i < LexTokens.Count - 3; i++)
            {
                if(LexTokens[i].Lexeme == Lexemes.Identifier 
                    && LexTokens[i + 1].Lexeme == Lexemes.Identifier 
                    && LexTokens[i + 2].Lexeme == Lexemes.Parentheses 
                    && LexTokens[i + 3].Lexeme == Lexemes.Compound)
                {
                    Methods.Add(new CsMethod()
                    {
                        Name = LexTokens[i + 1].Value,
                        Parameters = CsParameterParser.Parse(LexTokens[i + 2].Children),
                        Body = ParseBody(LexTokens[i + 3].Children),
                    });
                }
            }

            return Methods;
        }

        /// <summary>
        /// Parses out the classes from a token stream
        /// </summary>
        /// <param name="LexTokens"></param>
        /// <returns></returns>
        static List<CsClass> ParseClasses(List<LexToken> LexTokens)
        {
            List<CsClass> Classes = new List<CsClass>();

            for(int i = 0; i < LexTokens.Count - 2; i++)
            {
                if(LexTokens[i].Lexeme == Lexemes.Identifier && LexTokens[i].Value == "class")
                {
                    if(LexTokens[i + 1].Lexeme != Lexemes.Identifier)
                    {
                        ErrorHandler.ThrowException("Class name expected", LexTokens[i + 1].Line.ToString());
                    }
                    else
                    {
                        if (LexTokens[i + 2].Lexeme != Lexemes.Compound)
                        {
                            ErrorHandler.ThrowException("Compound expected in class declaration", LexTokens[i + 2].Line.ToString());
                        }
                        else
                        {
                            Classes.Add(new CsClass()
                            {
                                Name = LexTokens[i + 1].Value,
                                Methods = ParseMethods(LexTokens[i + 2].Children)
                            });
                        }
                    }
                }
            }

            return Classes;
        }
    }
}
