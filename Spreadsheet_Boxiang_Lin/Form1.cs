// <copyright file="Form1.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
            this.spreadsheet = new Spreadsheet(50, 26);
            this.spreadsheet.CellPropertyChanged += this.OnCellPropertyChanged;
            this.dataGridView1.CellBeginEdit += this.DataGridView1_CellBeginEdit;
            this.dataGridView1.CellEndEdit += this.DataGridView1_CellEndEdit;
        }

        /// <summary>
        /// Load event handler.
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Event. </param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.ResetDataGridView();
            this.InitColumns('A', 'Z');
            this.InitRows(1, 50);
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
            else if (e.PropertyName == "BGColor")
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

            // value match to cell text property.
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
            if (dataCell.Value != null)
            {
                string update = dataCell.Value.ToString();
                cell.Text = update;
            }

            dataCell.Value = cell.Value;
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
        /// A method that to reset the rows and columns of DataGridView.
        /// (Refer to .NET docs.)
        /// </summary>
        private void ResetDataGridView()
        {
            this.dataGridView1.CancelEdit();
            this.dataGridView1.Columns.Clear();
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
                    selectedCells.Add(curCell);
                }

                ColorCommand colorCommand = new ColorCommand(selectedCells, newColor);
                colorCommand.Execute();
            }
        }
    }
}
