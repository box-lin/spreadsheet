// <copyright file="TestInvalidReference.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SpreadsheetEngine;

namespace CptS321.Tests
{
    /// <summary>
    /// Class for testing invalid reference messages.
    /// </summary>
    [TestFixture]
    public class TestInvalidReference
    {
        private Spreadsheet sp = new Spreadsheet(50, 26);

        /// <summary>
        /// Test the single self referencing.
        /// </summary>
        [Test]
        public void TestSingleSelfReference()
        {
            TheCell cell = this.sp.Cells[0, 2];
            string curCellName = cell.Name;
            cell.Text = "=" + cell.Name;
            Assert.AreEqual("!(Self Reference)", cell.Value);
        }

        /// <summary>
        /// Test the multiple variable but at lease one self referencing.
        /// </summary>
        [Test]
        public void TestMulVariableContainsSelfReference()
        {
            TheCell cell = this.sp.Cells[0, 0];
            string curCellName = cell.Name;
            cell.Text = "=" + cell.Name + "+E5*C9";
            Assert.AreEqual("!(Self Reference)", cell.Value);
        }

        /// <summary>
        /// Test single bad referencing.
        /// </summary>
        [Test]
        public void TestSingleBadReference()
        {
            TheCell cell = this.sp.Cells[9, 9];
            string curCellName = cell.Name;
            cell.Text = "=hello";
            Assert.AreEqual("!(Bad Reference)", cell.Value);
        }

        /// <summary>
        /// Test the multiple variable but at lease one bad reference.
        /// </summary>
        [Test]
        public void TestMulVariableContainsBadReference()
        {
            TheCell cell = this.sp.Cells[1, 1];
            string curCellName = cell.Name;
            cell.Text = "=hello+A9+A2*3";
            Assert.AreEqual("!(Bad Reference)", cell.Value);
        }

        /// <summary>
        /// Test with simple circular referencing.
        /// </summary>
        [Test]
        public void TestSimpleCircularReference()
        {
            TheCell aCell = this.sp.Cells[10, 10];
            TheCell bCell = this.sp.Cells[11, 11];
            string aCellName = aCell.Name;
            string bCellName = bCell.Name;
            aCell.Text = "=" + bCellName;
            bCell.Text = "=" + aCellName;
            Assert.AreEqual("0", aCell.Value);
            Assert.AreEqual("!(Circular Reference)", bCell.Value);
        }

        /// <summary>
        /// Test with complicated circular referencing.
        /// A->B, B->C, C->D, E->D, G->E, D->A ==> D is circular referencing.
        /// </summary>
        [Test]
        public void TestComplicatedCircularReference()
        {
            TheCell a = this.sp.Cells[20, 20];
            TheCell b = this.sp.Cells[20, 21];
            TheCell c = this.sp.Cells[20, 22];
            TheCell d = this.sp.Cells[20, 23];
            TheCell e = this.sp.Cells[20, 24];
            TheCell g = this.sp.Cells[20, 25];
            string aName = a.Name;
            string bName = b.Name;
            string cName = c.Name;
            string dName = d.Name;
            string eName = e.Name;
            string gName = g.Name;
            a.Text = "=" + bName;
            b.Text = "=" + cName;
            c.Text = "=" + dName;
            e.Text = "=" + dName;
            g.Text = "=" + eName;
            d.Text = "=" + aName;
            Assert.AreEqual("0", a.Value);
            Assert.AreEqual("0", b.Value);
            Assert.AreEqual("0", c.Value);
            Assert.AreEqual("0", e.Value);
            Assert.AreEqual("0", g.Value);
            Assert.AreEqual("!(Circular Reference)", d.Value);
        }
 
        /// <summary>
        /// Test multiple invlid reference occur in a cell, machanism is to display the invalid message
        /// self reference and bad reference in precedance level and circular reference in secondary.
        /// </summary>
        [Test]

        public void TestMulInvalidInACell()
        {
            TheCell curCell = this.sp.Cells[0, 0];
            TheCell pCell = this.sp.Cells[1, 1];

            // bad reference will be first detected before the self reference.
            curCell.Text = "=" + pCell.Name + "+GG-" + curCell.Name;
            Assert.AreEqual("!(Bad Reference)", curCell.Value);

            // self reference will be first detected before the bad reference.
            curCell.Text = "=" + pCell.Name + "+" + curCell.Name + "-GG";
            Assert.AreEqual("!(Self Reference)", curCell.Value);

            // bad reference will be first detected before the circular reference.
            pCell.Text = "=" + curCell.Name + "+HHH";
            Assert.AreEqual("!(Bad Reference)", pCell.Value);
        }
    }
}
