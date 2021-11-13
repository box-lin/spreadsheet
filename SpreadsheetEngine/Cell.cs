// <copyright file="Cell.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptS321
{
    /// <summary>
    /// Abstract.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// Text inside a cell.
        /// </summary>
        #pragma warning disable SA1401 // Fields should be protected so can be use in spreadsheet from derived class.
        protected string text;
        #pragma warning restore SA1401

        /// <summary>
        /// Value of a cell.
        /// </summary>
        #pragma warning disable SA1401 // Fields should be protected so can be use in spreadsheet from derived class.
        protected string value;
        #pragma warning restore SA1401

        /// <summary>
        /// value for the background color.
        /// </summary>
        #pragma warning disable SA1401 // Fields should be protected so can be use in spreadsheet from derived class.
        protected uint bgColor;
        #pragma warning restore SA1401

        private readonly int rowIndex;
        private readonly char columnIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="rowIndex"> rowIndex. </param>
        /// <param name="columnIndex"> columnIndex. </param>
        public Cell(int rowIndex, char columnIndex)
        {
            this.text = string.Empty;
            this.value = string.Empty;
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
            this.bgColor = 0xFFFFFFFF;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the rowIndex.
        /// </summary>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }

        /// <summary>
        /// Gets the columnIndex.
        /// </summary>
        public char ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (this.text == value)
                {
                    return;
                }

                this.text = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
            }
        }

        /// <summary>
        /// Gets or sets the bgColor.
        /// </summary>
        public uint BGColor
        {
            get
            {
                return this.bgColor;
            }

            set
            {
                if (this.bgColor == value)
                {
                    return;
                }

                this.bgColor = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BGColor"));
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }
        }

        /// <summary>
        /// Changes string field value to newValue.
        /// </summary>
        /// <param name="newValue">New string value.</param>
        internal void SetValue(string newValue)
        {
            this.value = newValue;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
        }
    }
}
