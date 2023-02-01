using SSharp;
using SSharp.Tokens;
using System.Collections.Generic;

namespace SSharp.VM
{
    public class VMFunction : VMBaseFunction
    {
        public VMFunction(Block root)
        {
            Root = root;
        }

        public VMFunction(Block root, List<string> arguments)
        {
            Root = root;
            Arguments = arguments;
        }

        public static VMFunction FromScript(Script script)
        {
            return new VMFunction(script.RootToken);
        }

        public Block Root { get; set; }

        public override string ToString()
        {
            return "<VMObject Function>";
        }
    }
}
