using SSharp.VM;

namespace SSharp.TestLibrary
{
    public class Library : VMLibrary
    {
        public override void LoadLibrary(Interpreter i)
        {
            base.LoadLibrary(i);

            i.DefineVariable("test", new VMNativeFunction(new List<string> { ("object") },(List<VMObject> arguments) =>
            {
                Console.WriteLine(arguments[0]);
                return new VMNull();
            }), scope: null);
        }
    }
}