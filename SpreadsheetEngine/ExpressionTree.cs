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

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"> input expression. </param>
        public ExpressionTree(string expression)
        {
            this.expression = expression;
            this.variables = new Dictionary<string, double>();
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

        /// <summary>
        /// Using recursion to build up the expression tree.
        /// </summary>
        /// <param name="expression"> expression string. </param>
        /// <returns> root node of the expression tree. </returns>
        private Node BuildExpTree(string expression)
        {
            char[] operators = { '+', '-', '*', '/' };
            for (int i = expression.Length - 1; i >= 0; i--)
            {
                if (operators.Contains(expression[i]))
                {
                    OpNode opNode = this.GetOpNode(expression[i]);

                    // recursively to all the way left.
                    opNode.Left = this.BuildExpTree(expression.Substring(0, i));

                    // when backtrack from all the way left, we will assign the right.
                    opNode.Right = this.BuildExpTree(expression.Substring(i + 1));

                    // eventually, recursion steps completed, return the opNode (as it should be the root node).
                    return opNode;
                }
            }

            // Check and return if the remaining string are constant double value.
            double constant;
            if (double.TryParse(expression, out constant))
            {
                return new ConstantNode(constant);
            }

            // Else consider it as a variable and return it.
            else
            {
                return new VariableNode(expression, ref this.variables);
            }
        }

        /// <summary>
        /// Pick the correct Operation Node.
        /// (Will not need this after learned and use the OpNode Factory).
        /// </summary>
        /// <param name="op"> Operator symbol. </param>
        /// <returns> Corresponding OpNode. </returns>
        private OpNode GetOpNode(char op)
        {
            switch (op)
            {
                case '+': return new PlusOp();
                case '-': return new SubOp();
                case '*': return new MulOp();
                case '/': return new DivideOp();
                default:
                    return null;
            }
        }
    }
}
