// <copyright file="TestOpNodeFactory.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Reflection;
using CptS321;
using NUnit.Framework;

namespace CptS321.Tests
{
    /// <summary>
    /// Class to test the Expression Tree.
    /// </summary>
    [TestFixture]
    public class TestOpNodeFactory
    {
        private OpNodeFactory factory;

        /// <summary>
        /// Setup the factory.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.factory = new OpNodeFactory();
        }

        /// <summary>
        /// Test InitOp if all current exist operator objects have been filled into the dictionary.
        /// </summary>
        [Test]
        public void TestInitOp()
        {
            HashSet<char> opSet = new HashSet<char> { '+', '-', '/', '*'};
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
        /// Test InitOp if all current exist operator objects have been filled into the dictionary.
        /// </summary>
        [Test]
        public void TestInitOpWithAssembly()
        {
            Assembly.Load("OperatorLibrary-ForTests");
            OpNodeFactory fact= new OpNodeFactory();
            HashSet<char> opSet = new HashSet<char> { '+', '-', '/', '*', '^' };
            int expectSum = opSet.Count;
            int actualSum = 0;
            foreach (char key in fact.Op.Keys)
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
        /// Test the correctness operator object that CreateOperatorNode method returns.
        /// </summary>
        [Test]
        public void TestCreateOperatorNode()
        {
            Assert.That(this.factory.CreateOperatorNode('+'), Is.TypeOf<PlusOp>());
            Assert.That(this.factory.CreateOperatorNode('-'), Is.TypeOf<SubOp>());
            Assert.That(this.factory.CreateOperatorNode('*'), Is.TypeOf<MulOp>());
            Assert.That(this.factory.CreateOperatorNode('/'), Is.TypeOf<DivideOp>());
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
