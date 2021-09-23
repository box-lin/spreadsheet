// <copyright file="Form1.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW3_NotePad
{
    /// <summary>
    /// Form components.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Action event when click the "save to file" menuitem, proceed to save the contents from textbox to a plain text file.
        /// </summary>
        /// <param name="sender"> Object sender. </param>
        /// <param name="e"> Event. </param>
        private void SaveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // if the textBox is not empty (spaces count as empty) we will do the save file actions.
            if (this.textBox.Text.Trim() != string.Empty)
            {
                this.saveFileDialog.Filter = "Plain Text File(*.txt)|*.txt";

                if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = this.saveFileDialog.FileName;
                    string contents = this.textBox.Text;
                    StreamWriter sw = new StreamWriter(path);
                    sw.Write(contents);
                    sw.Flush();
                    sw.Close();
                }
            }
            else
            {
                // MessageBox warning users about no inputs.
                MessageBox.Show("No input contents received", "Warning:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load the file.
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Event. </param>
        private void FileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog.Filter = "Plain Text File(*.txt)|*.txt";
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = this.openFileDialog.FileName;
                StreamReader sr = new StreamReader(path);
                this.LoadText(sr);
                sr.Close();
            }
        }

        /// <summary>
        /// Read text from a file and put the contents into the textBox.
        /// </summary>
        /// <param name="sr"> TextReader object. </param>
        private void LoadText(TextReader sr)
        {
            this.textBox.Text = sr.ReadToEnd();
        }
    }
}
