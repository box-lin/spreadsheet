using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptS321
{
    public class PlusOp:OpNode
    {
        public static char Operator => '+';
        public static ushort Precedance => 7;

        public static Associative Associtivity = Associative.Left;

        public PlusOp()
        {
        }

        public override double Evaluate()
        {
            return this.Left.Evaluate() + this.Right.Evaluate();
        }


    }
}
