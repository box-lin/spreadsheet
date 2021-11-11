// <copyright file="ColorCommand.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Class that perform the change of cell's background color.
    /// </summary>
    public class ColorCommand : ICommand
    {
        /// <summary>
        /// Cells that for perform the color command.
        /// </summary>
        private List<TheCell> cells;

        /// <summary>
        /// new color get from UI.
        /// </summary>
        private uint newColor;

        /// <summary>
        /// previous cell color lookup.
        /// </summary>
        private Dictionary<TheCell, uint> prevColors = new Dictionary<TheCell, uint>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorCommand"/> class.
        /// </summary>
        /// <param name="cells"> cells that got selected by UI. </param>
        /// <param name="newColor"> new color that got selected by UI. </param>
        public ColorCommand(List<TheCell> cells, uint newColor)
        {
            this.cells = cells;
            this.newColor = newColor;
            this.SetPrevColors();
        }

        /// <summary>
        /// Execute the color change.
        /// </summary>
        public void Execute()
        {
            foreach (TheCell curCell in this.cells)
            {
                curCell.BGColor = this.newColor;
            }
        }

        /// <summary>
        /// Redo to previous color.
        /// </summary>
        public void Unexecute()
        {
            foreach (TheCell cell in this.cells)
            {
                cell.BGColor = this.prevColors[cell];
            }
        }

        /// <summary>
        /// Update the prevColor dictionary.
        /// </summary>
        private void SetPrevColors()
        {
            foreach (TheCell cell in this.cells)
            {
                this.prevColors.Add(cell, cell.BGColor);
            }
        }
    }
}
