﻿// <copyright file="TestOpNodeFactory.cs" company="Boxiang Lin - WSU 011601661">
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
        private readonly OpNodeFactory factory = new OpNodeFactory();

        /// <summary>
        /// Test InitOp if all current exist operator objects have been filled into the dictionary.
        /// </summary>
        [Test]
        public void TestInitOp()
        {
            List<char> expectList = new List<char> { '+', '-', '/', '*' };
            CollectionAssert.AreEquivalent(expectList, this.factory.GetOperators());
        }

        /// <summary>
        /// Test InitOp for operator that from outside project.
        /// </summary>
        [Test]
        public void TestInitOpWithAssembly()
        {
            Assembly.Load("OperatorLibrary-ForTests");
            OpNodeFactory fact = new OpNodeFactory();
            List<char> expectList = new List<char> { '+', '-', '/', '*', '^' };
            CollectionAssert.AreEquivalent(expectList, fact.GetOperators());
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
        /// Test the correctness of GetPrecedence.
        /// </summary>
        [Test]
        public void TestGetPrecedence()
        {
            Assert.AreEqual(6, this.factory.GetPrecedance('*'));
            Assert.AreEqual(6, this.factory.GetPrecedance('/'));
            Assert.AreEqual(7, this.factory.GetPrecedance('+'));
            Assert.AreEqual(7, this.factory.GetPrecedance('-'));
        }

        /// <summary>
        /// Test the NotSupportException for non-implemented operator.
        /// </summary>
        [Test]
        public void TestGetPrecedenceWithUnknownOperator()
        {
            Assert.Throws<NotSupportedException>(() => this.factory.GetPrecedance('`'));
            Assert.Throws<NotSupportedException>(() => this.factory.GetPrecedance(','));
        }

        /// <summary>
        /// Test the Exception operators that did not add precedence.
        /// </summary>
        [Test]
        public void TestGetPrecedenceWithNoPrecedenceOperator()
        {
            Assembly.Load("OperatorLibrary-ForTests");
            OpNodeFactory fact = new OpNodeFactory();
            Assert.Throws<Exception>(() => fact.GetPrecedance('^'));
        }

        /// <summary>
        /// Test the correness of GetAssociativity.
        /// </summary>
        [Test]
        public void TestGetAssociativity()
        {
            Assert.AreEqual(OpNode.Associative.Left, this.factory.GetAssociativity('*'));
            Assert.AreEqual(OpNode.Associative.Left, this.factory.GetAssociativity('/'));
            Assert.AreEqual(OpNode.Associative.Left, this.factory.GetAssociativity('+'));
            Assert.AreEqual(OpNode.Associative.Left, this.factory.GetAssociativity('-'));
        }

        /// <summary>
        /// Test the NotSupportException for non-implemented operator.
        /// </summary>
        [Test]
        public void TestGetAssociativityWithUnknownOperator()
        {
            Assert.Throws<NotSupportedException>(() => this.factory.GetAssociativity('`'));
            Assert.Throws<NotSupportedException>(() => this.factory.GetAssociativity(','));
        }

        /// <summary>
        /// Test the Exception operators that did not add Associativity.
        /// </summary>
        [Test]
        public void TestGetPrecedenceWithNoAssociativityOperator()
        {
            Assembly.Load("OperatorLibrary-ForTests");
            OpNodeFactory fact = new OpNodeFactory();
            Assert.Throws<Exception>(() => fact.GetAssociativity('^'));
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

        /// <summary>
        /// Test if the input char is a supported operator.
        /// </summary>
        [Test]
        public void TestIsOperatorWithSupportedOps()
        {
            Assert.AreEqual(true, this.factory.IsOperator('*'));
            Assert.AreEqual(true, this.factory.IsOperator('/'));
            Assert.AreEqual(true, this.factory.IsOperator('+'));
            Assert.AreEqual(true, this.factory.IsOperator('-'));
        }

        /// <summary>
        /// Test if the input char is not asupported operator.
        /// </summary>
        [Test]
        public void TestIsOperatorWithNotSupportedOps()
        {
            Assert.AreEqual(false, this.factory.IsOperator('$'));
            Assert.AreEqual(false, this.factory.IsOperator('#'));
            Assert.AreEqual(false, this.factory.IsOperator('%'));
            Assert.AreEqual(false, this.factory.IsOperator('?'));
        }
    }
}
