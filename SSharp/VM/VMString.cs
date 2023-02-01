namespace SSharp.VM
{
    public class VMString : VMObject
    {
        public VMString(string value)
        {
            //Console.WriteLine("New VMString " + value);
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public string Value { get; private set; }
    }
}
