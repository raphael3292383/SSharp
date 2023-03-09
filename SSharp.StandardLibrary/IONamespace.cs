using SSharp.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SSharp.StandardLibrary
{
    /// <summary>
    /// Namespace used for I/O functions
    /// </summary>
    public class IONamespace
    {
        static VMNamespace n;

        public static void Init(Interpreter i)
        {
            n = i.DefineNamespace("stdio");

            n.DefineVariable("println", new VMNativeFunction(new List<string> { "string" }, (List<VMObject> arguments) =>
            {
                Console.WriteLine(((VMString)arguments[0]).Value);

                return new VMNull();
            }), null);
            n.DefineVariable("print", new VMNativeFunction(new List<string> { "string" }, (List<VMObject> arguments) =>
            {
                Console.Write(((VMString)arguments[0]).Value);

                return new VMNull();
            }), null);
            n.DefineVariable("readInput", new VMNativeFunction(new List<string> { }, (List<VMObject> arguments) =>
            {
                return new VMString(Console.ReadLine());
            }), null);
        }
    }
}
