// <copyright file="OpNodeFactory.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptS321
{
    /// <summary>
    /// OpNodeFactory class to dynamically instantiate operation classes.
    /// </summary>
    public class OpNodeFactory
    {
        /// <summary>
        /// Store.
        /// </summary>
        private Dictionary<char, OpNode> op;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpNodeFactory"/> class.
        /// </summary>
        public OpNodeFactory()
        {
            this.op = new Dictionary<char, OpNode>();
        }

        /// <summary>
        /// Gets provide a get Op dictionary for testing purpose.
        /// </summary>
        public Dictionary<char, OpNode> Op
        {
            get
            {
                return this.op;
            }
        }

        /// <summary>
        /// Create the operator object.
        /// </summary>
        /// <param name="symbol"> char symbol of the operator. </param>
        /// <returns> return a operator object or exception. </returns>
        public OpNode CreateOperatorNode(char symbol)
        {
            if (this.op.ContainsKey(symbol))
            {
                return this.op[symbol];
            }
            else
            {
                throw new NotSupportedException("The operation" + symbol.ToString() + "is not supoorted");
            }
        }

        /// <summary>
        /// Initil the dictionary.
        /// </summary>
        private void InitOp()
        {
            //this.op['+'] = new PlusOp();
        }

    }
}
