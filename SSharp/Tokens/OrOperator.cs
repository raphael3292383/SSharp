using SSharp.VM;

namespace SSharp.Tokens
{
    public class OrOperator : Operator
    {
        public override string ToString()
        {
            return "or";
        }

        public override VMObject Operate(VMObject a, VMObject b)
        {
            if (a.IsTruthy())
                return a;
            else
                return b;
        }
    }
}
