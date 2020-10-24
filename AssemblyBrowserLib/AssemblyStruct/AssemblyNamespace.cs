using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowserLib.AssemblyStruct
{
    public class AssemblyNamespace
    {
        public readonly string Name;
        public readonly List<AssemblyDataType> DataTypes;

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
