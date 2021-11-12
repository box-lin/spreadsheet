// <copyright file="TextCommand.cs" company="Boxiang Lin - WSU 011601661">
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
    /// Text Change Command Class.
    /// </summary>
    public class TextCommand : ICommand
    {
        /// <summary>
        /// Cell for the text change.
        /// </summary>
        private TheCell cell;

        /// <summary>
        /// New value for the cell text.
        /// </summary>
        private string newValue;

        /// <summary>
        /// Previous value for the cell.
        /// </summary>
        private string prevValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextCommand"/> class.
        /// </summary>
        /// <param name="cell"> selected cell. </param>
        /// <param name="newValue"> new value. </param>
        public TextCommand(TheCell cell, string newValue)
        {
            this.cell = cell;
            this.newValue = newValue;
            this.SetPrevValue();
        }

        /// <summary>
        /// Execute the text change.
        /// </summary>
        public void Execute()
        {
            this.cell.Text = this.newValue;
        }

        /// <summary>
        /// Unexecute the text change.
        /// </summary>
        public void Unexecute()
        {
            this.cell.Text = this.prevValue;
        }

        /// <summary>
        /// Diplay the description of the command.
        /// </summary>
        /// <returns> string description. </returns>
        public override string ToString()
        {
            return "The Text Change Command";
        }

        /// <summary>
        /// Set the prev value with cells text.
        /// </summary>
        private void SetPrevValue()
        {
            this.prevValue = this.cell.Text;
        }
    }
}
