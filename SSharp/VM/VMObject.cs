namespace SSharp.VM
{
    public class VMObject
    {
        public override string ToString()
        {
            return "<VMObject>";
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public virtual bool IsTruthy()
        {
            return true;
        }

        public virtual bool PassByReference()
        {
            return false;
        }
    }
}
