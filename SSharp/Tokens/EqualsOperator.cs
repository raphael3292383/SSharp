using SSharp.VM;

namespace SSharp.Tokens
{
    public class EqualsOperator : Operator
    {
        public override string ToString()
        {
            return "==";
        }

        public bool CheckEquality(VMObject a, VMObject b)
        {
            if (a == b) return true;

            if (a is VMNumber numA && b is VMNumber numB) return numA.Value == numB.Value;
            if (a is VMString strA && b is VMString strB) return strA.Value == strB.Value;
            if (a is VMBoolean boolA && b is VMBoolean boolB) return boolA.Value == boolB.Value;
            if (a is VMNull && b is VMNull) return true;

            return false;
        }

        public override VMObject Operate(VMObject a, VMObject b)
        {
            return new VMBoolean(CheckEquality(a, b));
        }
    }
}
