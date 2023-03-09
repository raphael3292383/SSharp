using SSharp.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSharp.StandardLibrary
{
    /// <summary>
    /// Namespace used for math functions
    /// </summary>
    public class MathNamespace
    {
        static VMNamespace n;

        public static void Init(Interpreter i)
        {
            n = i.DefineNamespace("stdmath");
            n.DefineVariable("ceil", new VMNativeFunction(new List<string>() { "number" }, (List<VMObject> arguments) =>
            {
                if (arguments[0] is not VMNumber num)
                    throw new Exception("ceil expects a number.");

                return new VMNumber(Math.Ceiling(num.Value));
            }), null);
            n.DefineVariable("round", new VMNativeFunction(new List<string>() { "number" }, (List<VMObject> arguments) =>
            {
                if (arguments[0] is not VMNumber num)
                    throw new Exception("round expects a number.");

                return new VMNumber(Math.Round(num.Value));
            }), null);
            n.DefineVariable("floor", new VMNativeFunction(new List<string>() { "string" }, (List<VMObject> arguments) =>
            {
                if (arguments[0] is not VMNumber num)
                    throw new Exception("floor expects a number.");

                return new VMNumber(Math.Floor(num.Value));
            }), null);
        }
    }
}
