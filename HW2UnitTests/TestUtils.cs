// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using HW2;

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
        public void TestRanDomList()
        {
            int size = rdList.Count;
            Assert.AreEqual(10000, size);
            
        }
    }
}
