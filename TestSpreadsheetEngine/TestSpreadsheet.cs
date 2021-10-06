// <copyright file="TestSpreadsheet.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;
using CptS321;
using SpreadsheetEngine;

namespace TestSpreadsheetEngine
{

    /// <summary>
    /// Test Spreadsheet class.
    /// </summary>
    [TestFixture]
    public class TestSpreadsheet
    {
        private Spreadsheet ss = new Spreadsheet(49, 26);

        /// <summary>
        /// This method to test the charIndex for a cell at particular position.
        /// </summary>
        [Test]
        public void TestCellsInitCharIndex()
        {
            Assert.AreEqual('Z', this.ss.Cells[0, 25].ColumnIndex);
            Assert.AreEqual('A', this.ss.Cells[0, 0].ColumnIndex);
            Assert.AreEqual('B', this.ss.Cells[0, 1].ColumnIndex);
            Assert.AreEqual('N', this.ss.Cells[0, 13].ColumnIndex);
        }

        /// <summary>
        /// This method to test the rowIndex for a cell at particular position.
        /// </summary>
        [Test]
        public void TestCellsInitRowIndex()
        {
            Assert.AreEqual(1, this.ss.Cells[0, 25].RowIndex);
            Assert.AreEqual(2, this.ss.Cells[1, 25].RowIndex);
            Assert.AreEqual(5, this.ss.Cells[4, 20].RowIndex);
            Assert.AreEqual(48, this.ss.Cells[47, 25].RowIndex);
        }

        /// <summary>
        /// This method to test the GetCell within the boundary.
        /// </summary>
        [Test]
        public void TestGetCellValid()
        {
            Assert.That(this.ss.GetCell(0, 0), Is.InstanceOf<Cell>());
            Assert.That(this.ss.GetCell(1, 5), Is.InstanceOf<Cell>());
            Assert.That(this.ss.GetCell(2, 10), Is.InstanceOf<Cell>());
            Assert.That(this.ss.GetCell(20, 23), Is.InstanceOf<Cell>());
        }

        /// <summary>
        /// This method to test the GetCell ouside the boundary.
        /// </summary>
        [Test]
        public void TestGetCellInvalid()
        {
            Assert.AreEqual(null, this.ss.GetCell(-1, 5));
            Assert.AreEqual(null, this.ss.GetCell(1, 60));
            Assert.AreEqual(null, this.ss.GetCell(-10, 5));
            Assert.AreEqual(null, this.ss.GetCell(2, 70));
        }

        /// <summary>
        /// This method to test the properties.
        /// </summary>
        [Test]
        public void TestMemberProperties()
        {
            Assert.AreEqual(49, this.ss.RowCount);
            Assert.AreEqual(26, this.ss.ColumnCount);
        }

    }
}
