// <copyright file="Form1.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
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

        /// <summary>
        /// This is the load method that execute when program is run.
        /// This method does the calling of compute number of distinct value and setup the result to the textBox.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> event. </param>
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

            // Use StringBuilder to collect the output info
            StringBuilder output = new StringBuilder();

            output.AppendLine("1. HashSet Method: " + distinctByHashSet + " of unique numbers. ");
            output.AppendLine("It takes O(N) time complexity, where N = size of the random list, to visit all values in the random list. During each " +
                              "visit, I use the build-in hashset add method to add each value into the HashSet data structure, the build-in add will " +
                              "determine if the value is already present in the set or not and add if not present before, this build-in add time complexity " +
                              "is O(1). Hence O(N*1) = O(N) runtime complexity and O(M) space complexity, where M has 0<=M <= N possibilities.");
            output.AppendLine();
            output.AppendLine("2. O(1) Storage Method: " + distinctByOneSpace + " of unique numbers. ");
            output.AppendLine();
            output.AppendLine("3. Sorted Method: " + distinctBySort + " of unique numbers. ");

            // Pass StrigBuilder toString to the Text attribute in textBox
            this.textBox1.Text = output.ToString();
        }
    }
}
