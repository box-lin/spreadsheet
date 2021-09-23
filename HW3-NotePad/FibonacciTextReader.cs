// <copyright file="FibonacciTextReader.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HW3_NotePad
{
    /// <summary>
    /// Fibonacci Class.
    /// </summary>
    public class FibonacciTextReader : TextReader
    {
        private int maxLines;

        /// <summary>
        /// Initializes a new instance of the <see cref="FibonacciTextReader"/> class.
        /// Default constructor.
        /// </summary>
        public FibonacciTextReader()
        {
            this.maxLines = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FibonacciTextReader"/> class.
        /// </summary>
        /// <param name="maxLines"> Max number of line for fibonacci numbers. </param>
        public FibonacciTextReader(int maxLines)
        {
            this.maxLines = maxLines;
        }

        /// <summary>
        /// Override the ReadLine method in TextReader.
        /// </summary>
        /// <returns> A line of Fibonacci number. </returns>
        public override string ReadLine()
        {
            return "dsad";
        }

        /// <summary>
        /// This is a method that return the nth fibonacci value.
        /// </summary>
        /// <param name="n"> nth number. </param>
        /// <returns> the fibonacci value at nth number. </returns>
        public BigInteger Fib(BigInteger n)
        {
            return 1;
        }
    }
}
