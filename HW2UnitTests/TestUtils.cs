// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using HW2;
using System.Linq;

namespace HW2UnitTests
{
    [TestFixture]
    public class TestUtils
    {

        //Class member random List variable to be used in the test cases
        List<int> rdList;

        //Class variable to hold the expect distinct number from random list.
        int expectDistinct;
        


        /// <summary>
        /// random list instantiate when run the test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            rdList = Utils.GenerateRandomList(0, 20000, 10000);

            //Calculate the expect distinct number of random list by build-in Distinct()
            expectDistinct = rdList.Distinct().ToList().Count;
        }


        /// <summary>
        /// Test the random list size that generated from Utils class
        /// </summary>
        [Test]
        public void TestRandomListSize()
        {
            int size = rdList.Count;
            Assert.AreEqual(10000, size);
        }


        /// <summary>
        /// Test the range of random list generated from Utils class.
        /// </summary>
        [Test]
        public void TestRandomListRange()
        {
            bool isRange = false;
            if (rdList.Min() >= 0 && rdList.Max() <= 20000)
            {
                isRange = true;
            }
            Assert.That(isRange);
        }


        /// <summary>
        /// To test some self-define list by define arry in TestCases and convert to List inside the test method.
        /// Because the TestCase only support the compile time value, where "new" a list happens in the runtime.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        [TestCase(new[] { 1 }, ExpectedResult = 1)]
        [TestCase(new[] { 1, 2 }, ExpectedResult = 2)]
        [TestCase(new[] { 1, 2, 1, 1, 1 }, ExpectedResult = 2)]
        [TestCase(new[] { 0,0,0,0,0,0,0,0}, ExpectedResult = 1)]
        public int TestHashSetDistinct_SampleLists(int [] array)
        {
            List<int> rdList = array.ToList();
            return Utils.HashSetDistinct(rdList);
        }


        /// <summary>
        /// Test the Utils.HashSetDistinct by a random list generated from Utils.GenerateRandomList;
        /// The expectDistinct calculated from the Distinct() build-in function.
        /// </summary>
        [Test]
        public void TestHashSetDistinct_RandomList()
        {
            int actualDistinct = Utils.HashSetDistinct(rdList);
            Assert.AreEqual(expectDistinct, actualDistinct);
        }

        /// <summary>
        /// Test the correctness of Utils.ConstantSpaceDistinct by some self define list and expectedResults.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        [TestCase(new[] { 1 }, ExpectedResult = 1)]
        [TestCase(new[] { 1, 2 }, ExpectedResult = 2)]
        [TestCase(new[] { 1, 2, 1, 1, 1 }, ExpectedResult = 2)]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0 }, ExpectedResult = 1)]
        public int TestConstantSpaceDistinct_SampleLists(int [] array)
        {
            List<int> rdList = array.ToList();
            return Utils.ConstantSpaceDistinct(rdList);
        }


        [Test]
        public void TestConstantSpaceDistinct_RandomList()
        {
            int actualDistinct = Utils.ConstantSpaceDistinct(rdList);
            Assert.AreEqual(expectDistinct, actualDistinct);
        }




    }
}
