// <copyright file="Form1.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spreadsheet_Boxiang_Lin
{
    public partial class Form1 : Form
    {
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
            this.ResetDataGridView();
            this.InitColumns('A', 'Z');
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
