using SSharp.Tokens;
using SSharp.VM;
using System;
using System.Collections.Generic;

namespace SSharp
{
    public class Repl
    {
        public Interpreter Interpreter { get; } = new Interpreter();

        public void Start()
        {
            Interpreter.LoadLibrary(new StandardLibrary.StandardLibrary());

            Console.Clear();
            Console.WriteLine("S# v0.9.9");

            while (true)
            {
                Console.Write(">>> ");

                string input = Console.ReadLine();
                if (input == null)
                    continue;

                if (input.Trim().EndsWith("["))
                {
                    input += "\n";
                    while (true)
                    {
                        Console.Write("... ");

                        string input1 = Console.ReadLine();

                        if (input1 == null)
                            continue;

                        if (input1 != string.Empty)
                        {
                            input += input1 + "\n";
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                try
                {
                    Script script = new Script(input);

                    script.Lex();

                    //script.RootToken.Debug();
                    bool isExpression = true;
                    foreach (Token token in script.RootToken.Tokens)
                    {
                        if (token is Assignment || token is Block)
                        {
                            isExpression = false;
                        }
                    }

                    if (isExpression)
                    {
                        VMObject result = Interpreter.InterpretExpression(script.RootToken, script.RootToken);
                        if (result != null && result is not VMNull)
                        {
                            Console.WriteLine(result.ToString());
                        }
                    }
                    else
                    {
                        Interpreter.InterpretScript(script);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
