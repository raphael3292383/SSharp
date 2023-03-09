using SSharp.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSharp.StandardLibrary
{
    public class MainNamespace
    {
        static VMNamespace aNamespace;

        public static void Init(Interpreter i)
        {
            aNamespace = i.DefineNamespace("stdlib");

            aNamespace.DefineVariable("os", new VMString(Environment.OSVersion.VersionString), null);
            aNamespace.DefineVariable("runtime", new VMString(".NET " + Environment.Version.ToString()), null);
        }
    }
}
