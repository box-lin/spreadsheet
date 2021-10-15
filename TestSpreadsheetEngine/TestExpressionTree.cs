// <copyright file="TestExpressionTree.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CptS321;
using NUnit.Framework;

namespace TestSpreadsheetEngine
{
    /// <summary>
    /// Class to test the Expression Tree.
    /// </summary>
    [TestFixture]
    public class TestExpressionTree
    {
        /// <summary>
        /// Test expression by add operation.
        /// </summary>
        /// <param name="expression"> expression to be evaluate. </param>
        /// <returns> result. </returns>
        [Test]
        [TestCase("3+5",ExpectedResult = 8.0)]
        [TestCase("0+0", ExpectedResult = 0.0)]
        public double TestEvaluateAdd(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            return exp.Evaluate();
        }

        /// <summary>
        /// Test expression by subtract operation.
        /// </summary>
        /// <param name="expression"> expression to be evaluate. </param>
        /// <returns> result. </returns>
        [Test]
        [TestCase("50-5", ExpectedResult = 45.0)]
        [TestCase("0-0", ExpectedResult = 0.0)]
        public double TestEvaluateSub(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            return exp.Evaluate();
        }

        /// <summary>
        /// Test expression by multiply operation.
        /// </summary>
        /// <param name="expression"> expression to be evaluate. </param>
        /// <returns> result. </returns>
        [Test]
        [TestCase("20*5", ExpectedResult = 100.0)]
        [TestCase("0*0", ExpectedResult = 0.0)]
        public double TestEvaluateMul(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            return exp.Evaluate();
        }

        /// <summary>
        /// Test expression by divide operation.
        /// </summary>
        /// <param name="expression"> expression to be evaluate. </param>
        /// <returns> result. </returns>
        [Test]
        [TestCase("20/5", ExpectedResult = 4.0)]
        [TestCase("0/0", ExpectedResult = 0.0)]
        public double TestEvaluateDiv(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            return exp.Evaluate();
        }

        /// <summary>
        /// Test expression with parenthesis.
        /// </summary>
        /// <param name="expression"> expression to be evaluate. </param>
        /// <returns> result. </returns>
        [Test]
        [TestCase("5/(3+2)", ExpectedResult = 1.0)]
        [TestCase("10*(3-1)", ExpectedResult = 20.0)]
        [TestCase("(2+1)*(3-1)", ExpectedResult = 4.0)]
        [TestCase("((2+2))", ExpectedResult = 4.0)]
        public double TestEvaluateParenthesis(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            return exp.Evaluate();
        }
    }
}
