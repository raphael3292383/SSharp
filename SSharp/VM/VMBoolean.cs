namespace SSharp.VM
{
    public class VMBoolean : VMObject
    {
        public VMBoolean(bool value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool IsTruthy()
        {
            return Value;
        }

        public bool Value { get; private set; }
    }
}
