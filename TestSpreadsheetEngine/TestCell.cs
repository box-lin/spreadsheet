// <copyright file="TestCell.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using CptS321;
using NUnit.Framework;

namespace TestSpreadsheetEngine
{
    /// <summary>
    /// This Class to Test the properties of the Cell.
    /// </summary>
    [TestFixture]
    public class TestCell
    {
        private TheCell cell;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCell"/> class.
        /// Set up the cell.
        /// </summary>
        public TestCell()
        {
            this.cell = new TheCell(3, 'A');
            this.cell.Text = "Hello";
        }

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
            Assert.AreEqual("Hello", this.cell.Text);
            Assert.AreEqual('A', this.cell.ColumnIndex);
            Assert.AreEqual(3, this.cell.RowIndex);
        }

        /// <summary>
        /// TheCell class that extend to abstract class Cell.
        /// </summary>
        internal class TheCell : Cell
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TheCell"/> class.
            /// </summary>
            /// <param name="row"> rowIndex. </param>
            /// <param name="col"> colIndex. </param>
            public TheCell(int row, char col)
                : base(row, col)
            {
            }
        }
    }
}
