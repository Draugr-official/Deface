using Compilerator.Deface.Compiler.Lexical_Analyzer.Lex;
using System;
using System.Collections.Generic;
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
                Lexemes Token = Lexemes.Unidentified;
                string Value = Code[i].ToString();
                bool IdentifierPasser = false;

                switch (Code[i])
                {
                    case '{':
                        {
                            Token = Lexemes.CurlyBracketOpen;
                            break;
                        }
                    case '}':
                        {
                            Token = Lexemes.CurlyBracketClose;
                            break;
                        }


                    case '(':
                        {
                            Token = Lexemes.RoundBracketOpen;
                            break;
                        }
                    case ')':
                        {
                            Token = Lexemes.RoundBracketClose;
                            break;
                        }


                    case '[':
                        {
                            Token = Lexemes.SquareBracketOpen;
                            break;
                        }
                    case ']':
                        {
                            Token = Lexemes.SquareBracketClose;
                            break;
                        }

                    case '=':
                        {
                            if (Code[i + 1] == '=')
                            {
                                Token = Lexemes.Equals;
                                i++;
                            }
                            else
                                Token = Lexemes.Assign;
                            break;
                        }


                    case ';':
                        {
                            Token = Lexemes.Semicolon;
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
                                Token = Lexemes.Char;
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
                    case ' ': case '\t':
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
                    /* If a string was recently constructed, add the lexeme */
                    if(stringBuilder.Length > 0)
                    {
                        if(stringBuilder.ToString() != "\"")
                            LexTokens.Add(new LexToken()
                            {
                                Value = stringBuilder.ToString().Substring(1, stringBuilder.Length - 1),
                                Lexeme = Lexemes.String,
                                Line = CurrentLine
                            });

                        stringBuilder.Clear();
                        identifierBuilder.Clear();
                    }
                    /* Else, validate its token and add the lexeme */
                    else
                    {
                        if (identifierBuilder.Length > 0)
                        {
                            if (IValidator.IsNumeral(identifierBuilder.ToString()))
                                LexTokens.Add(new LexToken()
                                {
                                    Value = identifierBuilder.ToString(),
                                    Lexeme = Lexemes.Number,
                                    Line = CurrentLine
                                });
                            else
                                LexTokens.Add(new LexToken()
                                {
                                    Value = identifierBuilder.ToString(),
                                    Lexeme = Lexemes.Identifier,
                                    Line = CurrentLine
                                });

                            identifierBuilder.Clear();
                        }

                        if (identifierBuilder.Length == 0 && Token != Lexemes.Unidentified)
                            LexTokens.Add(new LexToken()
                            {
                                Lexeme = Token,
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
