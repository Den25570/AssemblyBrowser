using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib.AssemblyStruct
{
    public class AssemblyNamespace
    {
        public string Name;
        public List<AssemblyDataType> DataTypes;

        public AssemblyNamespace(string typeName)
        {
            this.DataTypes = new List<AssemblyDataType>();
            this.Name = typeName;
        }

        public void AddType(Type type)
        {
            DataTypes.Add(new AssemblyDataType(type));
        }

        public void AddType(Type extendedType, MethodInfo[] extensionMethods)
        {
            DataTypes.Add(new AssemblyDataType(extendedType, extensionMethods));
        }
    }
}
