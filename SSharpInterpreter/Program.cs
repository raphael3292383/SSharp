using SSharp.VM;
using SSharp;
using SSharp.StandardLibrary;

namespace SSharpInterpreter
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length >= 2)
            {
                Console.WriteLine("SS_ERR : Too many arguments. Try run this command without any argument to launch S# Repl or include a script as argument for interpret.");
                Environment.Exit(1);
            }else if (args.Length == 1)
            {
                if (File.Exists(args[0]))
                {
                    if (args[0].EndsWith(".ss"))
                    {
                        Script script = new Script(File.ReadAllText(args[0]));
                        script.Lex();
                        Interpreter interpreter = new Interpreter();
                        interpreter.LoadLibrary(new StandardLibrary());
                        interpreter.InterpretScript(script);
                    }
                    else
                    {
                        Console.WriteLine("SS_ERR : You need a file with .ss extension for the interpreter to work");
                    }
                }
                else
                {
                    Console.WriteLine("SS_ERR : The provided file path doens't exists.");
                }
            }
            else
            {
                new SSharp.Repl().Start();
            }
        }
    }
}