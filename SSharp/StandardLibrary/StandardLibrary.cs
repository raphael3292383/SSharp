using SSharp.VM;

namespace SSharp.StandardLibrary
{
    public partial class StandardLibrary : VMLibrary
    {
        public override void LoadLibrary(Interpreter interpreter)
        {
            // Misc
            interpreter.DefineVariable("exit", Stdlib_exit, scope: null);
            interpreter.DefineVariable("callScript", Stdlib_callScript, scope: null);

            // IO
            interpreter.DefineVariable("print", Stdlib_print, scope: null);
            interpreter.DefineVariable("readLine", Stdlib_readline, scope: null);
            interpreter.DefineVariable("readKey", Stdlib_readkey, scope: null);
            interpreter.DefineVariable("setBackground", Stdlib_setbackground, scope: null);
            interpreter.DefineVariable("setForeground", Stdlib_setforeground, scope: null);
            interpreter.DefineVariable("clear", Stdlib_clear, scope: null);
            //interpreter.DefineVariable("exec", Stdlib_exec, scope: null);

            // Types
            interpreter.DefineVariable("str", Stdlib_str, scope: null);
            interpreter.DefineVariable("num", Stdlib_num, scope: null);
            interpreter.DefineVariable("typeof", Stdlib_typeof, scope: null);
            interpreter.DefineVariable("nbrequals", Stdlib_nbrequals, scope: null);

            // Maths
            interpreter.DefineVariable("floor", Stdlib_floor, scope: null);
            interpreter.DefineVariable("round", Stdlib_round, scope: null);
            interpreter.DefineVariable("ceil", Stdlib_ceil, scope: null);

            // Environment variables
            interpreter.DefineVariable("_ENV", new VMString("S# on " + Environment.OSVersion.VersionString), scope: null);
            interpreter.DefineVariable("_ISX64OS", new VMBoolean(Environment.Is64BitOperatingSystem), scope: null);
            interpreter.DefineVariable("_ISX64PROC", new VMBoolean(Environment.Is64BitProcess), scope: null);
            interpreter.DefineVariable("_RUNTIME", new VMString(".NET " + Environment.Version.ToString()), scope: null);

            // Library Loader
            interpreter.DefineVariable("import", new VMNativeFunction(
            new List<string> { ("object") },
            (List<VMObject> arguments) =>
            {
               bool success = interpreter.LoadLibrary(((VMString)arguments[0]).Value);
               return new VMBoolean(success);
            }), scope: null);
        }

        // LibLoader
    }
}
