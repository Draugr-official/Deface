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

            switch(Lexeme.LexKind)
            {
                case LexKinds.String:
                    {
                        Type.TypeKind = CsTypeKind.Sequence;
                        Type.SequenceKind = CsSequenceKind.String;
                        break;
                    }

                case LexKinds.Number:
                    {
                        Type.PrimitiveKind = CsPrimitiveValidator.Integral(Lexeme.Value);
                        Type.IsNumeric = true;
                        break;
                    }

                case LexKinds.Boolean:
                    {
                        Type.PrimitiveKind = CsPrimitiveKind.Bool;
                        break;
                    }
            }

            return Type;
        }
    }
}
