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
        [Test]
        public void TestFibPositiveNthValue()
        {
            
            BigInteger a = 0;
            Assert.AreEqual(a, this.fbReader.Fib(1));

            BigInteger b = 1;
            Assert.AreEqual(b, this.fbReader.Fib(2));

            BigInteger e = 1;
            Assert.AreEqual(e, this.fbReader.Fib(3));

            BigInteger c = 377;
            Assert.AreEqual(c, this.fbReader.Fib(15));
            
        }


    }
}
