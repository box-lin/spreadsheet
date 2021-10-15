using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptS321
{
    public abstract class OpNode : Node
    {
        public enum Associative
        {
            Right,
            Left
        }

        public Node Left { get; set; }
        public Node Right { get; set; }

    }
}
