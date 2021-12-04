// <copyright file="Form1.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CptS321;
using SpreadsheetEngine;

namespace Spreadsheet_Boxiang_Lin
{
    /// <summary>
    /// GUI Form Class.
    /// </summary>
    public partial class Form1 : Form
    {
        private Spreadsheet spreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Load event handler.
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Event. </param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.Rows.Clear();
            this.InitColumns('A', 'Z');
            this.InitRows(1, 50);
            this.spreadsheet = new Spreadsheet(50, 26);
            this.spreadsheet.CellPropertyChanged += this.OnCellPropertyChanged;
            this.dataGridView1.CellBeginEdit += this.DataGridView1_CellBeginEdit;
            this.dataGridView1.CellEndEdit += this.DataGridView1_CellEndEdit;
            this.SetUndoRedoMeanuVisibilityAndInfo();
            this.dataGridView1.ClearSelection();
        }

        /// <summary>
        /// Update the dataGridView as cell property changed.
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Event. </param>
        private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            TheCell curCell = sender as TheCell;
            int col = (int)curCell.ColumnIndex - 'A';
            DataGridViewCell formCell = this.dataGridView1.Rows[curCell.RowIndex].Cells[col];
            if (e.PropertyName == "Value")
            {
                formCell.Value = curCell.Value;
            }

            if (e.PropertyName == "BGColor")
            {
                Color newColor = Color.FromArgb((int)curCell.BGColor);
                formCell.Style.BackColor = newColor;
            }
        }

        /// <summary>
        /// BeginEdit event handler.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> event. </param>
        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            TheCell cell = this.spreadsheet.GetCell(row, col);

            // Get the selected cell.
            DataGridViewCell dataCell = this.dataGridView1.Rows[row].Cells[col];

            // default cell.text is string.empty and let the dataCell to be string.empty as well.
            dataCell.Value = cell.Text;
        }

        /// <summary>
        /// EndEdit event handler.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> event. </param>
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            TheCell cell = this.spreadsheet.GetCell(row, col);
            DataGridViewCell dataCell = this.dataGridView1.Rows[row].Cells[col];

            // If new value we type into dataCell different than existing cell text, we want to make update our local cell.
            if ((dataCell.Value != null && cell.Text != dataCell.Value.ToString()) || (dataCell.Value != null && dataCell.Value.ToString().StartsWith("=")))
            {
                string newValue = dataCell.Value.ToString();
                TextCommand command = new TextCommand(cell, newValue);
                this.spreadsheet.NewCommandAdd(command);
                this.SetUndoRedoMeanuVisibilityAndInfo();
                dataCell.Value = cell.Value;
            }
        }

        /// <summary>
        /// Event for DEMO button click handler.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> event.</param>
        private void Button1_Click(object sender, EventArgs e)
        {
            // To set text for B column and apply formula to map text in B column to A column.
            for (int row = 0; row < this.spreadsheet.RowCount; row++)
            {
                this.spreadsheet.Cells[row, 1].Text = "This is cell B" + (row + 1);
            }

            for (int row = 0; row < this.spreadsheet.RowCount; row++)
            {
                this.spreadsheet.Cells[row, 0].Text = "=B" + (row + 1);
            }

            // Random 50 cells text set to "I love C#".
            Random random = new Random();
            for (int i = 0; i < 50; i++)
            {
                // location 0<=row<50, 2<=col<26 where row and col start at 0 as in cell 2D array.
                this.spreadsheet.Cells[random.Next(0, 50), random.Next(2, 26)].Text = "I love C#";
            }
        }

        /// <summary>
        /// Intial the rows in range [x,y].
        /// </summary>
        /// <param name="x"> The start number of row. </param>
        /// <param name="y"> The end number of row. </param>
        private void InitRows(int x, int y)
        {
            for (int i = x; i <= y; i++)
            {
                this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[i - 1].HeaderCell.Value = i.ToString();
            }
        }

        /// <summary>
        /// Initial the columns in range [x,y].
        /// </summary>
        /// <param name="x"> The start column. </param>
        /// <param name="y"> The end column. </param>
        private void InitColumns(char x, char y)
        {
            for (char i = x; i <= y; i++)
            {
                this.dataGridView1.Columns.Add(i.ToString(), i.ToString());
            }
        }

        /// <summary>
        /// RefactorySpreadsheet.
        /// </summary>
        private void RefactorySpreadsheet()
        {
            this.spreadsheet.RefactorySpreadsheet();
            this.SetUndoRedoMeanuVisibilityAndInfo();
            this.dataGridView1.ClearSelection();
        }

        /// <summary>
        /// Event handler for click the Change Background Color.
        /// Select the background color from dialog window.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> event. </param>
        private void ChangeTheColorForAllSelectedCellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog myDialog = new ColorDialog();
            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                uint newColor = (uint)myDialog.Color.ToArgb();
                List<TheCell> selectedCells = new List<TheCell>();
                foreach (DataGridViewCell formCell in this.dataGridView1.SelectedCells)
                {
                    TheCell curCell = this.spreadsheet.GetCell(formCell.RowIndex, formCell.ColumnIndex);

                    // only that local cell color different than new color should get color updated.
                    if (curCell.BGColor != newColor)
                    {
                        selectedCells.Add(curCell);
                    }
                }

                // if numbers of cell selected greater than 0, means some cell we have to color update.
                // then, create command and push it to undo stack.
                if (selectedCells.Count > 0)
                {
                    ColorCommand colorCommand = new ColorCommand(selectedCells, newColor);
                    this.spreadsheet.NewCommandAdd(colorCommand);
                    this.SetUndoRedoMeanuVisibilityAndInfo();
                }
            }
        }

        /// <summary>
        /// Undo event handler.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> event. </param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadsheet.RunUndoCommand();
            this.SetUndoRedoMeanuVisibilityAndInfo();
        }

        /// <summary>
        /// Redo event handler.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> event. </param>
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadsheet.RunRedoCommand();
            this.SetUndoRedoMeanuVisibilityAndInfo();
        }

        /// <summary>
        /// Set the enable or disable for redo and undo menu depending on size of stacks.
        /// Change to correct menu text when Redo and Undo are functional.
        /// </summary>
        private void SetUndoRedoMeanuVisibilityAndInfo()
        {
            if (this.spreadsheet.IsEmptyRedoStack())
            {
                this.redoToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.redoToolStripMenuItem.Enabled = true;
                this.redoToolStripMenuItem.Text = "Redo " + this.spreadsheet.GetRedoCommandInfo();
            }

            if (this.spreadsheet.IsEmptyUndoStack())
            {
                this.undoToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.undoToolStripMenuItem.Enabled = true;
                this.undoToolStripMenuItem.Text = "Undo " + this.spreadsheet.GetUndoCommandInfo();
            }
        }

        /// <summary>
        /// Load XML event.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> event. </param>
        private void LoadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "XML files | *.xml";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                Stream s = openDialog.OpenFile();
                this.RefactorySpreadsheet();
                this.spreadsheet.LoadFromXml(s);
                this.SetUndoRedoMeanuVisibilityAndInfo();
                s.Close();
            }
        }

        /// <summary>
        /// Save to XML event.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> event. </param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveToXML();
        }

        /// <summary>
        /// SaveToXML method.
        /// To use by new spreadsheet message dialog and UI menu button.
        /// </summary>
        private void SaveToXML()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "XML files | *.xml";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                Stream s = saveDialog.OpenFile();
                this.spreadsheet.SaveToXML(s);
                s.Close();
            }
        }

        /// <summary>
        /// Event handler to support new spreadsheet application.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> event. </param>
        private void NewSpreadsheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.spreadsheet.IsEmptyUndoStack())
            {
                this.RefactorySpreadsheet();
            }
            else
            {
                DialogResult res = MessageBox.Show("You spreadsheet has been modified. Do you want to save? ", "Notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if ((int)res == 1)
                {
                    this.SaveToXML();
                    this.RefactorySpreadsheet();
                }
                else
                {
                    this.RefactorySpreadsheet();
                }
            }
        }
    }
}
