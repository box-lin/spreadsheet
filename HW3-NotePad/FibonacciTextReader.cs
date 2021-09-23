﻿// <copyright file="FibonacciTextReader.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections;
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
