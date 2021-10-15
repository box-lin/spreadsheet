using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptS321
{
    /// <summary>
    /// Constant node.
    /// </summary>
    public class ConstantNode : Node
    {
        private readonly double val;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="val"> constant value. </param>
        public ConstantNode(double val)
        {
            this.val = val;
        }

        /// <summary>
        /// Evaluate the cosntant val.
        /// </summary>
        /// <returns> The costant val of the current node. </returns>
        public override double Evaluate()
        {
            return this.val;
        }
    }
}
