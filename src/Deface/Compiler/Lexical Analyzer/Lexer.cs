using Compilerator.Deface.Compiler.Lexical_Analyzer.Lex;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.Lexical_Analyzer
{
    class Lexer
    {

        static StringBuilder identifierBuilder = new StringBuilder();
        static StringBuilder stringBuilder = new StringBuilder();

        public static List<LexToken> Execute(string Code)
        {
            List<LexToken> LexTokens = new List<LexToken>();
            bool StringPasser = false;
            int CurrentLine = 0;

            for(int i = 0; i < Code.Length; i++)
            {
                LexKinds Token = LexKinds.Unidentified;
                string Value = Code[i].ToString();
                bool IdentifierPasser = false;

                switch (Code[i])
                {
                    case '{':
                        {
                            Token = LexKinds.CurlyBracketOpen;
                            break;
                        }
                    case '}':
                        {
                            Token = LexKinds.CurlyBracketClose;
                            break;
                        }


                    case '(':
                        {
                            Token = LexKinds.RoundBracketOpen;
                            break;
                        }
                    case ')':
                        {
                            Token = LexKinds.RoundBracketClose;
                            break;
                        }


                    case '[':
                        {
                            Token = LexKinds.SquareBracketOpen;
                            break;
                        }
                    case ']':
                        {
                            Token = LexKinds.SquareBracketClose;
                            break;
                        }

                    case '=':
                        {
                            if (Code[i + 1] == '=')
                            {
                                Token = LexKinds.Equals;
                                i++;
                            }
                            else
                                Token = LexKinds.Assign;
                            break;
                        }


                    case '#':
                        {
                            Token = LexKinds.Hashtag;
                            break;
                        }
                    case ';':
                        {
                            Token = LexKinds.Semicolon;
                            break;
                        }
                    case '"':
                        {
                            StringPasser = !StringPasser;
                            break;
                        }
                    case '\'':
                        {
                            if(!StringPasser && Code[i + 2] == '\'')
                            {
                                Value = Code[i + 1].ToString();
                                Token = LexKinds.Char;
                                i += 2;
                            }
                            break;
                        }
                    case '\n':
                        {
                            CurrentLine++;
                            break;
                        }

                    /* Ignore */
                    case ' ': 
                    case '\t':
                        {
                            break;
                        }

                    default:
                        {
                            if(!StringPasser)
                            {
                                identifierBuilder.Append(Code[i]);
                                IdentifierPasser = true;
                            }
                            break;
                        }
                }

                /* If entered string, append all next chars until the next quote is hit into a stringBuilder */
                if (StringPasser)
                {
                    stringBuilder.Append(Code[i]);
                }
                else if(!IdentifierPasser)
                {
                    /* If a string was recently constructed, add the lextoken */
                    if(stringBuilder.Length > 0)
                    {
                        if(identifierBuilder.Length > 0)
                        {
                            LexTokens.Add(new LexToken()
                            {
                                Value = identifierBuilder.ToString(),
                                LexKind = LexKinds.Identifier,
                                Line = CurrentLine
                            });

                            identifierBuilder.Clear();
                        }

                        if(stringBuilder.ToString() != "\"")
                            LexTokens.Add(new LexToken()
                            {
                                Value = stringBuilder.ToString().Substring(1, stringBuilder.Length - 1),
                                LexKind = LexKinds.String,
                                Line = CurrentLine
                            });

                        stringBuilder.Clear();
                    }

                    /* Else, validate its token and add the lextoken */
                    else
                    {
                        if (identifierBuilder.Length > 0)
                        {
                            if (IValidator.IsNumeral(identifierBuilder.ToString()))
                                LexTokens.Add(new LexToken()
                                {
                                    Value = identifierBuilder.ToString(),
                                    LexKind = LexKinds.Number,
                                    Line = CurrentLine
                                });
                            else
                                LexTokens.Add(new LexToken()
                                {
                                    Value = identifierBuilder.ToString(),
                                    LexKind = LexKinds.Identifier,
                                    Line = CurrentLine
                                });

                            identifierBuilder.Clear();
                        }

                        if (identifierBuilder.Length == 0 && Token != LexKinds.Unidentified)
                            LexTokens.Add(new LexToken()
                            {
                                LexKind = Token,
                                Value = Value,
                                Line = CurrentLine
                            });
                    }
                }

            }
            return LexProcessor.FixCompounds(
                   LexProcessor.FixParentheses(LexTokens));
        }
    }
}
