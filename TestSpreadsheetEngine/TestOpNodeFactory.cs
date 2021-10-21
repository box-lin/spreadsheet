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
    public class TestOpNodeFactory
    {
        private OpNodeFactory factory = new OpNodeFactory();

        /// <summary>
        /// Test InitOp if all current exist operator objects have been filled into the dictionary.
        /// </summary>
        [Test]
        public void TestInitOp()
        {
            HashSet<char> opSet = new HashSet<char> { '+', '-', '/', '*' };
            int expectSum = opSet.Count;
            int actualSum = 0;
            foreach (char key in this.factory.Op.Keys)
            {
                if (opSet.Contains(key))
                {
                    opSet.Remove(key);
                    actualSum++;
                }
            }

            Assert.That(actualSum, Is.EqualTo(expectSum), "The InitOp doesn't cover all the op classes that currently provided in the namespace.");
        }

        /// <summary>
        /// Test correctness operator object that CreateOperatorNode method returns.
        /// </summary>
        [Test]
        public void TestCreateOperatorNode()
        {
            Assert.That(this.factory.CreateOperatorNode('+'), Is.InstanceOf<PlusOp>());
            Assert.That(this.factory.CreateOperatorNode('-'), Is.InstanceOf<SubOp>());
            Assert.That(this.factory.CreateOperatorNode('*'), Is.InstanceOf<MulOp>());
            Assert.That(this.factory.CreateOperatorNode('/'), Is.InstanceOf<DivideOp>());
        }

        /// <summary>
        /// Test the NotSupportedException that CreateIoeratorNode returns.
        /// </summary>
        [Test]
        public void TestCreateOPeratorNodeException()
        {
            Assert.Throws<NotSupportedException>(() => this.factory.CreateOperatorNode('%'));
            Assert.Throws<NotSupportedException>(() => this.factory.CreateOperatorNode('$'));
        }
    }
}
