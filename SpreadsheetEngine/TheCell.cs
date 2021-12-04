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
    }
}
