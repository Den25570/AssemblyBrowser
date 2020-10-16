using AssemblyBrowserLib.AssemblyStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib
{
    public static class AssemblyInfo
    {
        public static AssemblyStruct.AssemblyStruct assemblyStruct { get; private set; }

        public static void LoadAssembly(string path) 
        {
            Assembly loadedAssembly = Assembly.LoadFrom(path);
            assemblyStruct = new AssemblyStruct.AssemblyStruct(loadedAssembly);

        }

        public static void LoadAssembly()
        {
            Assembly loadedAssembly = Assembly.GetExecutingAssembly();
            assemblyStruct = new AssemblyStruct.AssemblyStruct(loadedAssembly);
        }

        public static AssemblyStructView.AssemblyStructView GetAssemblyInfo()
        {
            return new AssemblyStructView.AssemblyStructView(assemblyStruct);
        }

    }
}
