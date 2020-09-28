using Compilerator.Deface.Compiler.AST_Generator.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator
{
    class CsDataValidator
    {
        /// <summary>
        /// Validates a lexeme's type and converts it to CsType
        /// </summary>
        /// <param name="Lexeme"></param>
        /// <returns></returns>
        public static CsType Validate(LexToken Lexeme)
        {
            CsType Type = new CsType()
            {
                TypeKind = CsTypeKind.Primitive
            };

            switch(Lexeme.Lexeme)
            {
                case Lexemes.String:
                    {
                        Type.TypeKind = CsTypeKind.Sequence;
                        Type.SequenceKind = CsSequenceKind.String;
                        break;
                    }

                case Lexemes.Number:
                    {
                        Type.PrimitiveKind = CsPrimitiveValidator.Integral(Lexeme.Value);
                        break;
                    }

                case Lexemes.Boolean:
                    {
                        Type.PrimitiveKind = CsPrimitiveKind.Bool;
                        break;
                    }
            }

            return Type;
        }
    }
}
