// <copyright file="Program.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CptS321;

namespace ExpressionTreeDemo
{
    /// <summary>
    /// Program that to test the functionalities of ExpressionTree.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entrance.
        /// </summary>
        /// <param name="args"> args. </param>
        public static void Main(string[] args)
        {
            string expression = "A1+B1+C1";
            ExpressionTree exp = new ExpressionTree(expression);
            bool run = true;
            while (run)
            {
                Menu(expression);
                string command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        Console.Write("Enter new expression: ");
                        expression = Console.ReadLine();
                        exp = new ExpressionTree(expression);
                        break;
                    case "2":
                        Console.Write("Enter variable name: ");
                        string variable = Console.ReadLine();
                        Console.Write("Enter variable value: ");
                        double constant = double.Parse(Console.ReadLine());
                        exp.SetVariable(variable, constant);
                        break;
                    case "3":
                        Console.WriteLine(exp.Evaluate());
                        break;
                    case "4":
                        run = false;
                        Console.WriteLine("Done");
                        break;
                }
            }

            // to keep the console on when quit so user can review.
            Console.ReadLine();
        }

        /// <summary>
        /// Print the menu.
        /// </summary>
        /// <param name="expression"> current expression. </param>
        private static void Menu(string expression)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Menu (current expression=\"" + expression + "\")");
            sb.AppendLine("  1 = Enter a new expression");
            sb.AppendLine("  2 = Set a variable value");
            sb.AppendLine("  3 = Evaluate tree");
            sb.AppendLine("  4 = Quit");
            Console.Write(sb.ToString());
        }
    }
}
