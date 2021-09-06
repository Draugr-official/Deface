using DefaceCompiler.Compiler.Lexer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.Lexer
{
    class CodeLexer
    {
        readonly List<string> Keywords = new List<string>
        {
            "abstract",
            "as",
            "base",
            "bool",
            "break",
            "byte",
            "case",
            "catch",
            "char",
            "checked",
            "class",
            "const",
            "continue",
            "decimal",
            "default",
            "delegate",
            "do",
            "double",
            "else",
            "enum",
            "event",
            "explicit",
            "extern",
            "finally",
            "fixed",
            "float",
            "for",
            "foreach",
            "goto",
            "if",
            "implicit",
            "in",
            "int",
            "interface",
            "internal",
            "is",
            "long",
            "lock",
            "namespace",
            "new",
            "null",
            "object",
            "operator",
            "out",
            "override",
            "params",
            "private",
            "protected",
            "public",
            "readonly",
            "ref",
            "return",
            "sbyte",
            "sealed",
            "sizeof",
            "stackalloc",
            "static",
            "string",
            "switch",
            "struct",
            "this",
            "throw",
            "try",
            "typeof",
            "uint",
            "ulong",
            "unchecked",
            "unsafe",
            "ushort",
            "using",
            "virtual",
            "void",
            "volatile",
            "while"
        };

        string Input { get; set; }

        public CodeLexer(string Input)
        {
            this.Input = Input;
        }

        LexKind Identify(string Value)
        {
            if (ulong.TryParse(Value, out _))
                return LexKind.Number;

            if (Value == "false"
                || Value == "true")
                return LexKind.Boolean;

            if (Keywords.Contains(Value))
                return LexKind.Keyword;

            return LexKind.Identifier;
        }

        public LexTokenList Analyze()
        {
            LexTokenList LexTokens = new LexTokenList();
            StringBuilder sb = new StringBuilder();
            int Line = 1;

            for (int i = 0; i < Input.Length; i++)
            {
                LexKind kind = LexKind.Terminal;
                string value = "";
                switch (Input[i])
                {
                    case '(': kind = LexKind.ParentheseOpen; break;
                    case ')': kind = LexKind.ParentheseClose; break;

                    case '[': kind = LexKind.BracketOpen; break;
                    case ']': kind = LexKind.BracketClose; break;

                    case '{': kind = LexKind.BraceOpen; break;
                    case '}': kind = LexKind.BraceClose; break;

                    case '<': kind = LexKind.ChevronOpen; break;
                    case '>': kind = LexKind.ChevronClose; break;


                    case ':': kind = LexKind.Colon; break;
                    case ';': kind = LexKind.Semicolon; break;

                    case ',': kind = LexKind.Comma; break;

                    case '"':
                        {
                            i++;
                            while (Input[i] != '"')
                            {
                                if (Input[i] == '\\' && Input[i + 1] == '"')
                                {
                                    i++;
                                }
                                sb.Append(Input[i]);
                                i++;
                            }
                            value = sb.ToString();
                            kind = LexKind.String;
                            sb.Clear();

                            break;
                        }

                    case '=': kind = LexKind.Equals; break;

                    case ' ': // Discard
                    case '\r':
                    case '\t':
                        break;

                    case '\n':
                        Line++;
                        break;

                    case '?':
                        {
                            kind = LexKind.Question;
                            break;
                        }

                    case '!':
                        {
                            kind = LexKind.Exclamation;
                            break;
                        }

                    case '+':
                        {
                            kind = LexKind.Add;
                            break;
                        }
                    case '-':
                        {
                            kind = LexKind.Sub;
                            break;
                        }
                    case '*':
                        {
                            kind = LexKind.Mul;
                            break;
                        }
                    case '/':
                        {
                            if (Input.Length > i + 1 && Input[i + 1] == '/')
                            {
                                i += 2;
                                for (; Input[i] != '\n' && i < Input.Length; i++)
                                {
                                    sb.Append(Input[i]);
                                }
                                value = sb.ToString();
                                kind = LexKind.Comment;
                                sb.Clear();
                                Line++;
                            }
                            else
                            {
                                kind = LexKind.Div;
                            }
                            break;
                        }

                    default:
                        {
                            if (Char.IsLetterOrDigit(Input[i])
                                || Input[i] == '.')
                            {
                                while (Input.Length > i && (Char.IsLetterOrDigit(Input[i]) || Input[i] == '.'))
                                {
                                    sb.Append(Input[i++]);
                                }
                                i--;
                            }
                            value = sb.ToString();
                            kind = Identify(value);

                            sb.Clear();
                            break;
                        }
                }


                if (kind != LexKind.Terminal)
                {
                    LexTokens.Add(new LexToken()
                    {
                        Kind = kind,
                        Value = value,
                        Line = Line
                    });
                }
            }

            LexTokens.Add(new LexToken()
            {
                Kind = LexKind.Terminal
            });

            LexTokens.Add(new LexToken()
            {
                Kind = LexKind.EOF
            });

            return LexTokens;
        }
    }
}
