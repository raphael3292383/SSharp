namespace SSharp.VM
{
    public class VMRange : VMObject
    {
        public VMRange(double lower, double upper)
        {
            if (upper > lower)
            {
                Lower = lower;
                Upper = upper;
            }
            else
            {
                Lower = upper;
                Upper = lower;
            }
        }

        public override string ToString()
        {
            return $"<Range {Lower.ToString()}..{Upper.ToString()}>";
        }

        public double Lower { get; private set; }
        public double Upper { get; private set; }
    }
}
