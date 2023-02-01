using System;
using System.Collections.Generic;

namespace SSharp.VM
{
    public class VMNativeFunction : VMBaseFunction
    {
        public VMNativeFunction(Func<List<VMObject>, VMObject> func)
        {
            Func = func;
        }

        public VMNativeFunction(List<string> arguments, Func<List<VMObject>, VMObject> func)
        {
            Func = func;
            Arguments = arguments;
        }

        public Func<List<VMObject>, VMObject> Func { get; private set; }

        public override string ToString()
        {
            return "<VMObject NativeFunction>";
        }
    }
}
