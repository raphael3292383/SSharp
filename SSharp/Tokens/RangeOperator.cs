using SSharp.VM;
using System;

namespace SSharp.Tokens
{
    public class RangeOperator : Operator
    {
        public override string ToString()
        {
            return "..";
        }

        public override VMObject Operate(VMObject a, VMObject b)
        {
            if (a is VMNumber numA && b is VMNumber numB)
            {
                return new VMRange(numA.Value, numB.Value);
            }
            throw new Exception($"Cannot instantiate a new range from {a.ToString()} and {b.ToString()}.");
        }
    }
}
