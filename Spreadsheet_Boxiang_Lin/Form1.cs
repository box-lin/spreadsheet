// <copyright file="Form1.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
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
        private readonly Spreadsheet spreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.spreadsheet = new Spreadsheet(50, 26);
            this.spreadsheet.CellPropertyChanged += this.OnCellPropertyChanged;
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
            if (e.PropertyName == "Value")
            {
                Cell curCell = (Cell)sender;
                int col = (int)curCell.ColumnIndex - 'A';

                // curCell val get set in Spreadsheet.
                this.dataGridView1.Rows[curCell.RowIndex].Cells[col].Value = curCell.Value;
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
                this.spreadsheet.Cells[row, 1].Text = "This is cell B" + (this.spreadsheet.Cells[row, 1].RowIndex + 1);
                this.spreadsheet.Cells[row, 0].Text = "=B" + this.spreadsheet.Cells[row, 1].RowIndex;
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
    }
}
