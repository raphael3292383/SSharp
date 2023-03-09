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

        public VMObject GetVariableByName(string name, Block scope)
        {
            string[] split = name.Split(".");

            // search in global variables
            foreach ((string VariableName, VMObject Object, Block Scope) variable in Variables)
            {
                if (variable.VariableName == name)
                {
                    if (variable.Scope == null || scope == null || scope == variable.Scope)
                        return variable.Object;
                    if (scope.IsDescendantOf(variable.Scope))
                        return variable.Object;
                }
            }
            throw new Exception($"Unknown variable {name}.");

        }

        public void DefineVariable(string name, VMObject obj, Block scope)
        {
            string[] split = name.Split('.');
            for (int i = 0; i < Variables.Count; i++)
            {
                var item = Variables[i];
                bool inScope = item.Scope == scope || scope == null || item.Scope == null || scope.IsDescendantOf(item.Scope);
                if (item.VariableName == name && inScope)
                {
                    // Update the variable.
                    Variables[i] = (name, obj, item.Scope);
                    return;
                }
            }
            Variables.Add((name, obj, scope));
        }
    }
}
