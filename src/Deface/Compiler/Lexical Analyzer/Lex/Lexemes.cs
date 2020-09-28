using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler
{
    public enum Lexemes
    {
        /// <summary>
        /// Token 'Identifier'
        /// </summary>
        Identifier,

        /// <summary>
        /// Token 'parentheses' (e.g '( ... )')
        /// </summary>
        Parentheses,

        /// <summary>
        /// Token 'compound' (e.g '{ ... }')
        /// </summary>
        Compound,

        /// <summary>
        /// Token 'CurlyBracketOpen' (e.g '{')
        /// </summary>
        CurlyBracketOpen,

        /// <summary>
        /// Token 'CurlyBracketClose' (e.g '}')
        /// </summary>
        CurlyBracketClose,

        /// <summary>
        /// Token 'RoundBracketOpen' (e.g '(')
        /// </summary>
        RoundBracketOpen,

        /// <summary>
        /// Token 'RoundBracketClose' (e.g ')')
        /// </summary>
        RoundBracketClose,

        /// <summary>
        /// Token 'SquareBracketOpen' (e.g '[')
        /// </summary>
        SquareBracketOpen,

        /// <summary>
        /// Token 'SquareBracketClose' (e.g ']')
        /// </summary>
        SquareBracketClose,

        /// <summary>
        /// Token 'String' (e.g "Hello World!")
        /// </summary>
        String,

        /// <summary>
        /// Token 'Number' (e.g '7', '8'..)
        /// </summary>
        Number,

        /// <summary>
        /// Token 'Char' (e.g 'A', 'B'..)
        /// </summary>
        Char,

        /// <summary>
        /// Token 'Boolean' (e.g 'true', 'false')
        /// </summary>
        Boolean,

        /// <summary>
        /// Token 'Mul' (e.g '*')
        /// </summary>
        Mul,

        /// <summary>
        /// Token 'Div' (e.g '/')
        /// </summary>
        Div,

        /// <summary>
        /// Token 'Add' (e.g '+')
        /// </summary>
        Add,

        /// <summary>
        /// Token 'Sub' (e.g '-')
        /// </summary>
        Sub,

        /// <summary>
        /// Token 'Dot' (e.g '.')
        /// </summary>
        Dot,

        /// <summary>
        /// Token 'Comma' (e.g ',')
        /// </summary>
        Comma,

        /// <summary>
        /// Token 'Semicolon' (e.g ';')
        /// </summary>
        Semicolon,

        /// <summary>
        /// Token 'Assign' (e.g '=')
        /// </summary>
        Assign,

        /// <summary>
        /// Token 'Equals' (e.g '==')
        /// </summary>
        Equals,

        /// <summary>
        /// Token 'Unidentified'
        /// </summary>
        Unidentified,
    }
}
