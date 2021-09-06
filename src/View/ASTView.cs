using DefaceCompiler.Compiler.ASTGen.AST.Expressions;
using DefaceCompiler.Compiler.ASTGen.AST.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.View
{
    class ASTView
    {
        public static string GenerateStatements(StatementList statements)
        {
            StringBuilder sb = new StringBuilder();

            foreach(Statement statement in statements)
            {
                sb.Append(GenerateStatement(statement));
            }

            return sb.ToString();
        }

        static int Indent = 0;
        static string GenerateStatement(Statement statement)
        {
            StringBuilder sba = new StringBuilder();

            if(statement is ReturnStatement returnStatement)
            {
                sba.AppendLine(GetIndent() + "return::" + GenerateExpression(returnStatement.Value));
            }
            if (statement is ClassDeclarationStatement classDeclarationStatement)
            {
                sba.AppendLine(GetIndent() + "class::" + classDeclarationStatement.Name + "\n" + GetIndent() + "{");
                Indent += 1;
                sba.Append(GenerateStatements(classDeclarationStatement.Body) + "\n");
                Indent -= 1;
                sba.AppendLine(GetIndent() + "}");
            }
            else if(statement is MethodDeclarationStatement methodDeclarationStatement)
            {
                sba.AppendLine(GetIndent() + "method::" + methodDeclarationStatement.Name + " [" + string.Join(", ", methodDeclarationStatement.Parameters.Select(t => ((LocalDeclarationExpression)t).DataType.Type.ToString() + " " + ((LocalDeclarationExpression)t).Name)) + "]\n" + GetIndent() + "{");
                Indent += 1;
                sba.Append(GenerateStatements(methodDeclarationStatement.Body));
                Indent -= 1;
                sba.AppendLine(GetIndent() + "}");
            }
            else if (statement is IfStatement ifStatement)
            {
                sba.AppendLine(GetIndent() + "if::[" + GenerateExpression(ifStatement.Condition) + "]\n" + GetIndent() + "{");
                Indent += 1;
                sba.Append(GenerateStatement(ifStatement.TrueStatement));
                Indent -= 1;
                sba.AppendLine(GetIndent() + "}");

                if(ifStatement.FalseStatement != null)
                {
                    sba.AppendLine(GetIndent() + "else::{");
                    Indent += 1;
                    sba.Append(GenerateStatement(ifStatement.FalseStatement));
                    Indent -= 1;
                    sba.AppendLine(GetIndent() + "}");
                }
            }
            else if (statement is WhileStatement whileStatement)
            {
                sba.AppendLine(GetIndent() + "while::[" + GenerateExpression(whileStatement.Condition) + "]\n" + GetIndent() + "{");
                Indent += 1;
                sba.Append(GenerateStatement(whileStatement.Body));
                Indent -= 1;
                sba.AppendLine(GetIndent() + "}");
            }
            else if (statement is DoWhileStatement doWhileStatement)
            {
                sba.AppendLine(GetIndent() + "do-while::[" + GenerateExpression(doWhileStatement.Condition) + "]\n" + GetIndent() + "{");
                Indent += 1;
                sba.Append(GenerateStatement(doWhileStatement.Body));
                Indent -= 1;
                sba.AppendLine(GetIndent() + "}");
            }
            else if (statement is ForLoopStatement forLoopStatement)
            {
                sba.AppendLine(GetIndent() + "for::[" + GenerateExpression(forLoopStatement.Initializer) + "; " + GenerateExpression(forLoopStatement.Condition) + "; " + GenerateExpression(forLoopStatement.Counter) + "]\n" + GetIndent() + "{");
                Indent += 1;
                sba.Append(GenerateStatement(forLoopStatement.Body));
                Indent -= 1;
                sba.AppendLine(GetIndent() + "}");
            }
            else if(statement is CompoundStatement compoundStatement)
            {
                sba.AppendLine(GetIndent() + "compound::\n" + GetIndent() + "{");
                Indent += 1;
                sba.Append(GenerateStatements(compoundStatement.Statements));
                Indent -= 1;
                sba.AppendLine(GetIndent() + "}");
            }
            else if(statement is LocalDeclarationStatement localDeclarationStatement)
            {
                sba.AppendLine(GetIndent() + "local::" + localDeclarationStatement.DataType.Type.ToString() + " " + localDeclarationStatement.Name + $" [default: {GenerateExpression(localDeclarationStatement.Value)}]");
            }
            else if(statement is MethodCallStatement methodCallStatement)
            {
                sba.AppendLine(GetIndent() + "call::" + methodCallStatement.Name + " [" + string.Join(", ", methodCallStatement.Arguments.Select(t => GenerateExpression(t))) + "]");
            }
            else if(statement is AssignmentStatement assignmentStatement)
            {
                sba.AppendLine(GetIndent() + "assignment::" + assignmentStatement.Name + (assignmentStatement.AccessingIndex ? "[" + GenerateExpression(assignmentStatement.Index) + "]" : "") + "=" + GenerateExpression(assignmentStatement.Value));
            }
            return sba.ToString();
        }

        static string GenerateExpression(Expression expression)
        {
            StringBuilder sba = new StringBuilder();

            if(expression is ConstantExpression constantExpression)
            {
                sba.Append("constant::" + constantExpression.Type.Type + "=" + constantExpression.Value);
            }

            if(expression is MethodCallExpression methodCallExpression)
            {
                sba.Append("call::" + methodCallExpression.Name + " [" + string.Join(", ", methodCallExpression.Arguments.Select(t => GenerateExpression(t))) + "]");
            }

            if(expression is RelationalExpression conditionalExpression)
            {
                sba.Append("relational::left=" + GenerateExpression(conditionalExpression.Left) + " infix='" + conditionalExpression.Infix.ToString() + "' right=" + GenerateExpression(conditionalExpression.Right));
            }

            if(expression is BinaryExpression binaryExpression)
            {
                sba.Append("binary::left=[" + GenerateExpression(binaryExpression.Left) + "] infix='" + (char)binaryExpression.Operator + "' right=[" + GenerateExpression(binaryExpression.Right) + "]");
            }

            if(expression is AssignmentExpression assignmentExpression)
            {
                sba.Append("assignment::name=" + assignmentExpression.Name + " operator=" + assignmentExpression.Infix.ToString() + " value=[" + GenerateExpression(assignmentExpression.Value) + "]");
            }

            if(expression is LocalDeclarationExpression localDeclarationExpression)
            {
                sba.Append("localdeclaration::" + localDeclarationExpression.DataType.Type.ToString() + " " + localDeclarationExpression.Name + "=" + GenerateExpression(localDeclarationExpression.Value));
            }

            if(expression is LocalExpression localExpression)
            {
                sba.Append("local::" + localExpression.Name);
            }

            if(expression is TernaryExpression ternaryExpression)
            {
                sba.Append("ternary::" + GenerateExpression(ternaryExpression.Condition) + " ? " + GenerateExpression(ternaryExpression.TrueCase) + " : " + GenerateExpression(ternaryExpression.FalseCase));
            }

            if(expression is IncrementExpression incrementExpression)
            {
                if(incrementExpression.FixType == FixType.PostFix)
                {
                    sba.Append("increment::" + incrementExpression.Name + "++");
                }
                else
                {
                    sba.Append("increment::++" + incrementExpression.Name);
                }
            }

            if (expression is DecrementExpression decrementExpression)
            {
                if (decrementExpression.FixType == FixType.PostFix)
                {
                    sba.Append("decrement::" + decrementExpression.Name + "--");
                }
                else
                {
                    sba.Append("decrement::--" + decrementExpression.Name);
                }
            }

            return sba.ToString();
        }

        static string GetIndent(int change = 0)
        {
            string sb = "";

            Indent += change;
            for(int i = 0; i < Indent; i++)
            {
                sb += "    ";
            }

            return sb;
        }
    }
}
