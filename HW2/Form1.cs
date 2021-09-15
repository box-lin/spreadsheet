// <copyright file="Form1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HW2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// A Form1 class that extends from Form.
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

        private void Form1_Load(object sender, EventArgs e)
        {
            // Generate and return a random list.
            List<int> rdList = Utils.GetGenerateRandomList(0, 20000, 10000);

            // Get the number of distinct value through Hashset filter.
            int distinctByHashSet = Utils.GetByHashSetDistinct(rdList);

            // Get the number of distinct value through constant space algorithm.
            int distinctByOneSpace = Utils.GetByConstantSpaceDistinct(rdList);

            // Get the number of distinct value through sorting technique.
            int distinctBySort = Utils.GetBySortDisdinct(rdList);

            StringBuilder output = new StringBuilder();
            this.textBox1.Text = "1. HashSet method: ";
        }
    }
}
