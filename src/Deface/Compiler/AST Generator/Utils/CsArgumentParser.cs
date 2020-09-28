using Compilerator.Deface.Compiler.AST_Generator.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.AST_Generator
{
    class CsArgumentParser
    {
        /// <summary>
        /// Parses the arguments of X and returns them as CsData list
        /// </summary>
        /// <param name="Lexemes"></param>
        /// <returns></returns>
        public static List<CsData> Parse(List<CsAst> AST)
        {
            List<CsData> Arguments = new List<CsData>();

            for(int i = 0; i < AST.Count; i++)
            {
                switch (AST[i].AstKind)
                {
                    case CsAstKind.SingularData:
                        {
                            Arguments.Add(AST[i] as CsData);
                            break;
                        }

                    case CsAstKind.Call:
                        {
                            CsCall Call = AST[i] as CsCall;
                            Arguments.Add(new CsData() 
                            { 
                                Data = Call,
                                Type = new CsType()
                                {
                                    TypeKind = CsTypeKind.Runtime
                                }
                            });
                            break;
                        }
                }
            }

            return Arguments;
        }
    }
}
