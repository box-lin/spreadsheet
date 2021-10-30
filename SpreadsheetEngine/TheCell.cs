// <copyright file="TheCell.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
using CptS321;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Class inherited from abstract class Cell.
    /// </summary>
    public class TheCell : Cell
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
        /// Event for referenced cells.
        /// </summary>
        public event EventHandler RefCellValueChanged;

        /// <summary>
        /// Subscribes ref cell to the propertychanged event of another cell.
        /// </summary>
        /// <param name="cell">Dependee cell.</param>
        public void SubToCellPropertyChange(Cell cell)
        {
            cell.PropertyChanged += this.OnRefCellValueChange;
        }

        /// <summary>
        /// Invoke event for change.
        /// </summary>
        /// <param name="sender">ref cell.</param>
        /// <param name="e">Event.</param>
        private void OnRefCellValueChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Value"))
            {
                this.RefCellValueChanged?.Invoke(this, new EventArgs());
            }
        }
    }
}
