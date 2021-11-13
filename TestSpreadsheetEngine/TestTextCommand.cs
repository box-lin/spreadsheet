// <copyright file="TestTextCommand.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using NUnit.Framework;
using SpreadsheetEngine;

namespace CptS321.Tests
{
    /// <summary>
    /// Test Text Command Class.
    /// </summary>
    [TestFixture]
    public class TestTextCommand
    {
        private Spreadsheet ss = new Spreadsheet(50, 26);

        /// <summary>
        /// Test the execute command.
        /// </summary>
        [Test]
        public void TestExecute()
        {
            TheCell cell = this.ss.GetCell(1, 1);
            TextCommand command = new TextCommand(cell, "newText");
            command.Execute();
            Assert.AreEqual("newText", cell.Text);
        }

        /// <summary>
        /// Test the unexecute command.
        /// </summary>
        [Test]
        public void TestUnexecute()
        {
            TheCell cell = this.ss.GetCell(3, 3);
            TextCommand command = new TextCommand(cell, "newText");
            command.Execute();
            command.Unexecute();
            Assert.AreEqual(string.Empty, cell.Text);
        }

        /// <summary>
        /// Test the tostring method.
        /// </summary>
        [Test]
        public void TestToString()
        {
            TheCell cell = this.ss.GetCell(2, 2);
            TextCommand command = new TextCommand(cell, "newText");
            Assert.AreEqual("The Text Change Command", command.ToString());
        }
    }
}
