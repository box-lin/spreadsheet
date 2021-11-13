// <copyright file="ICommand.cs" company="Boxiang Lin - WSU 011601661">
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
    /// ICommand interface.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Execute the command.
        /// </summary>
        void Execute();

        /// <summary>
        /// Unexecute the command.
        /// </summary>
        void Unexecute();
    }
}
