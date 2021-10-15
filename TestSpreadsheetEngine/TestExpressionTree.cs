// <copyright file="TestExpressionTree.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>
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
        [TestCase("3+5", ExpectedResult = 8.0)]
        [TestCase("3+5+10", ExpectedResult = 18.0)]
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
        [TestCase("50-5-10", ExpectedResult = 35.0)]
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
        [TestCase("20*5*2", ExpectedResult = 200.0)]
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
        [TestCase("20/5/2", ExpectedResult = 2.0)]
        [TestCase("5/0", ExpectedResult = double.PositiveInfinity)]
        public double TestEvaluateDiv(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            return exp.Evaluate();
        }

        /// <summary>
        /// Test the Set Variable with a constant.
        /// </summary>
        [Test]
        public void SetVariableTest()
        {
            string variable = "A5";
            double constant = 99.5;
            ExpressionTree exp = new ExpressionTree(variable);
            exp.SetVariable(variable, constant);
            Assert.AreEqual(exp.Evaluate(), constant);
        }

        /// <summary>
        /// Test the Set Variable default value that assign to a variable.
        /// </summary>
        [Test]
        public void SetVariableDefaultTest()
        {
            string variable = "B9";
            ExpressionTree exp = new ExpressionTree(variable);
            Assert.AreEqual(exp.Evaluate(), 0.0);
        }
    }
}
