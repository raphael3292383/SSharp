using SSharp.VM;

namespace SSharp.Tokens
{
    public class NotEqualsOperator : EqualsOperator
    {
        public override string ToString()
        {
            return "!=";
        }

        public override VMObject Operate(VMObject a, VMObject b)
        {
            return new VMBoolean(!CheckEquality(a, b));
        }
    }
}
