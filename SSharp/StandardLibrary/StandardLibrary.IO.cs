using SSharp.VM;

namespace SSharp.StandardLibrary
{
    public partial class StandardLibrary
    {
        private static VMNativeFunction Stdlib_print = new VMNativeFunction(
        new List<string> { ("object") },
        (List<VMObject> arguments) =>
        {
            Console.WriteLine(arguments[0]);
            return new VMNull();
        });

        private static VMNativeFunction Stdlib_clear = new VMNativeFunction(
        new List<string> { ("object") },
        (List<VMObject> arguments) =>
        {
            Console.Clear();
            return new VMNull();
        });

        private static VMNativeFunction Stdlib_readline = new VMNativeFunction(
        new List<string>(),
        (List<VMObject> arguments) =>
        {
            return new VMString(Console.ReadLine()!);
        });

        private static VMNativeFunction Stdlib_readkey = new VMNativeFunction(
        new List<string>(),
        (List<VMObject> arguments) =>
        {
            return new VMNumber((double)Console.ReadKey().Key!);
        });
        private static VMNativeFunction Stdlib_setbackground = new VMNativeFunction(
        new List<string> { ("number") },
        (List<VMObject> arguments) =>
        {
            ConsoleColor bg = (ConsoleColor)((VMNumber)arguments[0]).Value;

            Console.BackgroundColor = bg;

            return new VMNull();
        });
        private static VMNativeFunction Stdlib_setforeground = new VMNativeFunction(
        new List<string> { ("number") },
        (List<VMObject> arguments) =>
        {
            ConsoleColor bg = (ConsoleColor)((VMNumber)arguments[0]).Value;

            Console.BackgroundColor = bg;

            return new VMNull();
        });
    }
}
