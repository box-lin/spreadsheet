// <copyright file="TestSpreadsheet.cs" company="Boxiang Lin - WSU 011601661">
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
            Assert.AreEqual(0, this.ss.Cells[0, 25].RowIndex);
            Assert.AreEqual(1, this.ss.Cells[1, 25].RowIndex);
            Assert.AreEqual(4, this.ss.Cells[4, 20].RowIndex);
            Assert.AreEqual(47, this.ss.Cells[47, 25].RowIndex);
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

        /// <summary>
        /// Test the NewCommandAdd method.
        /// </summary>
        [Test]
        public void TestNewCommandAdd()
        {
            Spreadsheet ss = new Spreadsheet(50, 26);
            Assert.AreEqual(true, ss.IsEmptyUndoStack());
            CommandItem command = new CommandItem();
            ss.NewCommandAdd(command);
            Assert.AreEqual(false, ss.IsEmptyUndoStack());
        }

        /// <summary>
        /// Test the RunUndoCommand.
        /// </summary>
        [Test]
        public void TestRunUndoCommand()
        {
            Spreadsheet ss = new Spreadsheet(50, 26);
            CommandItem command = new CommandItem();
            ss.NewCommandAdd(command);
            ss.RunUndoCommand();
            Assert.AreEqual(true, ss.IsEmptyUndoStack());
        }

        /// <summary>
        /// Test the RunRedoCommand.
        /// </summary>
        [Test]
        public void TestRunRedoCommand()
        {
            Spreadsheet ss = new Spreadsheet(50, 26);
            CommandItem command = new CommandItem();
            ss.NewCommandAdd(command);
            ss.RunUndoCommand();

            // undo run, redo available.
            Assert.AreEqual(false, ss.IsEmptyRedoStack());

            // redo, then undo can be available again
            ss.RunRedoCommand();
            Assert.AreEqual(false, ss.IsEmptyUndoStack());
        }

        /// <summary>
        /// Test the functionalities of GetCommandInfo.
        /// </summary>
        public void TestCommandInfo()
        {
            Spreadsheet ss = new Spreadsheet(50, 26);
            CommandItem command = new CommandItem();
            ss.NewCommandAdd(command);
            Assert.AreEqual("default command", ss.GetUndoCommandInfo());

            this.ss.RunUndoCommand();
            Assert.AreEqual("default command", ss.GetRedoCommandInfo());
        }

        /// <summary>
        /// This is just command item use to test the redo and undo stack.
        /// </summary>
        private class CommandItem : ICommand
        {
            /// <summary>
            /// default non implementation.
            /// </summary>
            public void Execute()
            {
            }

            /// <summary>
            /// default non implementation.
            /// </summary>
            public void Unexecute()
            {
            }

            /// <summary>
            /// tostring method.
            /// </summary>
            /// <returns> default message. </returns>
            public override string ToString()
            {
                return "default command";
            }
        }
    }
}
