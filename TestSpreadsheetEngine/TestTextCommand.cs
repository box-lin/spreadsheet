using NUnit.Framework;
using SpreadsheetEngine;

namespace TestSpreadsheetEngine
{
    /// <summary>
    /// Test Text Command Class.
    /// </summary>
    [TestFixture]
    public class TestTextCommand
    {
        private Spreadsheet ss = new Spreadsheet(50,26);

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
            TheCell cell = this.ss.GetCell(1, 1);
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
