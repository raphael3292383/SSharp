using SSharp.VM;
using System.Security.Cryptography;

namespace SSharp.Threading
{
    public class SSThread
    {
        public Thread managedThread;
        public double ThreadID;
        public Script Script;

        public SSThread(Script script)
        {
            ThreadID = new Random().Next();
            Script = script;
        }

        public void Start()
        {
            managedThread = new(new ThreadStart(() =>
            {
                Interpreter i = new();
                Script.Lex();
                Interpreter interpreter = new Interpreter();
                interpreter.LoadLibrary(new StandardLibrary.StandardLibrary());
                interpreter.InterpretScript(Script);

            }));
            managedThread.Start();
        }

        public void Stop()
        {
            managedThread.Interrupt();
        }
    }
    public class Library : VMLibrary
    {
        public List<SSThread> RegisteredThreads = new();

        public override void LoadLibrary(Interpreter i)
        {
            base.LoadLibrary(i);
            Console.WriteLine("Threading Library version 1.0");
            Console.WriteLine("Made by RaphMar2019");

            i.DefineVariable("CreateThread", new VMNativeFunction(new List<string> { ("string") }, (List<VMObject> arguments) =>
            {
                SSThread t = new(new(File.ReadAllText(((VMString)arguments[0]).Value)));
                RegisteredThreads.Add(t);
                return new VMNumber(t.ThreadID);
            }), scope: null);

            i.DefineVariable("StartThread", new VMNativeFunction(new List<string> { ("number") }, (List<VMObject> arguments) =>
            {
                int threadId = (int)((VMNumber)arguments[0]).Value;

                foreach (SSThread t in RegisteredThreads)
                {
                    if (t.ThreadID == threadId)
                    {
                        t.Start();
                        return new VMBoolean(true);
                    }
                }

                return new VMBoolean(false);
            }), scope: null);

            i.DefineVariable("StopThread", new VMNativeFunction(new List<string> { ("number") }, (List<VMObject> arguments) =>
            {
                int threadId = (int)((VMNumber)arguments[0]).Value;

                foreach (SSThread t in RegisteredThreads)
                {
                    if (t.ThreadID == threadId)
                    {
                        t.Stop();
                        return new VMBoolean(true);
                    }
                }

                return new VMBoolean(false);
            }), scope: null);
        }
    }
}