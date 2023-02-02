using SSharp.VM;
using System;
using System.Collections.Generic;

namespace SSharp.StandardLibrary
{
    public partial class StandardLibrary
    {
        private static VMNativeFunction Stdlib_str = new VMNativeFunction(
        new List<string> { ("object") },
        (List<VMObject> arguments) =>
        {
            return new VMString(arguments[0].ToString());
        });

        private static VMNativeFunction Stdlib_nbrequals = new VMNativeFunction(
        new List<string> { ("number"), ("number") },
        (List<VMObject> arguments) =>
        {
            object o1 = ((VMNumber)arguments[0]).Value;
            object o2 = ((VMNumber)arguments[1]).Value;

            return new VMBoolean(o1 == o2);
        });

        private static VMNativeFunction Stdlib_num = new VMNativeFunction(
        new List<string> { ("string") },
        (List<VMObject> arguments) =>
        {
            if (arguments[0] is VMString)
            {
                if (double.TryParse(((VMString)arguments[0]).Value, out double result))
                {
                    return new VMNumber(result);
                }
                else
                {
                    throw new Exception("Failed to parse string to a number.");
                }
            }
            else
            {
                throw new Exception("num expects a string.");
            }
        });

        private static VMNativeFunction Stdlib_typeof = new VMNativeFunction(
        new List<string> { ("object") },
        (List<VMObject> arguments) =>
        {
            string typeStr = arguments[0] switch
            {
                VMBaseFunction => "function",
                VMBoolean => "boolean",
                VMNull => "null",
                VMNumber => "number",
                VMRange => "range",
                VMString => "string",
                _ => "object"
            };
            return new VMString(typeStr);
        });
    }
}
