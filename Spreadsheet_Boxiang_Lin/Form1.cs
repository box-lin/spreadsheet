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
        private Spreadsheet spreadsheet;

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
