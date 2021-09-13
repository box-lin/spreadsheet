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
        List<int> rdList;
        

        [SetUp]
        public void SetUp()
        {
            rdList = Utils.GenerateRandomList(0, 20000, 10000);
        }


        [Test]
        public void TestRandomListSize()
        {
            int size = rdList.Count;
            Assert.AreEqual(10000, size);
        }


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

        [Test]
        public void TestHashSetDistinct_RandomList()
        {
            //using the build-in Distinct for test purpose
            int expectDistinct = rdList.Distinct().ToList().Count;
            int actualDistinct = Utils.HashSetDistinct(rdList);
            Assert.AreEqual(expectDistinct, actualDistinct);
        }


        [TestCase(new[] { 1 }, ExpectedResult = 1)]
        [TestCase(new[] { 1, 2 }, ExpectedResult = 2)]
        [TestCase(new[] { 1, 2, 1, 1, 1 }, ExpectedResult = 2)]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0 }, ExpectedResult = 1)]
        public int TestConstantSpaceDistinct_SampleLists(int [] array)
        {
            List<int> rdList = array.ToList();
            return Utils.ConstantSpaceDistinct(rdList);
        }

    }
}
