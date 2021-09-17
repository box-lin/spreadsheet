// <copyright file="TestUtils.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
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
        /// <summary>
        /// Class member random List variable to be used in the test cases.
        /// </summary>
        private List<int> rdList;

        /// <summary>
        /// Class variable to hold the expect distinct number from random list.
        /// </summary>
        private int expectDistinct;

        /// <summary>
        /// random list instantiate when run the test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.rdList = Utils.GetGenerateRandomList(0, 20000, 10000);

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
        /// Method to test the GetByHashSetDistinct from Utils class by some sample lists.
        /// To test some self-define list by define arry in TestCases and convert to List inside the test method.
        /// Because the TestCase only support the compile time value, where "new" a list happens in the runtime.
        /// </summary>
        /// <param name="array"> pass in an array. </param>
        /// <returns> return a number of distinct value. </returns>
        [TestCase(new[] { 1 }, ExpectedResult = 1)]
        [TestCase(new[] { 1, 2 }, ExpectedResult = 2)]
        [TestCase(new[] { 1, 2, 1, 1, 1 }, ExpectedResult = 2)]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0 }, ExpectedResult = 1)]
        public int TestGetByHashSetDistinct_SampleLists(int[] array)
        {
            List<int> rdList = array.ToList();
            return Utils.GetByHashSetDistinct(rdList);
        }

        /// <summary>
        /// Test the Utils.GetByHashSetDistinct by a random list generated from Utils.GetGenerateRandomList;
        /// The expect result calculated from the Distinct() build-in function.
        /// </summary>
        [Test]
        public void TestGetByHashSetDistinct_RandomList()
        {
            int actualDistinct = Utils.GetByHashSetDistinct(this.rdList);
            Assert.AreEqual(this.expectDistinct, actualDistinct);
        }

        /// <summary>
        /// Test the correctness of Utils.GetByConstantSpaceDistinct by some self define list and expectedResults.
        /// </summary>
        /// <param name="array"> pass in an array. </param>
        /// <returns> return a number of distinct value. </returns>
        [TestCase(new[] { 1 }, ExpectedResult = 1)]
        [TestCase(new[] { 1, 2 }, ExpectedResult = 2)]
        [TestCase(new[] { 1, 2, 1, 1, 1 }, ExpectedResult = 2)]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0 }, ExpectedResult = 1)]
        public int TestGetByConstantSpaceDistinct_SampleLists(int[] array)
        {
            List<int> rdList = array.ToList();
            return Utils.GetByConstantSpaceDistinct(rdList);
        }

        /// <summary>
        /// Test the Utils.GetByConstantSpaceDistinct by a random list generated from Utils.GetGenerateRandomList;
        /// The expect result calculated from the Distinct() build-in function.
        /// </summary>
        [Test]
        public void TestGetByConstantSpaceDistinct_RandomList()
        {
            int actualDistinct = Utils.GetByConstantSpaceDistinct(this.rdList);
            Assert.AreEqual(this.expectDistinct, actualDistinct);
        }

        /// <summary>
        /// Test the correctness of Utils.GetSortDistinct by some self define list and expectedResults.
        /// </summary>
        /// <param name="array"> pass in an array. </param>
        /// <returns> return a number of distinct value. </returns>
        [TestCase(new[] { 1 }, ExpectedResult = 1)]
        [TestCase(new[] { 1, 2 }, ExpectedResult = 2)]
        [TestCase(new[] { 1, 2, 1, 1, 1 }, ExpectedResult = 2)]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0 }, ExpectedResult = 1)]
        public int TestGetSortDistinct_SampleLists(int[] array)
        {
            List<int> rdList = array.ToList();
            return Utils.GetBySortDisdinct(rdList);
        }

        /// <summary>
        /// Test the Utils.GetSortDistinct by a random list generated from Utils.GetGenerateRandomList;
        /// The expect result calculated from the Distinct() build-in function.
        /// </summary>
        [Test]
        public void TestGetSortDistinct_RandomList()
        {
            int actualDistinct = Utils.GetBySortDisdinct(this.rdList);
            Assert.AreEqual(this.expectDistinct, actualDistinct);
        }
    }
}
