/*
 Written by _King_ (NativeX64)

To-Add syntax:
    * Ternary conditional expressions : ADDED
    * Binary operations (E.g '3 + 4') | 3 + 4 / 2 * 3 == (3 + 4) => (/ 2) => (* 3) | : ADDED
    * Attributes
    * Tables

To-Add keywords:
    * Out
    * New
    * Ref
    * Using

To-Add Operators:
    * ++ : ADDED
    * -- : ADDED
    * Compound operators
 */

using DefaceCompiler.Compiler.ASTGen.AST;
using DefaceCompiler.Compiler.ASTGen.AST.Expressions;
using DefaceCompiler.Compiler.ASTGen.AST.Statements;
using DefaceCompiler.Compiler.ASTGen.AST.Objects;
using DefaceCompiler.Compiler.Lexer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceCompiler.Compiler.ASTGen
{
    /// <summary>
    /// Module for generating an abstract syntax tree out of lexical tokens
    /// </summary>
    class AstGenerator
    {
        /// <summary>
        /// C# access modifiers
        /// </summary>
        readonly string[] AccessModifiers = new string[]
        {
            "public",
            "private",
            "protected",
            "interal",
        };

        /// <summary>
        /// TokenReader instance to simplify handling lexical tokens
        /// </summary>
        TokenReader tokenReader { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="lexTokens"></param>
        public AstGenerator(LexTokenList lexTokens)
        {
            this.tokenReader = new TokenReader(lexTokens);
        }

        /// <summary>
        /// Converts lexical tokens to an abstract syntax tree
        /// </summary>
        /// <returns></returns>
        public StatementList Generate()
        {
            return this.ParseStatements();
        }

        /// <summary>
        /// Parses expressions into a tree until seperator is hit
        /// </summary>
        /// <returns></returns>
        ExpressionList ParseExpressions()
        {
            /* Note to self: Base must be after eventual paranthese to respect scope (E.g 'void method([HERE]...')) */

            ExpressionList expressions = new ExpressionList();
            int Scope = 1;

            while (Scope > 0)
            {
                //if (tokenReader.Expect(LexKind.ParentheseOpen))
                //{
                //    Scope++;
                //}
                //else 
                if (tokenReader.Expect(LexKind.ParentheseClose))
                {
                    Scope = 0;
                }

                if(ParseExpression(out Expression expression))
                {
                    expressions.Add(expression);
                }
                else
                {
                    tokenReader.Skip(1);
                }
            }

            return expressions;
        }

        /// <summary>
        /// Searches for and parses expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        bool ParseExpression(out Expression expression)
        {
            expression = new Expression();
            bool result = false;

            if (tokenReader.Expect(LexKind.ParentheseOpen))
            {
                tokenReader.Skip(1);
                if (ParseExpression(out Expression parExpression))
                {
                    expression = parExpression;
                    result = true;
                }
                tokenReader.Skip(1);
                result = true;
            }

            if(!result && ParseMethodCallExpression(out MethodCallExpression methodCallExpression))
            {
                expression = methodCallExpression;
                result = true;
            }

            if(!result && ParseLocalDeclarationExpression(out LocalDeclarationExpression localDeclarationExpression))
            {
                expression = localDeclarationExpression;
                result = true;
            }

            if(!result && ParseAssignmentExpression(out AssignmentExpression assignmentExpression))
            {
                expression = assignmentExpression;
                result = true;
            }

            if(!result && ParseIncrementExpression(out IncrementExpression incrementExpression))
            {
                expression = incrementExpression;
                result = true;
            }

            if(!result && ParseDecrementExpression(out DecrementExpression decrementExpression))
            {
                expression = decrementExpression;
                result = true;
            }

            if(!result && ParseLocalExpression(out LocalExpression localExpression))
            {
                expression = localExpression;
                result = true;
            }

            if (!result && ParseConstantExpression(out ConstantExpression constantExpression))
            {
                expression = constantExpression;
                result = true;
            }

            if (result && ParseTernaryExpression(expression, out TernaryExpression ternaryExpression))
            {
                expression = ternaryExpression;
                return true;
            }

            if (result && ParseRelationalExpression(expression, out RelationalExpression conditionalExpression))
            {
                expression = conditionalExpression;
                return true;
            }

            if(result && ParseBinaryExpression(expression, out BinaryExpression binaryExpression))
            {
                expression = binaryExpression;
                return true;
            }

            return result;
        }

        bool ParseMethodCallExpression(out MethodCallExpression methodCallExpression)
        {
            methodCallExpression = new MethodCallExpression();

            if (tokenReader.Expect(LexKind.Identifier))
            {
                if (tokenReader.Expect(LexKind.ParentheseOpen, 1))
                {
                    methodCallExpression.Name = tokenReader.Peek().Value;

                    if (tokenReader.Expect(LexKind.ParentheseClose, 2)) // No args
                    {
                        tokenReader.Skip(3);
                        methodCallExpression.Arguments = new ExpressionList();
                    }
                    else // Args
                    {
                        tokenReader.Skip(2);
                        methodCallExpression.Arguments = ParseExpressions();
                    }

                    return true;
                }
            }

            return false;
        }

        bool ParseBinaryExpression(Expression leftExpression, out BinaryExpression binaryExpression)
        {
            binaryExpression = new BinaryExpression();

            if(LexKindHelper.IsBinaryOperator(tokenReader.Peek().Kind))
            {
                binaryExpression.Left = leftExpression;
                
                switch(tokenReader.Peek().Kind)
                {
                    case LexKind.Add:
                        binaryExpression.Operator = BinaryInfix.Add;
                        break;

                    case LexKind.Sub:
                        binaryExpression.Operator = BinaryInfix.Sub;
                        break;

                    case LexKind.Mul:
                        binaryExpression.Operator = BinaryInfix.Mul;
                        break;

                    case LexKind.Div:
                        binaryExpression.Operator = BinaryInfix.Div;
                        break;
                }

                tokenReader.Skip(1);
                if (ParseExpression(out Expression rightExpression))
                {
                    binaryExpression.Right = rightExpression;
                    return true;
                }
            }

            return false;
        }

        bool ParseTernaryExpression(Expression condition, out TernaryExpression ternaryExpression)
        {
            ternaryExpression = new TernaryExpression();

            if (tokenReader.Expect(LexKind.Question))
            {
                tokenReader.Skip(1);
                ternaryExpression.Condition = condition;
                if(ParseExpression(out Expression trueCaseExpression))
                {
                    ternaryExpression.TrueCase = trueCaseExpression;
                    if(tokenReader.ExpectFatal(LexKind.Colon))
                    {
                        tokenReader.Skip(1);
                        if(ParseExpression(out Expression falseCaseExpression))
                        {
                            ternaryExpression.FalseCase = falseCaseExpression;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        bool ParseIncrementExpression(out IncrementExpression incrementExpression)
        {
            incrementExpression = new IncrementExpression();

            if(tokenReader.Expect(LexKind.Identifier))
            {
                if(tokenReader.Expect(LexKind.Add, 1))
                {
                    if (tokenReader.Expect(LexKind.Add, 2))
                    {
                        incrementExpression.Name = tokenReader.Peek().Value;
                        incrementExpression.FixType = FixType.PostFix;
                        tokenReader.Skip(3);

                        return true;
                    }
                }
            }

            if(tokenReader.Expect(LexKind.Add))
            {
                if(tokenReader.Expect(LexKind.Add, 1))
                {
                    if (tokenReader.Expect(LexKind.Identifier, 2))
                    {
                        incrementExpression.Name = tokenReader.Peek(2).Value;
                        incrementExpression.FixType = FixType.PreFix;
                        tokenReader.Skip(3);

                        return true;
                    }
                }
            }

            return false;
        }

        bool ParseDecrementExpression(out DecrementExpression decrementExpression)
        {
            decrementExpression = new DecrementExpression();

            if (tokenReader.Expect(LexKind.Identifier))
            {
                if (tokenReader.Expect(LexKind.Sub, 1))
                {
                    if (tokenReader.Expect(LexKind.Sub, 2))
                    {
                        decrementExpression.Name = tokenReader.Peek().Value;
                        decrementExpression.FixType = FixType.PostFix;
                        tokenReader.Skip(3);

                        return true;
                    }
                }
            }

            if (tokenReader.Expect(LexKind.Sub))
            {
                if (tokenReader.Expect(LexKind.Sub, 1))
                {
                    if (tokenReader.Expect(LexKind.Identifier, 2))
                    {
                        decrementExpression.Name = tokenReader.Peek(2).Value;
                        decrementExpression.FixType = FixType.PreFix;
                        tokenReader.Skip(3);

                        return true;
                    }
                }
            }

            return false;
        }

        bool ParseAssignmentExpression(out AssignmentExpression assignmentExpression)
        {
            assignmentExpression = new AssignmentExpression();

            if(tokenReader.Expect(LexKind.Identifier))
            {
                assignmentExpression.Name = tokenReader.Peek().Value;

                // Determine if assignment is accessing a index
                if (tokenReader.Expect(LexKind.BracketOpen, 1))
                {
                    assignmentExpression.AccessingIndex = true;

                    tokenReader.Skip(2);
                    if (ParseExpression(out Expression index))
                    {
                        assignmentExpression.Index = index;
                    }
                }

                if(tokenReader.Expect(LexKind.Equals, 1))
                {
                    if(tokenReader.Expect(LexKind.Equals, 2))
                    {
                        return false;
                    }

                    tokenReader.Skip(2);
                    if(ParseExpression(out Expression value))
                    {
                        assignmentExpression.Value = value;
                        return true;
                    }
                }
                else if (tokenReader.Expect(LexKind.Equals, 2))
                {
                    switch(tokenReader.Peek(1).Kind)
                    {
                        case LexKind.Add:
                            {
                                assignmentExpression.Infix = AssignmentInfix.PlusEquals;
                                break;
                            }

                        case LexKind.Sub:
                            {
                                assignmentExpression.Infix = AssignmentInfix.MinusEquals;
                                break;
                            }

                        case LexKind.Mul:
                            {
                                assignmentExpression.Infix = AssignmentInfix.MultiplyEquals;
                                break;
                            }

                        case LexKind.Div:
                            {
                                assignmentExpression.Infix = AssignmentInfix.DivideEquals;
                                break;
                            }

                        default:
                            {
                                return false;
                            }
                    }

                    tokenReader.Skip(3);
                    if(ParseExpression(out Expression value))
                    {
                        assignmentExpression.Value = value;
                        return true;
                    }
                }
            }

            return false;
        }

        bool ParseLocalExpression(out LocalExpression localExpression)
        {
            localExpression = new LocalExpression();

            if(tokenReader.Expect(LexKind.Identifier))
            {
                localExpression.Name = tokenReader.Peek().Value;
                tokenReader.Skip(1);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Parses relational expression (E.g '3 == 4')
        /// </summary>
        /// <param name="leftExpression"></param>
        /// <param name="relationalExpression"></param>
        /// <returns></returns>
        bool ParseRelationalExpression(Expression leftExpression, out RelationalExpression relationalExpression)
        {
            relationalExpression = new RelationalExpression();

            if(tokenReader.Expect(LexKind.Equals))
            {
                if(tokenReader.Expect(LexKind.Equals, 1))
                {
                    tokenReader.Skip(2);

                    relationalExpression.Left = leftExpression;
                    relationalExpression.Infix = RelationalInfix.Equals;

                    if(ParseExpression(out Expression expression))
                    {
                        relationalExpression.Right = expression;
                    }
                    else
                    {
                        throw new Exception("No right hand expression found on relational operation");
                    }

                    return true;
                }
                return false;
            }
            
            if(tokenReader.Expect(LexKind.ChevronClose))
            {
                if(tokenReader.Expect(LexKind.Equals, 1))
                {
                    relationalExpression.Infix = RelationalInfix.EqualsBigger;
                    tokenReader.Skip(2);
                }
                else
                {
                    relationalExpression.Infix = RelationalInfix.Bigger;
                    tokenReader.Skip(1);
                }

                relationalExpression.Left = leftExpression;

                if (ParseExpression(out Expression expression))
                {
                    relationalExpression.Right = expression;
                }
                else
                {
                    throw new Exception("No right hand expression found on relational operation");
                }

                return true;
            }

            if (tokenReader.Expect(LexKind.ChevronOpen))
            {
                if (tokenReader.Expect(LexKind.Equals, 1))
                {
                    relationalExpression.Infix = RelationalInfix.EqualsSmaller;
                    tokenReader.Skip(2);
                }
                else
                {
                    relationalExpression.Infix = RelationalInfix.Smaller;
                    tokenReader.Skip(1);
                }

                relationalExpression.Left = leftExpression;

                if (ParseExpression(out Expression expression))
                {
                    relationalExpression.Right = expression;
                }
                else
                {
                    throw new Exception("No right hand expression found on relational operation");
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Parses local declaration expression (E.g)
        /// </summary>
        /// <param name="localDeclarationExpression"></param>
        /// <returns></returns>
        bool ParseLocalDeclarationExpression(out LocalDeclarationExpression localDeclarationExpression)
        {
            localDeclarationExpression = new LocalDeclarationExpression();

            if (tokenReader.Expect(LexKind.Keyword))
            {
                if (tokenReader.Expect(LexKind.Identifier, 1) && DataType.IsDataType(tokenReader.Peek().Value))
                {
                    localDeclarationExpression.DataType = new DataType(tokenReader.Peek().Value, true);
                    localDeclarationExpression.Name = tokenReader.Peek(1).Value;

                    /* If local is assigned at creation */
                    if (tokenReader.Expect(LexKind.Equals, 2))
                    {
                        tokenReader.Skip(3);
                        if (ParseExpression(out Expression local))
                        {
                            localDeclarationExpression.Value = local;
                        }
                        else
                        {
                            Console.WriteLine("Unable to parse expression @ line " + tokenReader.Peek().Line);
                        }
                    }
                    else
                    {
                        tokenReader.Skip(2);
                    }

                    return true;
                }
            }

            return false;
        }

        bool ParseConstantExpression(out ConstantExpression constantExpression)
        {
            constantExpression = new ConstantExpression();

            if (tokenReader.Expect(LexKind.String) || tokenReader.Expect(LexKind.Number) || tokenReader.Expect(LexKind.Boolean))
            {
                constantExpression.Type = new DataType(tokenReader.Peek().Value, false);
                constantExpression.Value = tokenReader.Peek().Value;

                tokenReader.Skip(1);

                return true;
            }

            return false;
        }

        StatementList ParseStatements()
        {
            StatementList statements = new StatementList();
            int Scope = 1;

            while(Scope > 0)
            {
                if (tokenReader.Expect(LexKind.BraceOpen))
                {
                    Scope++;
                }
                else if(tokenReader.Expect(LexKind.BraceClose))
                {
                    Scope--;
                }

                bool statementParsed = ParseStatement(out Statement statement);
                if(statementParsed)
                {
                    statements.Add(statement);
                }
                else
                {
                    tokenReader.Skip(1);
                }

                if(tokenReader.Expect(LexKind.EOF))
                {
                    Scope = 0;
                }
            }

            return statements;
        }

        bool ParseStatement(out Statement statement)
        {
            statement = new Statement();

            if (ParseReturnStatement(out ReturnStatement returnStatement))
            {
                statement = returnStatement;
                return true;
            }

            if (ParseMethodDeclarationStatement(out MethodDeclarationStatement methodDeclarationStatement))
            {
                statement = methodDeclarationStatement;
                return true;
            }

            if (ParseLocalDeclarationStatement(out LocalDeclarationStatement localDeclarationStatement))
            {
                statement = localDeclarationStatement;
                return true;
            }

            if (ParseMethodCallStatement(out MethodCallStatement methodCallStatement))
            {
                statement = methodCallStatement;
                return true;
            }

            if (ParseClassDeclarationStatement(out ClassDeclarationStatement classDeclarationStatement))
            {
                statement = classDeclarationStatement;
                return true;
            }

            if(ParseCompoundStatement(out CompoundStatement compoundStatement))
            {
                statement = compoundStatement;
                return true;
            }

            if(ParseIfStatement(out IfStatement ifStatement))
            {
                statement = ifStatement;
                return true;
            }

            if(ParseWhileStatement(out WhileStatement whileStatement))
            {
                statement = whileStatement;
                return true;
            }

            if(ParseDoWhileStatement(out DoWhileStatement doWhileStatement))
            {
                statement = doWhileStatement;
                return true;
            }
            if(ParseForStatement(out ForLoopStatement forLoopStatement))
            {

                statement = forLoopStatement;
                return true;
            }

            if(ParseAssignmentStatement(out AssignmentStatement assignmentStatement))
            {
                statement = assignmentStatement;
                return true;
            }

            return false;
        }

        bool ParseReturnStatement(out ReturnStatement returnStatement)
        {
            returnStatement = new ReturnStatement();

            if (tokenReader.Expect(LexKind.Keyword) && tokenReader.ExpectValue("return"))
            {
                tokenReader.Skip(1);
                if (ParseExpression(out Expression expression))
                {
                    returnStatement.Value = expression;
                }

                tokenReader.ExpectFatal(LexKind.Semicolon);
                tokenReader.Skip(1);
                return true;
            }

            return false;
        }

        bool ParseAssignmentStatement(out AssignmentStatement assignmentStatement)
        {
            assignmentStatement = new AssignmentStatement();

            if (tokenReader.Expect(LexKind.Identifier))
            {
                assignmentStatement.Name = tokenReader.Peek().Value;

                if (tokenReader.Expect(LexKind.Equals, 1))
                {
                    if (!tokenReader.Expect(LexKind.Equals, 2))
                    {
                        tokenReader.Skip(2);
                        if (ParseExpression(out Expression value))
                        {
                            assignmentStatement.Value = value;

                            return true;
                        }
                    }
                }
                else if (tokenReader.Expect(LexKind.BracketOpen, 1))
                {
                    assignmentStatement.AccessingIndex = true;

                    tokenReader.Skip(2);
                    if (ParseExpression(out Expression index))
                    {
                        assignmentStatement.Index = index;

                        tokenReader.Skip(1);
                        if (tokenReader.ExpectFatal(LexKind.Equals))
                        {
                            tokenReader.Skip(1);
                            if (ParseExpression(out Expression value))
                            {
                                assignmentStatement.Value = value;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        bool ParseForStatement(out ForLoopStatement forLoopStatement)
        {
            forLoopStatement = new ForLoopStatement();

            if(tokenReader.Expect(LexKind.Keyword) && tokenReader.ExpectValue("for"))
            {
                tokenReader.Skip(2);
                if (ParseExpression(out Expression initializer))
                {
                    forLoopStatement.Initializer = initializer;
                }
                tokenReader.Skip(1);

                if (ParseExpression(out Expression condition))
                {
                    forLoopStatement.Condition = condition;
                }
                tokenReader.Skip(1);

                if (ParseExpression(out Expression counter))
                {
                    forLoopStatement.Counter = counter;
                }
                tokenReader.Skip(1);
                if (ParseStatement(out Statement body))
                {
                    forLoopStatement.Body = body;
                }
                else
                {
                    throw new Exception("Statement expected after for header");
                }

                return true;
            }

            return false;
        }

        bool ParseCompoundStatement(out CompoundStatement clauseStatement)
        {
            clauseStatement = new CompoundStatement();

            if(tokenReader.Expect(LexKind.BraceOpen))
            {
                tokenReader.Skip(1);
                clauseStatement.Statements = ParseStatements();
                return true;
            }

            return false;
        }

        bool ParseClassDeclarationStatement(out ClassDeclarationStatement classDeclarationStatement)
        {
            classDeclarationStatement = new ClassDeclarationStatement();

            if (tokenReader.Expect(LexKind.Keyword))
            {
                if (tokenReader.Expect(LexKind.Identifier, 1))
                {
                    if (tokenReader.Peek().Value == "class" && tokenReader.Expect(LexKind.BraceOpen, 2))
                    {
                        classDeclarationStatement = new ClassDeclarationStatement()
                        {
                            Name = tokenReader.Peek(1).Value
                        };

                        tokenReader.Skip(3);
                        classDeclarationStatement.Body = ParseStatements();
                        return true;
                    }
                }
            }

            return false;
        }

        bool ParseMethodDeclarationStatement(out MethodDeclarationStatement methodDeclaration)
        {
            methodDeclaration = new MethodDeclarationStatement();

            if (tokenReader.Expect(LexKind.Keyword))
            {
                if (tokenReader.Expect(LexKind.Identifier, 1))
                {
                    if (tokenReader.Expect(LexKind.ParentheseOpen, 2))
                    {
                        methodDeclaration.ReturnType = new DataType(tokenReader.Peek().Value, true);
                        methodDeclaration.Name = tokenReader.Peek(1).Value;

                        tokenReader.Skip(3);
                        methodDeclaration.Parameters = ParseExpressions();
                        tokenReader.Skip(1);
                        methodDeclaration.Body = ParseStatements();
                        return true;
                    }
                }
            }

            return false;
        }

        bool ParseLocalDeclarationStatement(out LocalDeclarationStatement localDeclarationStatement)
        {
            localDeclarationStatement = new LocalDeclarationStatement();

            if (tokenReader.Expect(LexKind.Keyword))
            {
                if (tokenReader.Expect(LexKind.Identifier, 1))
                {
                    if (DataType.IsDataType(tokenReader.Peek().Value))
                    {
                        localDeclarationStatement = new LocalDeclarationStatement()
                        {
                            DataType = new DataType(tokenReader.Peek().Value, true),
                            Name = tokenReader.Peek(1).Value
                        };

                        /* If local is assigned at creation */
                        if (tokenReader.Expect(LexKind.Equals, 2))
                        {
                            tokenReader.Skip(3);
                            if (ParseExpression(out Expression local))
                            {
                                localDeclarationStatement.Value = local;
                            }
                            else
                            {
                                Console.WriteLine("Unable to parse expression @ line " + tokenReader.Peek().Line);
                            }
                        }
                        else
                        {
                            tokenReader.Skip(2);
                        }

                        tokenReader.ExpectFatal(LexKind.Semicolon);

                        return true;
                    }
                }
            }

            return false;
        }

        bool ParseMethodCallStatement(out MethodCallStatement methodCallStatement)
        {
            methodCallStatement = new MethodCallStatement();

            if (tokenReader.Expect(LexKind.Identifier))
            {
                if (tokenReader.Expect(LexKind.ParentheseOpen, 1))
                {
                    methodCallStatement.Name = tokenReader.Peek().Value;

                    if (tokenReader.Expect(LexKind.ParentheseClose, 2)) // No args
                    {
                        tokenReader.Skip(3);
                        methodCallStatement.Arguments = new ExpressionList();
                    }
                    else // Args
                    {
                        tokenReader.Skip(2);
                        methodCallStatement.Arguments = ParseExpressions();
                    }

                    tokenReader.ExpectFatal(LexKind.Semicolon);

                    return true;
                }
            }

            return false;
        }

        bool ParseWhileStatement(out WhileStatement whileStatement)
        {
            whileStatement = new WhileStatement();

            if (tokenReader.Expect(LexKind.Keyword) && tokenReader.ExpectValue("while"))
            {
                tokenReader.Skip(2);
                if(ParseExpression(out Expression condition))
                {
                    whileStatement.Condition = condition;
                    tokenReader.Skip(1);
                    if(ParseStatement(out Statement body))
                    {
                        whileStatement.Body = body;
                        return true;
                    }
                    else
                    {
                        throw new Exception("Body expected in while loop");
                    }
                }
                else
                {
                    throw new Exception("Condition expected in while loop");
                }
            }

            return false;
        }

        bool ParseDoWhileStatement(out DoWhileStatement doWhileStatement)
        {
            doWhileStatement = new DoWhileStatement();

            if(tokenReader.Expect(LexKind.Keyword) && tokenReader.ExpectValue("do"))
            {
                tokenReader.Skip(1);
                if(ParseStatement(out Statement body))
                {
                    doWhileStatement.Body = body;
                    if (tokenReader.ExpectFatal(LexKind.Keyword) && tokenReader.ExpectValue("while"))
                    {
                        tokenReader.Skip(2);
                        if(ParseExpression(out Expression condition))
                        {
                            doWhileStatement.Condition = condition;
                            return true;
                        }
                        else
                        {
                            throw new Exception("Condition expected in do-while loop");
                        }
                    }
                }
                else
                {
                    throw new Exception("Body expected in do-while loop");
                }
            }

            return false;
        }

        bool ParseIfStatement(out IfStatement ifStatement)
        {
            ifStatement = new IfStatement();

            if(tokenReader.Expect(LexKind.Keyword) && tokenReader.ExpectValue("if"))
            {
                tokenReader.Skip(2);
                if(ParseExpression(out Expression expression))
                {
                    ifStatement.Condition = expression;
                    tokenReader.Skip(1);
                    if(ParseStatement(out Statement trueStatement))
                    {
                        ifStatement.TrueStatement = trueStatement;
                        if(tokenReader.Expect(LexKind.Keyword) && tokenReader.ExpectValue("else"))
                        {
                            tokenReader.Skip(1);
                            if (ParseStatement(out Statement falseStatement))
                            {
                                ifStatement.FalseStatement = falseStatement;
                            }
                        }

                        return true;
                    }
                    else
                    {
                        throw new Exception("Statement expected after if statement (" + tokenReader.Peek().Kind + " gotten)");
                    }
                }
                else
                {
                    throw new Exception("Condition expected in if statement");
                }
            }

            return false;
        }
    }
}
