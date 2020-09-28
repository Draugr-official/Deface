using Compilerator.Deface.Compiler.AST_Generator.AST;
using Compilerator.Deface.Compiler.Code_Generator.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.Code_Generator
{
    class CodeGen
    {
        /// <summary>
        /// The x86 assembly context
        /// </summary>
        Context ctx { get; set; }

        /// <summary>
        /// Appends a new instruction at the bottom of the .text section
        /// </summary>
        /// <param name="xMnemonic"></param>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        void AddInstruction(x86Mnemonics xMnemonic, 
                            object op1, 
                            object op2)
        {
            ctx.SectionText.Add(new x86Instruction()
            {
                Mnemonic = xMnemonic,
                Operand1 = op1,
                Operand2 = op2
            });
        }

        /// <summary>
        /// Appends a new instruction at the bottom of the .text section
        /// </summary>
        /// <param name="xMnemonic"></param>
        /// <param name="op1"></param>
        void AddInstruction(x86Mnemonics xMnemonic,
                            object op1)
        {
            ctx.SectionText.Add(new x86Instruction()
            {
                Mnemonic = xMnemonic,
                Operand1 = op1
            });
        }

        /// <summary>
        /// Declares new uninitialized data
        /// </summary>
        /// <param name="LabelName"></param>
        /// <param name="Directive"></param>
        /// <param name="DataSize"></param>
        void DeclareUninitializedData(string LabelName, 
                                      x86ReserveDirectives Directive,
                                      int DataSize)
        {
            ctx.SectionBss.Add(new x86DataDeclaration_UNI()
            {
                ReserveDirective = Directive,
                Label = LabelName,
                Size = DataSize
            });
        }

        /// <summary>
        /// Declares new initialized data
        /// </summary>
        /// <param name="LabelName"></param>
        /// <param name="Directive"></param>
        /// <param name="InitVal"></param>
        void DeclareInitializedData(string LabelName,
                                    x86DefineDirectives Directive, 
                                    object InitVal)
        {
            ctx.SectionData.Add(new x86DataDeclaration_INI()
            {
                DefineDerective = Directive,
                InitialValue = InitVal,
                Label = LabelName
            });
        }


        /// <summary>
        /// Converts an AST context to according x86 instructions
        /// </summary>
        /// <param name="Classes"></param>
        /// <returns></returns>
        public Context Generate(List<CsClass> Classes)
        {
            foreach(CsClass Class in Classes)
            {
                foreach(CsMethod Method in Class.Methods)
                {
                    foreach(CsAst Node in Method.Body)
                    {
                        switch(Node.AstKind)
                        {
                            case CsAstKind.Call:
                                {
                                    CsCall Call = Node as CsCall;
                                    var (FileDesc, FuncIdx) = IConverter.MemberToInterrupt(Call.Name);

                                    for(int i = 0; i < Call.Arguments.Count; i++)
                                    {
                                        Console.WriteLine(Call.Arguments[i].Type.TypeKind + " - " + Call.Arguments[i].Data);
                                    }

                                    break;
                                }
                        }
                    }
                }
            }

            AddInstruction(x86Mnemonics.mov, x86Registers.eax, (int)Props.Interrupts.Kernel32.sys_exit);
            AddInstruction(x86Mnemonics._int, (int)Props.Interrupts.WinApi.Kernel);

            return ctx;
        }

        public CodeGen(Context _ctx) =>
            ctx = _ctx;
    }
}
