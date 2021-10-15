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
        private string expression;
        private Dictionary<string, double> variables = new Dictionary<string, double>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"> input expression. </param>
        public ExpressionTree(string expression)
        {
            this.expression = expression;
        }

        /// <summary>
        /// Evaluate the expression.
        /// </summary>
        /// <returns> result. </returns>
        public double Evaluate()
        {
            return 0.0;
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
    }
}
