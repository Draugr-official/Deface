using DefaceCompiler.Compiler.Lexer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen
{
    /// <summary>
    /// Lexical token reader
    /// </summary>
    class TokenReader
    {
        LexTokenList LexTokens { get; set; }

        int Base { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="lexTokens"></param>
        public TokenReader(LexTokenList lexTokens)
        {
            this.LexTokens = lexTokens;
            this.Base = 0;
        }

        /// <summary>
        /// Expects token to have specified kind at position relative to base
        /// </summary>
        /// <param name="lexKind"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        
        public bool Expect(LexKind lexKind, int count = 0) => (this.Base + count <= this.LexTokens.Count - 1 && this.LexTokens[this.Base + count].Kind == lexKind);


        /// <summary>
        /// Expects token to have specified value at position relative to base
        /// </summary>
        /// <param name="lexKind"></param>
        /// <param name="count"></param>
        /// <returns></returns>

        public bool ExpectValue(string value, int count = 0) => (this.Base + count <= this.LexTokens.Count - 1 && this.LexTokens[this.Base + count].Value == value);

        /// <summary>
        /// Expects a token at position, throws an exception if mismatch
        /// </summary>
        /// <param name="lexToken"></param>
        /// <returns></returns>

        public bool ExpectFatal(LexKind lexKind, int count = 0) => (this.LexTokens[this.Base + count].Kind == lexKind ? true : throw new Exception($"Token '{lexKind}' expected, got " + this.Peek(count).Kind.ToString()));

        /// <summary>
        /// Peeks token at 'count' ahead of current token
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>

        public LexToken Peek(int count = 0) => this.LexTokens[this.Base + count];

        /// <summary>
        /// Performs a step
        /// </summary>
        
        public void Step() => this.Base += 1;

        /// <summary>
        /// Skips 'count' tokens
        /// </summary>
        /// <param name="count"></param>
        
        public void Skip(int count) => this.Base += count;

    }
}
