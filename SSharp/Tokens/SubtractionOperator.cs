using SSharp.VM;
using System;

namespace SSharp.Tokens
{
    public class SubtractionOperator : Operator, IUnaryOperator
    {
        public override string ToString()
        {
            return "-";
        }

        VMObject IUnaryOperator.OperateUnary(VMObject x)
        {
            if (x is VMNumber num)
            {
                return new VMNumber(-num.Value);
            }
            throw new Exception($"Cannot subtract {x.ToString()}.");
        }

        public override VMObject Operate(VMObject a, VMObject b)
        {
            if (a is VMNumber numA && b is VMNumber numB)
            {
                return new VMNumber(numA.Value - numB.Value);
            }
            throw new Exception($"Cannot subtract {a.ToString()} and {b.ToString()}.");
        }
    }
}
