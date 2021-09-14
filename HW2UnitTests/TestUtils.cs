// <copyright file="TestUtils.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HW2UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using HW2;
    using NUnit.Framework;

    /// <summary>
    /// The is the Test Class that test all static methods for the HW2 project Utils class.
    /// </summary>
    [TestFixture]
    public class TestUtils
    {
        // Class member random List variable to be used in the test cases.
        private List<int> rdList;

        // Class variable to hold the expect distinct number from random list.
        private int expectDistinct;

        /// <summary>
        /// random list instantiate when run the test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.rdList = Utils.GenerateRandomList(0, 20000, 10000);

            // Calculate the expect distinct number of random list by build-in Distinct().
            this.expectDistinct = this.rdList.Distinct().ToList().Count;
        }

        /// <summary>
        /// Test the random list size that generated from Utils class.
        /// </summary>
        [Test]
        public void TestRandomListSize()
        {
            int size = this.rdList.Count;
            Assert.AreEqual(10000, size);
        }

        /// <summary>
        /// Test the range of random list generated from Utils class.
        /// </summary>
        [Test]
        public void TestRandomListRange()
        {
            bool isRange = false;
            if (this.rdList.Min() >= 0 && this.rdList.Max() <= 20000)
            {
                isRange = true;
            }

            Assert.That(isRange);
        }

        /// <summary>
        /// To test some self-define list by define arry in TestCases and convert to List inside the test method.
        /// Because the TestCase only support the compile time value, where "new" a list happens in the runtime.
        /// </summary>
        /// <param name="array"> pass in an array. </param>
        /// <returns> return a number of distinct value. </returns>
        [TestCase(new[] { 1 }, ExpectedResult = 1)]
        [TestCase(new[] { 1, 2 }, ExpectedResult = 2)]
        [TestCase(new[] { 1, 2, 1, 1, 1 }, ExpectedResult = 2)]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0 }, ExpectedResult = 1)]
        public int TestHashSetDistinct_SampleLists(int[] array)
        {
            List<int> rdList = array.ToList();
            return Utils.HashSetDistinct(rdList);
        }

        /// <summary>
        /// Test the Utils.HashSetDistinct by a random list generated from Utils.GenerateRandomList;
        /// The expect result calculated from the Distinct() build-in function.
        /// </summary>
        [Test]
        public void TestHashSetDistinct_RandomList()
        {
            int actualDistinct = Utils.HashSetDistinct(this.rdList);
            Assert.AreEqual(this.expectDistinct, actualDistinct);
        }

        /// <summary>
        /// Test the correctness of Utils.ConstantSpaceDistinct by some self define list and expectedResults.
        /// </summary>
        /// <param name="array"> pass in an array. </param>
        /// <returns> return a number of distinct value. </returns>
        [TestCase(new[] { 1 }, ExpectedResult = 1)]
        [TestCase(new[] { 1, 2 }, ExpectedResult = 2)]
        [TestCase(new[] { 1, 2, 1, 1, 1 }, ExpectedResult = 2)]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0 }, ExpectedResult = 1)]
        public int TestConstantSpaceDistinct_SampleLists(int[] array)
        {
            List<int> rdList = array.ToList();
            return Utils.ConstantSpaceDistinct(rdList);
        }

        /// <summary>
        /// Test the Utils.ConstantSpaceDistinct by a random list generated from Utils.GenerateRandomList;
        /// The expect result calculated from the Distinct() build-in function.
        /// </summary>
        [Test]
        public void TestConstantSpaceDistinct_RandomList()
        {
            int actualDistinct = Utils.ConstantSpaceDistinct(this.rdList);
            Assert.AreEqual(this.expectDistinct, actualDistinct);
        }

        /// <summary>
        /// Test the correctness of Utils.SortGetDistinct by some self define list and expectedResults.
        /// </summary>
        /// <param name="array"> pass in an array. </param>
        /// <returns> return a number of distinct value. </returns>
        [TestCase(new[] { 1 }, ExpectedResult = 1)]
        [TestCase(new[] { 1, 2 }, ExpectedResult = 2)]
        [TestCase(new[] { 1, 2, 1, 1, 1 }, ExpectedResult = 2)]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0 }, ExpectedResult = 1)]
        public int TestSortGetDistinct_SampleLists(int[] array)
        {
            List<int> rdList = array.ToList();
            return Utils.SortGetDisdinct(rdList);
        }
    }
}
