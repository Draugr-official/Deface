using Compilerator.Deface.Compiler.Lexical_Analyzer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.PreProc
{
    class Preprocessor
    {
        public static List<LexToken> Execute(List<LexToken> Tokens)
        {
            for(int i = 0; i < Tokens.Count; i++)
            {

                /* If token is hashtag, proceed */
                if (Tokens[i].LexKind == LexKinds.Hashtag)
                {
                    /* If a preprocessor directive follows the hashtag */
                    if (Tokens[i + 1].LexKind == LexKinds.Identifier)
                    {
                        /* Preprocessor directives */
                        switch(Tokens[i + 1].Value)
                        {
                            case "include":
                                {
                                    /* If ar is file */
                                    if(Tokens[i + 2].LexKind == LexKinds.String)
                                    {
                                        /* If file exists */
                                        if(File.Exists(Tokens[i + 2].Value))
                                        {
                                            string FileName = Tokens[i + 2].Value;
                                            Tokens.RemoveRange(i, 3);
                                            Tokens.InsertRange(i, Lexer.Execute(File.ReadAllText(FileName)));
                                        }
                                        else
                                        {
                                            Handlers.ErrorHandler.ThrowException($"File \"{Tokens[i + 2].Value}\" does not exist", "PPROC:12", Tokens[i + 2].Line.ToString());
                                            Tokens.RemoveRange(i, 3);
                                        }
                                    }
                                    else
                                    {
                                        Handlers.ErrorHandler.ThrowException("Expected file name", "PPROC:11", Tokens[i + 2].Line.ToString());
                                    }
                                    break;
                                }
                        }
                    }
                    else
                    {
                        Handlers.ErrorHandler.ThrowException("Expected preprocessor directive as first non-whitespace", "PPROC:01", Tokens[i + 1].Line.ToString());
                    }
                    continue;
                }

                if(Tokens[i].LexKind == LexKinds.Compound)
                {
                    Tokens[i] = new LexToken()
                    {
                        Children = Execute(Tokens[i].Children),
                        LexKind = LexKinds.Compound,
                        Line = Tokens[i].Line
                    };
                }
            }

            return Tokens;
        }
    }
}
