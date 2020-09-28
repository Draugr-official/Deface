using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.Lexical_Analyzer.Lex
{
    class LexProcessor
    {
        public static List<LexToken> FixCompounds(List<LexToken> tokenStream)
        {
            List<LexToken> Tokens = new List<LexToken>();
            List<LexToken> TClc = new List<LexToken>();
            bool startCollect = false;
            int tlen = 0;

            for (int i = 0; i < tokenStream.Count; i++)
            {
                switch (tokenStream[i].Lexeme)
                {
                    case Lexemes.CurlyBracketOpen:
                        tlen++;
                        startCollect = true;
                        break;

                    case Lexemes.CurlyBracketClose:
                        tlen--;
                        if (tlen == 0)
                            startCollect = false;
                        break;
                }

                if (startCollect)
                    TClc.Add(tokenStream[i]);
                else
                {
                    if (TClc.Count == 0)
                        Tokens.Add(tokenStream[i]);
                    else
                    {
                        TClc.RemoveAt(0);
                        Tokens.Add(new LexToken()
                        {
                            Lexeme = Lexemes.Compound,
                            Children = FixCompounds(TClc)
                        });
                        TClc.Clear();
                    }
                }
            }

            return Tokens;
        }

        public static List<LexToken> FixParentheses(List<LexToken> tokenStream)
        {
            List<LexToken> Tokens = new List<LexToken>();
            List<LexToken> TClc = new List<LexToken>();
            bool startCollect = false;
            int tlen = 0;

            for (int i = 0; i < tokenStream.Count; i++)
            {
                switch (tokenStream[i].Lexeme)
                {
                    case Lexemes.RoundBracketOpen:
                        tlen++;
                        startCollect = true;
                        break;

                    case Lexemes.RoundBracketClose:
                        tlen--;
                        if (tlen == 0)
                            startCollect = false;
                        break;
                }

                if (startCollect)
                    TClc.Add(tokenStream[i]);
                else
                {
                    if (TClc.Count == 0)
                        Tokens.Add(tokenStream[i]);
                    else
                    {
                        TClc.RemoveAt(0);
                        Tokens.Add(new LexToken()
                        {
                            Lexeme = Lexemes.Parentheses,
                            Children = FixParentheses(TClc)
                        });
                        TClc.Clear();
                    }
                }
            }

            return Tokens;
        }
    }
}
