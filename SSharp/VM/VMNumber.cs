namespace SSharp.VM
{
    public class VMNumber : VMObject
    {
        public VMNumber(double value)
        {
            //Console.WriteLine("New VMNumber " + value);
            Value = value;
        }

        public override string ToString()
        {
            // Trim the end of any decimal points due to a Cosmos bug.
            return Value.ToString().TrimEnd('.');
        }

        public double Value { get; private set; }
    }
}
