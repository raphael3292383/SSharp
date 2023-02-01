namespace SSharp.VM
{
    public class VMNull : VMObject
    {
        public VMNull()
        {
            //Console.WriteLine("New VMNull");
        }

        public override string ToString()
        {
            return "null";
        }

        public override bool IsTruthy()
        {
            return false;
        }
    }
}
