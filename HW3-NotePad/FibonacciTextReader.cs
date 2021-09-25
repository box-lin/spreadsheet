// <copyright file="FibonacciTextReader.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Numerics;
using System.Text;

namespace HW3_NotePad
{
    /// <summary>
    /// Fibonacci Class.
    /// </summary>
    public class FibonacciTextReader : TextReader
    {
        // To support large than 32 bits Integer position(lines).
        private BigInteger maxLine;
        private BigInteger curLine;

        /// <summary>
        /// Initializes a new instance of the <see cref="FibonacciTextReader"/> class.
        /// Default constructor.
        /// </summary>
        public FibonacciTextReader()
        {
            this.maxLine = 0;
            this.curLine = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FibonacciTextReader"/> class.
        /// </summary>
        /// <param name="maxLine"> Max number of line for fibonacci numbers. </param>
        public FibonacciTextReader(BigInteger maxLine)
        {
            this.maxLine = maxLine;
            this.curLine = 1;
        }

        /// <summary>
        /// Override the ReadLine method inherited from TextReader.
        /// </summary>
        /// <returns> A line of Fibonacci number. </returns>
        public override string ReadLine()
        {
            if (this.curLine > this.maxLine)
            {
                return null;
            }

            return this.curLine.ToString() + ": " + this.Fib(this.curLine).ToString();
        }

        /// <summary>
        /// Override the ReadToEnd method inherited from TextReader.
        /// </summary>
        /// <returns> string set of output. </returns>
        public override string ReadToEnd()
        {
            StringBuilder output = new StringBuilder();
            while (this.curLine <= this.maxLine)
            {
                output.AppendLine(this.ReadLine());
                this.curLine++;
            }

            // rest counter.
            this.curLine = 1;
            return output.ToString();
        }

        /// <summary>
        /// This is a method that return the nth fibonacci value.
        /// </summary>
        /// <param name="n"> nth number. </param>
        /// <returns> the fibonacci value at nth number. </returns>
        public BigInteger Fib(BigInteger n)
        {
            if (n <= 0)
            {
                throw new ArgumentException("nth position of fibonacci number must > 0");
            }
            else
            {
                // We set the position 1 = 0 and position 2 = 1;
                BigInteger first = 0, second = 1;

                // if input is 1 then return res = 0 else res 1 (res will be change in loop so this handles when n = 1 and 2;
                BigInteger res = n == 1 ? first : second;

                // Position that are > 2, will be determine from this loop.
                // We want res = first + second (sum of prev two). So we must set bounday i to be at most n-1. res calculated at
                // n-1 will be the nth value because where the second has been a value of n-1 in prev iteration.
                for (BigInteger i = 2; i < n; i++)
                {
                    res = first + second;
                    first = second;
                    second = res;
                }

                return res;
            }
        }
    }
}
