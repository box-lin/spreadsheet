// <copyright file="ExpressionTree.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private Node BuildExpTree(string expression)
        {
            char[] operators = { '+', '-', '*', '/' };
            for (int i = expression.Length - 1; i >= 0; i--)
            {
                if (operators.Contains(expression[i]))
                {
                    OpNode opNode = this.GetOpNode(expression[i]);
                    opNode.Left = this.BuildExpTree(expression.Substring(0, i));
                    opNode.Right = this.BuildExpTree(expression.Substring(i + 1));
                    return opNode;
                }
            }

            double constant;
            if (double.TryParse(expression, out constant))
            {
                return new ConstantNode(constant);
            }
            else
            {
                return new VariableNode(expression, ref this.variables);
            }
        }

        /*
        private Node BuildExpTree(string expression)
        {
            char[] operators = { '+', '-', '*', '/', '^' };
            foreach (char op in operators)
            {
                Node n = this.Compile(expression, op);
                if (n != null) return n;
            }

            double constant;
            if (double.TryParse(expression, out constant))
            {
                return new ConstantNode(constant);
            }
            else
            {
                // We need a VariableNode
                return new VariableNode(expression, ref this.variables);
            }
        }

        private Node Compile(string expression, char op)
        {
            for (int i = expression.Length-1; i >= 0; i--)
            {
                if (op == expression[i])
                {
                    OpNode opNode = this.GetOpNode(op);
                    opNode.Left = this.BuildExpTree(expression.Substring(0, i));
                    opNode.Right = this.BuildExpTree(expression.Substring(i + 1));
                    return opNode;
                }
            }

            return null;
        }
        */

        /// <summary>
        /// Pick the correct Operation Node.
        /// (Will not need this after learned and use the OpNode Factory.
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
                default: // if it is not any of the operators that we support, throw an exception:
                    throw new NotSupportedException(
                        "Operator not supported.");
            }

        }

 


    }
}
