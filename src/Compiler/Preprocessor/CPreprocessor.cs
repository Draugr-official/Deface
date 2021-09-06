using DefaceCompiler.Compiler.Lexer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.Preprocessor
{
    /// <summary>
    /// Lexical preprocessor, based off the C preprocessor
    /// </summary>
    class CPreprocessor
    {
        LexTokenList LexTokens { get; set; }

        public CPreprocessor(LexTokenList lexTokens)
        {
            this.LexTokens = lexTokens;
        }

        public LexTokenList Process()
        {
            LexTokenList lexTokens = new LexTokenList();

            foreach(LexToken lexToken in this.LexTokens)
            {
                if(lexToken.Kind != LexKind.Comment)
                {
                    lexTokens.Add(lexToken);
                }
            }

            return lexTokens;
        }
    }
}
