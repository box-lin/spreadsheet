// <copyright file="FibTests.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
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
        /// This method valid non-zero positive Nth position fibonacci number (Within 32 bits).
        /// </summary>
        /// <param name="n"> nth position of fibonacci. </param>
        [Test]
        public void TestFibPositiveNthValue()
        {
            // Naming vOne => "v" stands value of and "one" stands for position => value of specific position.
            BigInteger vOne = 0;
            Assert.AreEqual(vOne, this.fbReader.Fib(1));

            BigInteger vTwo = 1;
            Assert.AreEqual(vTwo, this.fbReader.Fib(2));

            BigInteger vThree = 1;
            Assert.AreEqual(vThree, this.fbReader.Fib(3));

            BigInteger vFifteen = 377;
            Assert.AreEqual(vFifteen, this.fbReader.Fib(15));
        }


        /// <summary>
        /// This method test the BigInteger output that are larger than 32 bits.
        /// </summary>
        [Test]
        public void TestFibLargerThirtyTwoBitInteger()
        {
            var vNinetyNine = BigInteger.Parse("135301852344706746049");
            Assert.AreEqual(vNinetyNine, this.fbReader.Fib(99));

            var vHundred = BigInteger.Parse("218922995834555169026");
            Assert.AreEqual(vHundred, this.fbReader.Fib(100));
        }

        /// <summary>
        /// This method test the invalid position (n. < 1) should throw arugment exception.
        /// </summary>
        [Test]
        public void TestFibInvalidPositions()
        {
            Assert.Throws<ArgumentException>(() => this.fbReader.Fib(0));
            Assert.Throws<ArgumentException>(() => this.fbReader.Fib(-1));
            Assert.Throws<ArgumentException>(() => this.fbReader.Fib(-2));
            Assert.Throws<ArgumentException>(() => this.fbReader.Fib(-10));
            Assert.Throws<ArgumentException>(() => this.fbReader.Fib(-50));
        }
    }
}
