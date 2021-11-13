// <copyright file="TestColorCommand.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System.Collections.Generic;
using NUnit.Framework;
using SpreadsheetEngine;

namespace CptS321.Tests
{
    /// <summary>
    /// Test Text Command Class.
    /// </summary>
    [TestFixture]
    public class TestColorCommand
    {
        private Spreadsheet ss = new Spreadsheet(50, 26);

        /// <summary>
        /// Test the execute command.
        /// </summary>
        [Test]
        public void TestExecute()
        {
            List<TheCell> cells = new List<TheCell> { this.ss.GetCell(1, 1) };
            ColorCommand command = new ColorCommand(cells, 0xAAAAAAAA);
            command.Execute();
            Assert.AreEqual(0xAAAAAAAA, cells[0].BGColor);
        }

        /// <summary>
        /// Test the unexecute command.
        /// </summary>
        [Test]
        public void TestUnexecute()
        {
            List<TheCell> cells = new List<TheCell> { this.ss.GetCell(2, 2) };
            ColorCommand command = new ColorCommand(cells, 0xAAAAAAAA);
            command.Execute();
            command.Unexecute();
            Assert.AreEqual(0xFFFFFFFF, cells[0].BGColor);
        }

        /// <summary>
        /// Test the tostring method.
        /// </summary>
        [Test]
        public void TestToString()
        {
            List<TheCell> cells = new List<TheCell> { this.ss.GetCell(6, 6) };
            ColorCommand command = new ColorCommand(cells, 0xAAAAAAAA);
            Assert.AreEqual("The Background Color Change Command", command.ToString());
        }
    }
}
