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
        private static Assembly loadedAssembly;
        public static void LoadAssembly(string path) 
        {
            loadedAssembly = Assembly.LoadFrom(path);
        }

        public static void LoadAssembly()
        {
            loadedAssembly = Assembly.GetExecutingAssembly();
        }

        public static AssemblyStruct.AssemblyStruct GetAssemblyInfo()
        {
            return new AssemblyStruct.AssemblyStruct(loadedAssembly);
        }

    }
}
