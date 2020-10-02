using Compilerator.Deface.Compiler.Code_Generator.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilerator.Deface.Compiler.Code_Generator
{
    /// <summary>
    /// x86 application context
    /// </summary>
    class Context
    {
        /// <summary>
        /// Text section containing x86 instructions
        /// </summary>
        public List<x86Instruction> SectionText { get; set; }

        /// <summary>
        /// Data section containing initialized data
        /// </summary>
        public List<x86DataDeclaration_INI> SectionData { get; set; }

        /// <summary>
        /// Data section containing constants
        /// </summary>
        public List<x86DataDeclaration_INI> SectionRData { get; set; }

        /// <summary>
        /// Bss section containing uninitialized data
        /// </summary>
        public List<x86DataDeclaration_UNI> SectionBss { get; set; }

        /// <summary>
        /// Context constructor
        /// </summary>
        public Context()
        {
            SectionText = new List<x86Instruction>();
            SectionData = new List<x86DataDeclaration_INI>();
            SectionRData = new List<x86DataDeclaration_INI>();
            SectionBss  = new List<x86DataDeclaration_UNI>();
        }

        /// <summary>
        /// Converts the context into a human-readable format
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("global _start\nsection .text:\n_start:\n");
            foreach (x86Instruction Instruction in SectionText)
            {
                if(Instruction.Mnemonic == x86Mnemonics.label)
                {
                    sb.Append($"{Instruction.Operand1}:\n");
                }
                else
                {
                    sb.Append($"{Instruction.Mnemonic.ToString().Replace("_", "")} {Instruction.Operand1}" + (Instruction.Operand2 == null ? "\n" : $", {Instruction.Operand2}\n"));
                }
            }

            if(SectionData.Count > 0)
            {
                sb.Append("section .data:\n");
                foreach (x86DataDeclaration_INI DataDecl in SectionData)
                {
                    sb.Append($"{DataDecl.Label} {DataDecl.DefineDerective} {DataDecl.InitialValue}\n");
                }
            }

            if (SectionRData.Count > 0)
            {
                sb.Append("section .rdata:\n");
                foreach (x86DataDeclaration_INI DataDecl in SectionRData)
                {
                    sb.Append($"{DataDecl.Label} {DataDecl.DefineDerective} {DataDecl.InitialValue}\n");
                }
            }

            return sb.ToString();
        }
    }
}
