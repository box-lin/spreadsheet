// <copyright file="TestCell.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using CptS321;
using NUnit.Framework;
using SpreadsheetEngine;

namespace CptS321.Tests
{
    /// <summary>
    /// This Class to Test the properties of the Cell.
    /// </summary>
    [TestFixture]
    public class TestCell
    {
        private TheCell cell = new TheCell(3, 'A');

        /// <summary>
        /// Test the cell's private member variables that gets set by the constructor.
        /// </summary>
        [Test]
        public void TestCellPrivateMemberVariables()
        {
            var row = this.cell.RowIndex;
            var column = this.cell.ColumnIndex;
            Assert.AreEqual(3, row);
            Assert.AreEqual('A', column);
        }

        /// <summary>
        /// Test the cell's protected member variables and get set property.
        /// </summary>
        [Test]
        public void TestCellProtectedMemberVariablesAndPropertyField()
        {
            this.cell.Text = "hello";
            Assert.AreEqual("hello", this.cell.Text);
        }
    }
}
