using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilerator.Deface.Compiler.AST_Generator.AST;
using Compilerator.Deface.Compiler.Lexical_Analyzer;
using Compilerator.Deface.Handlers;
using Compilerator.Deface.Compiler.AST_Generator.Utils;
using System.Runtime.CompilerServices;

namespace Compilerator.Deface.Compiler.AST_Generator
{
    class AstGen
    {
        static bool IsCall(List<LexToken> LexTokens, int Index) =>
            LexTokens[Index].LexKind            == LexKinds.Identifier 
                && LexTokens[Index + 1].LexKind == LexKinds.Parentheses;

        static bool IsConditionalStatement(List<LexToken> LexTokens, int Index) =>
            LexTokens[Index].LexKind            == LexKinds.Identifier 
                && LexTokens[Index].Value       == "if" 
                && LexTokens[Index + 1].LexKind == LexKinds.Parentheses 
                && LexTokens[Index + 2].LexKind == LexKinds.Compound;

        static bool ElsePresent(List<LexToken> LexTokens, int Index) =>
            LexTokens[Index].LexKind            == LexKinds.Identifier
                && LexTokens[Index].Value       == "else"
                && LexTokens[Index + 1].LexKind == LexKinds.Compound;


        /// <summary>
        /// Parses a stream of lexical tokens into a hierarchical, abstract syntax tree representation
        /// </summary>
        /// <param name="LexTokens"></param>
        /// <returns></returns>
        public static List<CsClass> Generate(List<LexToken> LexTokens) =>
            ParseClasses(LexTokens);

        static List<CsAst> ParseConditional(List<CsAst> AST)
        {
            for(int i = 1; i < AST.Count - 1; i++)
            {
                if(AST[i] is CsPassEq && AST.Count > i + 1)
                {
                    AST[i] = new CsBinaryExpr()
                    {
                        Left = AST[i - 1],
                        Right = AST[i + 1]
                    };
                    AST.RemoveAt(i - 1);
                    AST.RemoveAt(i);
                }
            }

            return AST;
        }

        /// <summary>
        /// Converts an array of lextokens into a abstract syntax tree
        /// </summary>
        /// <param name="LexTokens"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static List<CsAst> ParseBody(List<LexToken> LexTokens)
        {
            List<CsAst> AST = new List<CsAst>();
            int lc = LexTokens.Count;

            for (int i = 0; i < lc; i++)
            {
                /* Logical syntax */
                if (lc > i + 2 && IsConditionalStatement(LexTokens, i))
                {
                    List<CsAst> ElseCase = (lc > i + 4 && ElsePresent(LexTokens, i + 3) ? ParseBody(LexTokens[i + 4].Children) : null);

                    AST.Add(new CsConditionalSt()
                    {
                        Test = ParseBody(LexTokens[i + 1].Children),
                        TrueCase = ParseBody(LexTokens[i + 2].Children),
                        FalseCase = ElseCase
                    });
                    i += 2;
                }

                if (lc > i + 1 && IsCall(LexTokens, i))
                {
                    AST.Add(new CsCall()
                    {
                        Name = LexTokens[i].Value,
                        Arguments = IParseHelper.ParseArguments(ParseBody(LexTokens[i + 1].Children)),
                        Line = LexTokens[i].Line
                    });
                    i++;
                    continue;
                }

                if(LexTokens[i].LexKind == LexKinds.Equals)
                {
                    AST.Add(new CsPassEq());
                    continue;
                }

                /* If no logical syntax can be found, move on to constants */
                if(LexTokens[i].LexKind == LexKinds.String)
                {
                    AST.Add(new CsData()
                    {
                        AstKind = CsAstKind.SingularData,
                        Type = new CsType()
                        {
                            TypeKind = CsTypeKind.Sequence,
                            SequenceKind = CsSequenceKind.String,
                            IsConstant = true
                        },
                        Data = LexTokens[i].Value,
                        Line = LexTokens[i].Line
                    });
                    continue;
                }

                if (LexTokens[i].LexKind == LexKinds.Number)
                {

                    AST.Add(new CsData()
                    {
                        AstKind = CsAstKind.SingularData,
                        Type = new CsType()
                        {
                            TypeKind = CsTypeKind.Primitive,
                            PrimitiveKind = CsPrimitiveValidator.Integral(LexTokens[i].Value),
                            IsConstant = true,
                            IsNumeric = true
                        },
                        Data = LexTokens[i].Value,
                        Line = LexTokens[i].Line
                    });
                    continue;
                }
            }

            return ParseConditional(AST);
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
                if(LexTokens[i].LexKind == LexKinds.Identifier 
                    && LexTokens[i + 1].LexKind == LexKinds.Identifier 
                    && LexTokens[i + 2].LexKind == LexKinds.Parentheses 
                    && LexTokens[i + 3].LexKind == LexKinds.Compound)
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
                if(LexTokens[i].LexKind == LexKinds.Identifier && LexTokens[i].Value == "class")
                {
                    if(LexTokens[i + 1].LexKind != LexKinds.Identifier)
                    {
                        ErrorHandler.ThrowException("Class name expected", "AST:31", LexTokens[i + 1].Line.ToString());
                    }
                    else
                    {
                        if (LexTokens[i + 2].LexKind != LexKinds.Compound)
                        {
                            ErrorHandler.ThrowException("Compound expected in class declaration", "AST:32", LexTokens[i + 2].Line.ToString());
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
