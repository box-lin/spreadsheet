// <copyright file="FibTests.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System.Numerics;
using HW3_NotePad;
using NUnit.Framework;

namespace HW3Tests
{
    /// <summary>
    /// Fib Method in FibonacciTextReader Test Class.
    /// </summary>
    [TestFixture]
    public class FibTests
    {
        private FibonacciTextReader fbReader;

        /// <summary>
        /// random list instantiate when run the test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.fbReader = new FibonacciTextReader();
        }

        /// <summary>
        /// This method test positive Nth position fibonacci number.
        /// </summary>
        /// <param name="n"> nth position of fibonacci. </param>
        /// <returns> nth value of fibonacci calculated by Fib in fbReader. </returns>
        [TestCase(1, ExpectedResult = 0)]
        [TestCase(2, ExpectedResult = 1)]
        [TestCase(4, ExpectedResult = 2)]
        [TestCase(15, ExpectedResult = 377)]
        public BigInteger TestFibPositiveNthValue(int n)
        {
            return this.fbReader.Fib(n);
        }
    }
}
