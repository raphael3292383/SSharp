using SSharp.VM;
using System.Security.Cryptography;

namespace SSharp.FileManagement
{
    public class Library : VMLibrary
    {
        public override void LoadLibrary(Interpreter i)
        {
            base.LoadLibrary(i);
            Console.WriteLine("File Management Library version 1.0");
            Console.WriteLine("Made by RaphMar2019");

            i.DefineVariable("GetFileContents", new VMNativeFunction(new List<string> { ("string") },(List<VMObject> arguments) =>
            {
                return new VMString(File.ReadAllText(((VMString)arguments[0]).Value));
            }), scope: null);

            i.DefineVariable("SetFileContents", new VMNativeFunction(new List<string> { ("string"), ("string") }, (List<VMObject> arguments) =>
            {
                File.WriteAllText(((VMString)arguments[0]).Value, (string?)((VMString)arguments[1]).Value);
                return new VMNull();
            }), scope: null);
            i.DefineVariable("FileExists", new VMNativeFunction(new List<string> { ("string") }, (List<VMObject> arguments) =>
            {
                if (File.Exists(((VMString)arguments[0]).Value))
                    return new VMBoolean(true);
                else
                    return new VMBoolean(false);
            }), scope: null);
            i.DefineVariable("CreateFile", new VMNativeFunction(new List<string> { ("string") }, (List<VMObject> arguments) =>
            {
                File.Create(((VMString)arguments[0]).Value);
                return new VMNull();
            }), scope: null);
            i.DefineVariable("DeleteFile", new VMNativeFunction(new List<string> { ("string") }, (List<VMObject> arguments) =>
            {
                File.Delete(((VMString)arguments[0]).Value);
                return new VMNull();
            }), scope: null);
            i.DefineVariable("CopyFile", new VMNativeFunction(new List<string> { ("string"), ("string") }, (List<VMObject> arguments) =>
            {
                File.Copy(((VMString)arguments[0]).Value, ((VMString)arguments[1]).Value);
                return new VMNull();
            }), scope: null);
            i.DefineVariable("MoveFile", new VMNativeFunction(new List<string> { ("string"), ("string") }, (List<VMObject> arguments) =>
            {
                File.Move(((VMString)arguments[0]).Value, ((VMString)arguments[1]).Value);
                return new VMNull();
            }), scope: null);
            i.DefineVariable("DeleteFile", new VMNativeFunction(new List<string> { ("string") }, (List<VMObject> arguments) =>
            {
                File.Delete(((VMString)arguments[0]).Value);
                return new VMNull();
            }), scope: null);
            i.DefineVariable("EncryptFile", new VMNativeFunction(new List<string> { ("string") }, (List<VMObject> arguments) =>
            {
                
                return new VMNull();
            }), scope: null);
        }
    }
}