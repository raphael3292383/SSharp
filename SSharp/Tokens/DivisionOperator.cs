using SSharp.VM;
using System;

namespace SSharp.Tokens
{
    public class DivisionOperator : Operator
    {
        public override string ToString()
        {
            return "/";
        }

        public override VMObject Operate(VMObject a, VMObject b)
        {
            if (a is VMNumber numA && b is VMNumber numB)
            {
                if (numB.Value == 0)
                {
                    throw new DivideByZeroException($"Cannot divide {a.ToString()} by zero.");
                }
                return new VMNumber(numA.Value / numB.Value);
            }
            throw new Exception($"Cannot divide {a.ToString()} by {b.ToString()}.");
        }
    }
}
