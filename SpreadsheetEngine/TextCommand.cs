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
        /// Execute the text change.
        /// </summary>
        public void Execute()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Unexecute the text change.
        /// </summary>
        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
