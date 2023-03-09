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
            // Trim the end of any decimal points due to a Cosmos bug. Yes atmo, but i don't really care since SSharp is made for Windows, macOS & Linux.
            return Value.ToString().TrimEnd('.');
        }

        public double Value { get; private set; }
    }
}
