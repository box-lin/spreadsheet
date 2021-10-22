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
        /// Holds valid operators char and type.
        /// </summary>
        private Dictionary<char, Type> operators;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpNodeFactory"/> class.
        /// </summary>
        public OpNodeFactory()
        {
            this.operators = new Dictionary<char, Type>();
            this.TraverseAvailableOperators((op, type) => this.operators.Add(op, type));
        }

        /// <summary>
        /// Delegate for operator char and type.
        /// </summary>
        /// <param name="op">Operater character.</param>
        /// <param name="type">Operator type.</param>
        private delegate void OnOperator(char op, Type type);

        /// <summary>
        /// Takes an operator char and returns coresponding operator node.
        /// Accepts all operators that are defined which inherit the OperatorNode class.
        /// </summary>
        /// <param name="op">Operator.</param>
        /// <returns>Operator node.</returns>
        public OpNode CreateOperatorNode(char op)
        {
            if (this.operators.ContainsKey(op))
            {
                object operatorNodeObject = System.Activator.CreateInstance(this.operators[op]);
                if (operatorNodeObject is OpNode)
                {
                    return (OpNode)operatorNodeObject;
                }
            }

            throw new NotSupportedException("Not Supported Operator");
        }

        /// <summary>
        /// Returns if the operator is supported.
        /// </summary>
        /// <param name="op">Operator char.</param>
        /// <returns>Boolean.</returns>
        internal bool IsOperator(char op)
        {
            return this.operators.ContainsKey(op);
        }

        /// <summary>
        /// Traverses all assemblies for subclasses of OperatorNode
        /// and adds their Operator property and type to operators dictionary.
        /// </summary>
        /// <param name="onOperator">Delegate.</param>
        private void TraverseAvailableOperators(OnOperator onOperator)
        {
            Type operatorNodeType = typeof(OpNode);

            // Load outside assembly purpose.
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                IEnumerable<Type> operatorTypes = assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));

                foreach (var type in operatorTypes)
                {
                    PropertyInfo operatorField = type.GetProperty("Operator");

                    if (operatorField != null)
                    {
                        object value = operatorField.GetValue(type);

                        if (value is char)
                        {
                            char operatorSymbol = (char)value;
                            onOperator(operatorSymbol, type);
                        }
                    }
                }
            }
        }
    }
}
