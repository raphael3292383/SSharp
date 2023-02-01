using SSharp.VM;

namespace SSharp.Tokens
{
    public abstract class Operator : Token
    {
        public abstract VMObject Operate(VMObject a, VMObject b);

        public override string ToString()
        {
            return "Operator";
        }
    }
}
