using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.Lexer.Objects
{
    public enum LexKind
    {
        UN,

        Terminal,

        /// <summary>
        /// E'g '// Short comment'
        /// </summary>
        Comment,

        /// <summary>
        /// E.g '['
        /// </summary>
        BracketOpen,

        /// <summary>
        /// E.g ']'
        /// </summary>
        BracketClose,


        /// <summary>
        /// E.g '('
        /// </summary>
        ParentheseOpen,

        /// <summary>
        /// E.g ')'
        /// </summary>
        ParentheseClose,


        /// <summary>
        /// E.g '{'
        /// </summary>
        BraceOpen,

        /// <summary>
        /// E.g '}'
        /// </summary>
        BraceClose,


        /// <summary>
        /// E.g '<'
        /// </summary>
        ChevronOpen,

        /// <summary>
        /// E.g '>'
        /// </summary>
        ChevronClose,


        /// <summary>
        /// E.g ':'
        /// </summary>
        Colon,

        /// <summary>
        /// E.g ';'
        /// </summary>
        Semicolon,

        /// <summary>
        /// E.g '.'
        /// </summary>
        Dot,

        /// <summary>
        /// E.g ','
        /// </summary>
        Comma,

        /// <summary>
        /// E.g '?'
        /// </summary>
        Question,

        /// <summary>
        /// E.g '!'
        /// </summary>
        Exclamation,

        /// <summary>
        /// E.g '='
        /// </summary>
        Equals,

        /// <summary>
        /// E.g '"Hello World"'
        /// </summary>
        String,

        /// <summary>
        /// E.g ''a''
        /// </summary>
        Char,

        /// <summary>
        /// E.g '369'
        /// </summary>
        Number,

        /// <summary>
        /// E.g 'true', 'false'
        /// </summary>
        Boolean,

        /// <summary>
        /// E.g 'var1', '8ball'
        /// </summary>
        Identifier,

        /// <summary>
        /// E.g 'if', 'while'
        /// </summary>
        Keyword,

        /// <summary>
        /// E.g '+'
        /// </summary>
        Add,

        /// <summary>
        /// E.g '-'
        /// </summary>
        Sub,

        /// <summary>
        /// E.g '*'
        /// </summary>
        Mul,

        /// <summary>
        /// E.g '/'
        /// </summary>
        Div,

        /// <summary>
        /// End of file
        /// </summary>
        EOF
    }

    class LexKindHelper
    {
        public static bool IsBinaryOperator(LexKind lexKind)
        {
            switch(lexKind)
            {
                case LexKind.Add:
                case LexKind.Sub:
                case LexKind.Mul:
                case LexKind.Div:
                    return true;

                default:
                    return false;
            }
        }
    }
}
