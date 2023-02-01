using SSharp.VM;

namespace SSharp.Tokens
{
    public class AndOperator : Operator
    {
        public override string ToString()
        {
            return "and";
        }

        public override VMObject Operate(VMObject a, VMObject b)
        {
            if (!a.IsTruthy())
                return a;

            if (!b.IsTruthy())
                return b;

            return b;
        }
    }
}
