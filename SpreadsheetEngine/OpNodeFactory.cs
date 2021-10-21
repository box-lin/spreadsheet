// <copyright file="OpNodeFactory.cs" company="Boxiang Lin - WSU 011601661">
// Copyright (c) Boxiang Lin - WSU 011601661. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            this.InitOp();
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
        /// Use reflection to initil the dictionary.
        /// reference about access classes info in namespace: https://blog.csdn.net/huoliya12/article/details/78873123
        /// refrence about get type's property: https://www.codenong.com/1196991/.
        /// </summary>
        private void InitOp()
        {
            // For load assembly purpose.
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                IEnumerable<Type> operatorTypes = assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(OpNode)));
                foreach (var item in operatorTypes)
                {
                    if (item.IsSubclassOf(typeof(OpNode)))
                    {
                        char symbol = (char)item.GetProperty("Operator").GetValue(item);
                        OpNode curOp = (OpNode)System.Activator.CreateInstance(item);
                        this.op[symbol] = curOp;
                    }
                }
            }
        }
    }
}
