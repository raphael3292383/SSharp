using System.Collections.Generic;

namespace SSharp.VM
{
    public class VMBaseFunction : VMObject
    {
        public VMBaseFunction()
        {
            Arguments = new();
        }

        public VMBaseFunction(List<string> arguments)
        {
            Arguments = arguments;
        }

        public List<string> Arguments { get; set; }

        public override string ToString()
        {
            return "<VMBaseFunction>";
        }

        public override bool PassByReference()
        {
            return true;
        }
    }
}
