using Compilerator.Deface.Compiler.AST_Generator.AST;
using Compilerator.Deface.Compiler.Code_Generator.Props;
using Compilerator.Deface.Compiler.Code_Generator.Props.Interrupts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

        void DeclareConstant(string LabelName,
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
                foreach (CsMethod Method in Class.Methods)
                {
                    GenerateMethod(Method.Body);
                }
            }

            AddInstruction(x86Mnemonics.mov, x86Registers.eax, (int)Kernel32.sys_exit);
            AddInstruction(x86Mnemonics._int, (int)WinApi.Kernel + "h");

            return ctx;
        }

        int lbsIndex = 0;
        public void GenerateMethod(List<CsAst> Nodes)
        {
            foreach (CsAst Node in Nodes)
            {
                switch (Node.AstKind)
                {
                    case CsAstKind.Call:
                        {
                            CsCall Call = Node as CsCall;
                            var (FileDesc, FuncIndex, ApiIndex) = IConverter.MemberToInterrupt(Call.Name);

                            foreach (CsData Argument in Call.Arguments)
                            {
                                if (Argument.Type.IsConstant)
                                {
                                    string Value = Argument.Data.ToString();
                                    string DataLabel = "var" + lbsIndex++;

                                    switch (Argument.Type.TypeKind)
                                    {
                                        case CsTypeKind.Sequence:
                                            {
                                                if (Argument.Type.SequenceKind == CsSequenceKind.String)
                                                {

                                                    DeclareConstant(DataLabel, x86DefineDirectives.db, $"'{Value}'{(FuncIndex == 4 ? ",0xA" : "")},0");
                                                    AddInstruction(x86Mnemonics.mov, x86Registers.edx, Value.Length + (FuncIndex == 4 ? 1 : 0));
                                                }
                                                break;
                                            }

                                        case CsTypeKind.Primitive:
                                            {
                                                if (Argument.Type.IsNumeric)
                                                {
                                                    if (ulong.TryParse(Value, out ulong ParsedValue))
                                                    {
                                                        DeclareConstant(DataLabel, x86DefineDirectives.db, $"'{ParsedValue}'");
                                                        AddInstruction(x86Mnemonics.mov, x86Registers.edx, ParsedValue.ToString().Length);
                                                    }
                                                }
                                                else
                                                {
                                                    // ...
                                                }
                                                break;
                                            }
                                    }


                                    AddInstruction(x86Mnemonics.mov, x86Registers.ecx, DataLabel);
                                    AddInstruction(x86Mnemonics.mov, x86Registers.ebx, FileDesc);
                                    AddInstruction(x86Mnemonics.mov, x86Registers.eax, FuncIndex);
                                    AddInstruction(x86Mnemonics._int, ApiIndex + "h");
                                }
                            }

                            // Console.WriteLine($"{FileDesc} - {FuncIdx}: {Call.Name} - {Method.Name} (Line {Call.Line})");

                            break;
                        }

                    case CsAstKind.ConditionalStatement:
                        {
                            CsConditionalSt Conditional = Node as CsConditionalSt;

                            for (int i = 0; i < Conditional.Test.Count; i++)
                            {
                                switch (Conditional.Test[i].AstKind)
                                {
                                    case CsAstKind.BinaryExpr:
                                        {
                                            CsBinaryExpr Expression = Conditional.Test[i] as CsBinaryExpr;
                                            string SourceLabel = "var" + lbsIndex++;
                                            string SourceLenLabel = "var" + lbsIndex++;
                                            string DestLabel = "var" + lbsIndex++;

                                            string isEqualsLabel = "var" + lbsIndex++;

                                            /* Source */
                                            switch (Expression.Left.AstKind)
                                            {
                                                case CsAstKind.SingularData:
                                                    {
                                                        CsData Data = Expression.Left as CsData;

                                                        switch (Data.Type.TypeKind)
                                                        {
                                                            case CsTypeKind.Sequence:
                                                                {
                                                                    switch (Data.Type.SequenceKind)
                                                                    {
                                                                        case CsSequenceKind.String:
                                                                            {
                                                                                DeclareConstant(SourceLabel, x86DefineDirectives.db, $"'{Data.Data}',0");
                                                                                DeclareConstant(SourceLenLabel, x86DefineDirectives.equ, $"$-{SourceLabel}");
                                                                                break;
                                                                            }
                                                                    }
                                                                    break;
                                                                }
                                                            case CsTypeKind.Primitive: { break; }
                                                            case CsTypeKind.Runtime: { break; }
                                                            case CsTypeKind.Pointer: { break; }
                                                            case CsTypeKind.Enum: { break; }
                                                        }
                                                        break;
                                                    }
                                            }

                                            /* Destination */
                                            switch (Expression.Right.AstKind)
                                            {
                                                case CsAstKind.SingularData:
                                                    {
                                                        CsData Data = Expression.Right as CsData;

                                                        switch (Data.Type.TypeKind)
                                                        {
                                                            case CsTypeKind.Sequence:
                                                                {
                                                                    switch (Data.Type.SequenceKind)
                                                                    {
                                                                        case CsSequenceKind.String:
                                                                            {
                                                                                DeclareConstant(DestLabel, x86DefineDirectives.db, $"'{Data.Data}',0");
                                                                                break;
                                                                            }
                                                                    }
                                                                    break;
                                                                }
                                                            case CsTypeKind.Primitive: { break; }
                                                            case CsTypeKind.Runtime: { break; }
                                                            case CsTypeKind.Pointer: { break; }
                                                            case CsTypeKind.Enum: { break; }
                                                        }
                                                        break;
                                                    }
                                            }

                                            AddInstruction(x86Mnemonics.lea, x86Registers.esi, $"[{SourceLabel}]");
                                            AddInstruction(x86Mnemonics.lea, x86Registers.edi, $"[{DestLabel}]");
                                            AddInstruction(x86Mnemonics.mov, x86Registers.ecx, $"{SourceLenLabel}");
                                            AddInstruction(x86Mnemonics.rep, "cmpsb");
                                            AddInstruction(x86Mnemonics.jne, isEqualsLabel);

                                            GenerateMethod(Conditional.TrueCase);

                                            AddInstruction(x86Mnemonics.label, isEqualsLabel);

                                            break;
                                        }
                                }
                            }

                            break;
                        }
                }
            }
        }

        public CodeGen(Context _ctx) =>
            ctx = _ctx;
    }
}
