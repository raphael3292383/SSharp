using SSharp.VM;
using System;

namespace SSharp.Tokens
{
    public class MultiplicationOperator : Operator
    {
        public override string ToString()
        {
            return "*";
        }

        public override VMObject Operate(VMObject a, VMObject b)
        {
            if (a is VMNumber numA && b is VMNumber numB)
            {
                return new VMNumber(numA.Value * numB.Value);
            }
            throw new Exception($"Cannot multiply {a.ToString()} by {b.ToString()}.");
        }
    }
}
