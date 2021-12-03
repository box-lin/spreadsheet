// <copyright file="Spreadsheet.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
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
        /// dependencies dictionary to help get track of variable cells.
        /// </summary>
        private Dictionary<string, HashSet<string>> dependencies;

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
            this.dependencies = new Dictionary<string, HashSet<string>>();
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
        /// Load the xml to spreadsheet from stream.
        /// </summary>
        /// <param name="s"> stream. </param>
        public void LoadFromXml(Stream s)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(s);

            // root pick
            XmlNode root = xDoc.SelectSingleNode("spreadsheet");

            if (root == null)
            {
                return;
            }

            this.undos.Clear();
            this.redos.Clear();
            XmlNodeList childList = root.ChildNodes;

            // traverse through cells in spreadsheet.
            foreach (XmlNode child in childList)
            {
                XmlElement element = (XmlElement)child;

                // encounter a cell.
                if (element.Name == "cell")
                {
                    // retrieve the spreadsheet cell.
                    string cellname = element.GetAttribute("name");
                    TheCell cell = this.GetCellByName(cellname);

                    // traverse the attributes of a cell --> for cell's text and bgcolors.
                    foreach (XmlNode cchild in child.ChildNodes)
                    {
                        XmlElement childElement = (XmlElement)cchild;
                        if (childElement.Name == "bgcolor")
                        {
                            string color = childElement.InnerText;

                            // if no input in bgcolor tag, continue for next tag
                            if (color.Length == 0)
                            {
                                continue;
                            }

                            // if such bgcolor not in ARGB format we want its prefix to be 0 until most significant bit - A
                            while (color.Length < 6)
                            {
                                color = "0" + color;
                            }

                            // if such bgcolor not in ARGB we want its A(alpha component value) to be fully opaque.
                            while (color.Length < 8)
                            {
                                color = "F" + color;
                            }

                            uint newColor = Convert.ToUInt32(color, 16);
                            cell.BGColor = newColor;
                        }

                        if (childElement.Name == "text")
                        {
                            cell.Text = childElement.InnerText;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Spreadsheet save to XML from stream.
        /// </summary>
        /// <param name="s"> stream. </param>
        public void SaveToXML(Stream s)
        {
            XmlWriter wr = XmlWriter.Create(s);

            // root
            wr.WriteStartElement("spreadsheet");

            // refer to our local cell data.
            foreach (TheCell cell in this.Cells)
            {
                // if not default value we need to save info to the XML
                if (cell.Text != string.Empty || cell.BGColor != 0xFFFFFFFF)
                {
                    string cellname = cell.ColumnIndex + (cell.RowIndex + 1).ToString();

                    // second layer by cell.
                    wr.WriteStartElement("cell");
                    wr.WriteAttributeString("name", cellname);

                    // write the color first if not default.
                    if (cell.BGColor != 0xFFFFFFFF)
                    {
                        // third layer.
                        wr.WriteStartElement("bgcolor");

                        // for 16 bits int in string representation.
                        wr.WriteString(cell.BGColor.ToString("X"));
                        wr.WriteEndElement();
                    }

                    if (cell.Text != string.Empty)
                    {
                        // third layer.
                        wr.WriteStartElement("text");
                        wr.WriteString(cell.Text);
                        wr.WriteEndElement();
                    }

                    // end second layer.
                    wr.WriteEndElement();
                }
            }

            // close first layer.
            wr.WriteEndElement();
            wr.Close();
        }

        /// <summary>
        /// Refactory the spreadsheet cells.
        /// </summary>
        public void RefactorySpreadsheet()
        {
            // all cells goes to default values.
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                {
                    this.Cells[i, j].Text = string.Empty;
                    this.Cells[i, j].SetValue(string.Empty);
                    this.Cells[i, j].BGColor = 0xFFFFFFFF;
                }
            }

            // all undo redo goes to empty.
            this.undos.Clear();
            this.redos.Clear();
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
            if (e.PropertyName == "Text")
            {
                TheCell curCell = sender as TheCell;
                this.RemoveDependencies(curCell.Name);

                if (curCell.Text != string.Empty && curCell.Text[0] == '=')
                {
                    ExpressionTree exp = new ExpressionTree(curCell.Text.Substring(1).Replace(" ", string.Empty));
                    this.BuildDependencies(curCell.Name, exp.GetAllVariableName());
                }

                this.Evaluate(sender as TheCell);
            }
            else if (e.PropertyName == "BGColor")
            {
                this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("BGColor"));
            }
        }

        /// <summary>
        /// Every time the cell text event triggered, we will have all cell slot of dependencies that contains it.
        /// </summary>
        /// <param name="cellName"> the cell that we are changing. </param>
        private void RemoveDependencies(string cellName)
        {
            foreach (string key in this.dependencies.Keys)
            {
                if (this.dependencies[key].Contains(cellName))
                {
                    this.dependencies[key].Remove(cellName);
                }
            }
        }

        /// <summary>
        /// Build the current cell to dependet on all variables (reference cell) in its formula.
        /// </summary>
        /// <param name="cellName"> cell editing. </param>
        /// <param name="variablesUsed"> variables filter from the formula. </param>
        private void BuildDependencies(string cellName, HashSet<string> variablesUsed)
        {
            foreach (string v in variablesUsed)
            {
                if (this.dependencies.ContainsKey(v) == false)
                {
                    this.dependencies[v] = new HashSet<string>();
                }

                this.dependencies[v].Add(cellName);
            }
        }

        /// <summary>
        /// Evaluating the current editing cell. 1. With formula or 2. just plain text.
        /// </summary>
        /// <param name="cell"> current editing cell.</param>
        private void Evaluate(TheCell cell)
        {
            // null or empty
            if (string.IsNullOrEmpty(cell.Text))
            {
                cell.SetValue(string.Empty);
                this.CellPropertyChanged?.Invoke(cell, new PropertyChangedEventArgs("Value"));
            }

            // if it is an exp
            else if (cell.Text[0] == '=' && cell.Text.Length >= 2)
            {
                bool hasError = this.EvaluateHelper(cell);
                if (hasError)
                {
                    return;
                }
            }

            // others being text
            else
            {
                cell.SetValue(cell.Text);
                this.CellPropertyChanged?.Invoke(cell, new PropertyChangedEventArgs("Value"));
            }

            if (this.dependencies.ContainsKey(cell.Name))
            {
                foreach (var dependentCell in this.dependencies[cell.Name])
                {
                    this.Evaluate(this.GetCellByName(dependentCell));
                }
            }
        }

        /// <summary>
        /// A Helper method of evaluate for formulas. Check for 1. self reference, 2. bad reference, 3. circular reference.
        /// and 4. assign value to variables.
        /// </summary>
        /// <param name="cell"> current editing cell. </param>
        /// <returns> true or false. </returns>
        private bool EvaluateHelper(TheCell cell)
        {
            ExpressionTree exptree = new ExpressionTree(cell.Text.Substring(1));
            HashSet<string> variables = exptree.GetAllVariableName();

            foreach (string variableName in variables)
            {
                // bad reference
                if (this.GetCellByName(variableName) == null)
                {
                    cell.SetValue("!(Bad Reference)");
                    this.CellPropertyChanged?.Invoke(cell, new PropertyChangedEventArgs("Value"));
                    return true;
                }

                // self reference
                if (variableName == cell.Name)
                {
                    cell.SetValue("!(Self Reference)");
                    this.CellPropertyChanged?.Invoke(cell, new PropertyChangedEventArgs("Value"));
                    return true;
                }

                // circular reference.
                if (this.IsCircular(cell, variableName))
                {
                    cell.SetValue("!(Circular Reference)");
                    this.CellPropertyChanged?.Invoke(cell, new PropertyChangedEventArgs("Value"));
                    return true;
                }
            }

            // if we reach to this point that means no error found yet. we can assign value to variable for this operation.
            foreach (string variableName in variables)
            {
                // assign double value to variables.
                TheCell variableCell = this.GetCellByName(variableName);
                double value;

                if (string.IsNullOrEmpty(variableCell.Value))
                {
                    exptree.SetVariable(variableCell.Name, 0);
                }
                else if (!double.TryParse(variableCell.Value, out value))
                {
                    exptree.SetVariable(variableName, 0);
                }
                else
                {
                    exptree.SetVariable(variableName, value);
                }
            }

            // then we will evaluate the expression tree.
            cell.SetValue(exptree.Evaluate().ToString());
            this.CellPropertyChanged?.Invoke(cell, new PropertyChangedEventArgs("Value"));
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
                // to support lowercase cell name
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

        /// <summary>
        /// Check the current variables, then search through the dependent cells from dependency dictionary.
        /// </summary>
        /// <param name="cell"> cell editing. </param>
        /// <param name="varName"> one variable cell name. </param>
        /// <returns> true or false.  </returns>
        private bool IsCircular(TheCell cell, string varName)
        {
            if (varName == cell.Name)
            {
                return true;
            }

            if (!this.dependencies.ContainsKey(cell.Name))
            {
                return false;
            }

            // if we at this point means there is a dependency slot our current cell compose of.
            // we have to check its all dependents (values in a dict) will the deepest and fartest distance.
            string curname = cell.Name;
            return this.Dfs(curname, varName);
        }

        /// <summary>
        /// Depth first serch helper.
        /// Search over the dependent's dependent's dependent .....
        /// </summary>
        /// <param name="curname"> curcell (change-able by recursion). </param>
        /// <param name="vname"> variable cell name.</param>
        /// <returns> true or false. </returns>
        private bool Dfs(string curname, string vname)
        {
            if (vname == curname)
            {
                return true;
            }

            // need to visit each dependents.
            foreach (string dname in this.dependencies[curname])
            {
                if (this.dependencies.ContainsKey(dname))
                {
                    // search dependent's dependent's dependent..
                    return this.Dfs(dname, vname);
                }
            }

            return false;
        }
    }
}
