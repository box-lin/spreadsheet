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
        /// 2D Array stored the cells.
        /// </summary>
        public Cell[,] Cells;
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
                    this.Cells[i, j] = new TheCell(i + 1, colIndex);
                    this.Cells[i, j].PropertyChanged += this.CellPropertyChanged;
                }
            }
        }

        /// <summary>
        /// Handline the event if Cell.text changed.
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Event. </param>
        private void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
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
            return row >= 0 && row <= this.rowCount && col >= 0 && col <= this.rowCount ? this.Cells[row, col] : null;
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
        }

    }
}
