using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CptS321;

namespace OperatorLibrary_ForTests
{
    public class PowOp : OpNode
    {
        public static new char Operator => '^';
        public override Associative Associativity => Associative.Right;
        public override int Precedence => 2;

        public override double Evaluate()
        {
            return Math.Pow(Left.Evaluate(),Right.Evaluate());
        }
    }
}
