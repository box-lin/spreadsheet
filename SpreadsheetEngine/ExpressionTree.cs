// <copyright file="ExpressionTree.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;

namespace CptS321
{
    /// <summary>
    /// ExpressionTree class.
    /// </summary>
    public class ExpressionTree
    {
        private Node root;
        private string expression;
        private Dictionary<string, double> variables;
        private OpNodeFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"> input expression. </param>
        public ExpressionTree(string expression)
        {
            this.expression = expression;
            this.variables = new Dictionary<string, double>();
            this.factory = new OpNodeFactory();
            this.root = this.BuildExpTree(expression);
        }

        /// <summary>
        /// Evaluate the expression.
        /// </summary>
        /// <returns> result. </returns>
        public double Evaluate()
        {
            return this.root.Evaluate();
        }

        /// <summary>
        /// Sets the specific variable within the ExpressionTree variable dictionary.
        /// </summary>
        /// <param name="variableName"> Variables key. </param>
        /// <param name="variableValue"> Variables val. </param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.variables[variableName] = variableValue;
        }
       
        private List<string> GetPostFixList(string expression)
        {
            List<string> postList = new List<string>();
            Stack<char> stack = new Stack<char>();
            int operandStart = -1;
            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];

                if (this.IsOpOrParenthesis(c))
                {
                    if (operandStart != -1)
                    {
                        string operand = expression.Substring(operandStart, i - operandStart);
                        postList.Add(operand);
                        operandStart = -1;
                    }

                    if (c.Equals('('))
                    {
                        stack.Push(c);
                    }
                    else if (c.Equals(')'))
                    {
                        char op = stack.Pop();
                        while (!op.Equals('('))
                        {
                            postList.Add(op.ToString());
                            op = stack.Pop();
                        }
                    }
                }
                else if (this.factory.IsOperator(c))
                {
                    if (stack.Count == 0 || stack.Peek().Equals('('))
                    {
                        stack.Push(c);
                    }
                    else if (this.IsHigherPrecedance(c, stack.Peek())
                        || (this.IsSamePrecedance(c, stack.Peek()) && this.IsRightAssociative(c)))
                    {
                        stack.Push(c);
                    }
                    else if (this.IsLowerPrecedance(c, stack.Peek())
                        || (this.IsSamePrecedance(c, stack.Peek()) && this.IsLeftAssociative(c)))
                    {
                        do
                        {
                            char op = stack.Pop();
                            postList.Add(op.ToString());
                        }while (stack.Count > 0 && (this.IsLowerPrecedance(c, stack.Peek())
                        || (this.IsSamePrecedance(c, stack.Peek()) && this.IsLeftAssociative(c))));

                        stack.Push(c);
                    }
                }
                else if (operandStart == -1)
                {
                    operandStart = i;
                }
            }

            if (operandStart != -1)
            {
                postList.Add(expression.Substring(operandStart, expression.Length - operandStart));
                operandStart = -1;
            }

            while (stack.Count > 0)
            {
                postList.Add(stack.Pop().ToString());
            }

            return postList;
        }

        /// <summary>
        /// Using recursion to build up the expression tree.
        /// </summary>
        /// <param name="expression"> expression string. </param>
        /// <returns> root node of the expression tree. </returns>
        private Node BuildExpTree(string expression)
        {
            Stack<Node> nodes = new Stack<Node>();
            var post = this.GetPostFixList(expression);
            foreach (var item in post)
            {
                if (item.Length == 1 && this.IsOpOrParenthesis(item[0]))
                {
                    OpNode node = this.factory.CreateOperatorNode(item[0]);
                    node.Right = nodes.Pop();
                    node.Left = nodes.Pop();
                    nodes.Push(node);
                }
                else
                {
                    double num = 0.0;
                    if (double.TryParse(item, out num))
                    {
                        nodes.Push(new ConstantNode(num));
                    }
                    else
                    {
                        nodes.Push(new VariableNode(item, ref this.variables));
                    }
                }
            }

            return nodes.Pop();
        }

        /// <summary>
        /// Check if the input char is operator or parenthesis.
        /// </summary>
        /// <param name="c"> char. </param>
        /// <returns> True or False. </returns>
        private bool IsOpOrParenthesis(char c)
        {
            if (c.Equals('(') || c.Equals(')') || this.factory.IsOperator(c))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check if x precedance higher than y.
        /// </summary>
        /// <param name="x"> char x. </param>
        /// <param name="y"> char y. </param>
        /// <returns> True or False. </returns>
        private bool IsHigherPrecedance(char x, char y)
        {
            OpNode cur = this.factory.CreateOperatorNode(x);
            OpNode topOp = this.factory.CreateOperatorNode(y);
            if (cur.Precedence > topOp.Precedence)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check if x precedance same as y.
        /// </summary>
        /// <param name="x"> char x. </param>
        /// <param name="y"> char y. </param>
        /// <returns> True or False. </returns>
        private bool IsSamePrecedance(char x, char y)
        {
            OpNode cur = this.factory.CreateOperatorNode(x);
            OpNode topOp = this.factory.CreateOperatorNode(y);
            if (cur.Precedence == topOp.Precedence)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check if x precedance lower than y.
        /// </summary>
        /// <param name="x"> char x. </param>
        /// <param name="y"> char y. </param>
        /// <returns> True or False. </returns>
        private bool IsLowerPrecedance(char x, char y)
        {
            OpNode cur = this.factory.CreateOperatorNode(x);
            OpNode topOp = this.factory.CreateOperatorNode(y);
            if (cur.Precedence < topOp.Precedence)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check if x is associtive left.
        /// </summary>
        /// <param name="x"> char x. </param>
        /// <returns>True or False.  </returns>
        private bool IsLeftAssociative(char x)
        {
            OpNode cur = this.factory.CreateOperatorNode(x);
            if (cur.Associativity == OpNode.Associative.Left)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check if x is associtive left.
        /// </summary>
        /// <param name="x"> char x, </param>
        /// <returns>True or False.  </returns>
        private bool IsRightAssociative(char x)
        {
            OpNode cur = this.factory.CreateOperatorNode(x);
            if (cur.Associativity == OpNode.Associative.Right)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       
    }
}
