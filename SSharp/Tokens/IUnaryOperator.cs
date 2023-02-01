using SSharp.VM;

namespace SSharp.Tokens
{
    public interface IUnaryOperator
    {
        public VMObject OperateUnary(VMObject x);
    }
}
