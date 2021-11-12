// <copyright file="Spreadsheet.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
#pragma warning disable SA1401 // Public as PDF instruction.
        public TheCell[,] Cells;
#pragma warning restore SA1401 // Public as PDF instruction.

        private int columnCount;
        private int rowCount;

        /// <summary>
        /// Undo stack to store specic command that implements the ICommand Interface.
        /// </summary>
        private Stack<ICommand> undos;

        /// <summary>
        /// Redo stack to store specifc command that implements the Icoomand Interface.
        /// </summary>
        private Stack<ICommand> redos;

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
            this.undos = new Stack<ICommand>();
            this.redos = new Stack<ICommand>();
        }

        /// <summary>
        /// CellPropertyChanged Event Handler.
        /// </summary>
        public event PropertyChangedEventHandler CellPropertyChanged;

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
        public TheCell GetCell(int row, int col)
        {
            return row >= 0 && row < this.rowCount && col >= 0 && col < this.rowCount ? this.Cells[row, col] : null;
        }

        /// <summary>
        /// When brand new command add, redo should be disable.
        /// By instruction, disable is to make the redo stack empty.
        /// </summary>
        /// <param name="command"> incoming command. </param>
        public void NewCommandAdd(ICommand command)
        {
            this.redos.Clear();
            command.Execute();
            this.undos.Push(command);
        }

        /// <summary>
        /// Undo Command execution.
        /// pop the undo stack, unexecute, and push it to redo stack.
        /// </summary>
        public void RunUndoCommand()
        {
            if (this.undos.Count > 0)
            {
                ICommand undoCommand = this.undos.Pop();
                undoCommand.Unexecute();
                this.redos.Push(undoCommand);
            }
        }

        /// <summary>
        /// Redo command execution.
        /// pop the redo stack, execute, and push it to undo stack.
        /// </summary>
        public void RunRedoCommand()
        {
            if (this.redos.Count > 0)
            {
                ICommand redoCommand = this.redos.Pop();
                redoCommand.Execute();
                this.undos.Push(redoCommand);
            }
        }

        /// <summary>
        /// Returning the info fo the command in string for UI to display.
        /// </summary>
        /// <returns> string description of the type. </returns>
        public string GetRedoCommandInfo()
        {
            return this.redos.Peek().ToString();
        }

        /// <summary>
        /// Returning the info fo the command in string for UI to display.
        /// </summary>
        /// <returns> string description of the command. </returns>
        public string GetUndoCommandInfo()
        {
            return this.undos.Peek().ToString();
        }

        /// <summary>
        /// Return bool empty if true otherwise false for use of UI determination.
        /// </summary>
        /// <returns> bool. </returns>
        public bool IsEmptyUndoStack()
        {
            return this.undos.Count <= 0;
        }

        /// <summary>
        /// Return bool empty if true otherwise false for use of UI determination.
        /// </summary>
        /// <returns> bool. </returns>
        public bool IsEmptyRedoStack()
        {
            return this.redos.Count <= 0;
        }


        /// <summary>
        /// Init the 2D cell elements and configure the CellPropertyChange event for each cell in array.
        /// </summary>
        /// <param name="row"> row number. </param>
        /// <param name="col"> column number. </param>
        private void CellsInit(int row, int col)
        {
            this.Cells = new TheCell[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    char colIndex = (char)('A' + j);
                    TheCell cell = new TheCell(i, colIndex);
                    this.Cells[i, j] = cell;
                    cell.PropertyChanged += this.OnCellPropertyChanged;
                    cell.RefCellValueChanged += this.OnRefCellValueChanged;
                }
            }
        }

        /// <summary>
        /// Cell property event listener. Update the value when text property get changed.
        /// </summary>
        /// <param name="sender"> object.</param>
        /// <param name="e"> event.</param>
        private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Text"))
            {
                this.SetCellValue(sender as TheCell);
            }
            else if (e.PropertyName.Equals("BGColor"))
            {
                this.CellPropertyChanged(sender as TheCell, new PropertyChangedEventArgs("BGColor"));
            }
        }

        /// <summary>
        /// Reference cell event listener. Update value as ref cell property changes.
        /// </summary>
        /// <param name="sender">Ref cell.</param>
        /// <param name="e">Event.</param>
        private void OnRefCellValueChanged(object sender, EventArgs e)
        {
            this.SetCellValue(sender as TheCell);
        }

        /// <summary>
        /// <Helper> Set the cell value and if do the expression computation. </Helper>
        /// Case 1: If start with =, use expression tree to evaluate the result.
        /// Case 2: Not start with =, just set text to value.
        /// </summary>
        /// <param name="cell">Spreadsheet cell.</param>
        private void SetCellValue(TheCell cell)
        {
            string newValue = cell.Text;

            if (newValue.StartsWith("="))
            {
                // remove = and whitespaces
                string expression = cell.Text.Substring(1).Replace(" ", string.Empty);
                ExpressionTree exp = new ExpressionTree(expression);
                bool error = this.SetVariable(exp, cell);
                if (!error)
                {
                    newValue = exp.Evaluate().ToString();
                }
                else
                {
                    newValue = cell.Value;
                }
            }

            cell.SetValue(newValue);
            this.CellPropertyChanged?.Invoke(cell, new PropertyChangedEventArgs("Value"));
        }

        /// <summary>
        /// Variable in the Expression tree set with value according to the ref cell.
        /// </summary>
        /// <param name="exp"> ExpressionTree. </param>
        /// <param name="currCell"> current cell.</param>
        /// <returns> error or no error. </returns>
        private bool SetVariable(ExpressionTree exp, TheCell currCell)
        {
            // all variable names captured during the construction of expression tree.
            HashSet<string> variableNames = exp.GetAllVariableName();
            foreach (string cellname in variableNames)
            {
                // Get the reference cell.
                TheCell refCell = this.GetCellByName(cellname);

                if (refCell == null)
                {
                    currCell.SetValue("Out Of Range");
                    return true;
                }
                else if (currCell == refCell)
                {
                    currCell.SetValue("Self Reference LATER");
                    return true;
                }
                else
                {
                    double num = 0.0;
                    if (double.TryParse(refCell.Value, out num))
                    {
                        num = double.Parse(refCell.Value);
                    }

                    exp.SetVariable(cellname, num);
                    currCell.SubToCellPropertyChange(refCell);
                }
            }

            return false;
        }

        /// <summary>
        /// As required, valName = [char][digit].
        /// </summary>
        /// <param name="valName"> char++digit. </param>
        /// <returns> a specific TheCell object in our 2D storage. </returns>
        private TheCell GetCellByName(string valName)
        {
            try
            {
                char col = valName[0];
                string row = valName.Substring(1);
                int colIndex = col - 'A';
                int rowIndex = int.Parse(row) - 1;
                return this.GetCell(rowIndex, colIndex);
            }
            catch
            {
                return null;
            }
        }
    }
}
