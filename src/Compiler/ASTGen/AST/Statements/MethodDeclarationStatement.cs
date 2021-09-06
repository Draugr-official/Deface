using DefaceCompiler.Compiler.ASTGen.AST.Expressions;
using DefaceCompiler.Compiler.ASTGen.AST.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen.AST.Statements
{
    /// <summary>
    /// C# method declaration statement
    /// </summary>
    class MethodDeclarationStatement : Statement
    {
        /// <summary>
        /// Name of the method
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Return-type of the method
        /// </summary>
        public DataType ReturnType { get; set; }

        /// <summary>
        /// Body of the method
        /// </summary>
        public StatementList Body { get; set; }

        /// <summary>
        /// Parameters of the method
        /// </summary>
        public ExpressionList Parameters { get; set; }
    }
}
