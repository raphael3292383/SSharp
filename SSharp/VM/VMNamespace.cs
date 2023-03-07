using SSharp.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSharp.VM
{
    public class VMNamespace
    {
        public string NamespaceName = "";
        public List<(string VariableName, VMObject Object, Block Scope)> Variables;

        public VMNamespace(string Name)
        {
            NamespaceName = Name;
            Variables = new List<(string VariableName, VMObject Object, Block Scope)>();
        }
    }
}
