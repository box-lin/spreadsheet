// <copyright file="Spreadsheet.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CptS321;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Spreadsheet class.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// Public Required 2D Array stored the cells.
        /// </summary>
        #pragma warning disable SA1401 // Fields should be public so can be use in Form1.cs
        public Cell[,] Cells;
        #pragma warning restore SA1401
        private int columnCount;
        private int rowCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="row"> row number. </param>
        /// <param name="col"> column number. </param>
        public Spreadsheet(int row, int col)
        {
            this.CellsInit(row, col);
            this.columnCount = col;
            this.rowCount = row;
        }

        /// <summary>
        /// CellPropertyChanged Event Handler.
        /// </summary>
        public event PropertyChangedEventHandler CellPropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets the columnCount.
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return this.columnCount;
            }
        }

        /// <summary>
        /// Gets the rowCount.
        /// </summary>
        public int RowCount
        {
            get
            {
                return this.rowCount;
            }
        }

        /// <summary>
        /// Get Cell by location.
        /// </summary>
        /// <param name="row"> row. </param>
        /// <param name="col"> col. </param>
        /// <returns> Specific Cell. </returns>
        public Cell GetCell(int row, int col)
        {
            return row >= 0 && row < this.rowCount && col >= 0 && col < this.rowCount ? this.Cells[row, col] : null;
        }

        /// <summary>
        /// Init the 2D cell elements and configure the CellPropertyChange event for each cell in array.
        /// </summary>
        /// <param name="row"> row number. </param>
        /// <param name="col"> column number. </param>
        private void CellsInit(int row, int col)
        {
            this.Cells = new Cell[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    char colIndex = (char)('A' + j);
                    this.Cells[i, j] = new TheCell(i, colIndex);

                    // The spreadsheet class has to subscribe to all the PropertyChanged events
                    this.Cells[i, j].PropertyChanged += this.OnCellPropertyChanged;
                }
            }
        }

        /// <summary>
        /// Handler the event if Cell.text changed.
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Event. </param>
        private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                TheCell curCell = (TheCell)sender;
                string newVal = curCell.Text;

                // For formula.
                if (curCell.Text[0] == '=')
                {
                    newVal = this.EvaluateExpression(curCell);
                }

                // For just text.
                curCell.SetValue(newVal);
                this.CellPropertyChanged(curCell, new PropertyChangedEventArgs("Value"));
            }
        }

        /// <summary>
        /// Evaluate the formula expression in a cell.
        /// </summary>
        /// <param name="curCell"> TheCell object. </param>
        /// <returns> Return the string evaluated result. </returns>
        private string EvaluateExpression(TheCell curCell)
        {
            string expression = curCell.Text.Substring(1);
            
            // ExpressionTree gets build
            ExpressionTree expTree = new ExpressionTree(expression);
            return expTree.Evaluate().ToString();
        }

        private void GetVariableCell(ExpressionTree expTree, TheCell cell)
        {

        }

        /// <summary>
        /// Private class inherited from abstract class Cell.
        /// </summary>
        private class TheCell : Cell
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TheCell"/> class.
            /// </summary>
            /// <param name="rowIndex"> row index. </param>
            /// <param name="colIndex"> column index. </param>
            public TheCell(int rowIndex, char colIndex)
                : base(rowIndex, colIndex)
            {
            }

            /// <summary>
            /// Set the value.
            /// </summary>
            /// <param name="value"> string value. </param>
            public void SetValue(string value)
            {
                this.value = value;
            }
        }
    }
}
