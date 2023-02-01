using SSharp.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSharp.StandardLibrary
{
    public partial class StandardLibrary
    {
        private static VMNativeFunction Stdlib_callScript = new VMNativeFunction(
        new List<string> { ("string") },
        (List<VMObject> arguments) =>
        {
            string s = ((VMString)arguments[0]).Value;

            Script script = new(File.ReadAllText(s));
            script.Lex();
            Interpreter i = new();
            i.LoadLibrary(new StandardLibrary());
            i.InterpretScript(script);

            return new VMNull();
        });

        private static VMNativeFunction Stdlib_exit = new VMNativeFunction(
        new List<string> { "number" },
        (List<VMObject> arguments) =>
        {
            int eC = (int)((VMNumber)arguments[0]).Value;

            Environment.Exit(eC);

            return new VMNull();
        });
    }
}
