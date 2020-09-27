using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib.AssemblyStruct
{
    public class AssemblyStruct : INotifyPropertyChanged
    {
        private Dictionary<string, AssemblyNamespace> namespacessDictionary;

        private List<AssemblyNamespace> _namespaces;

        public List<AssemblyNamespace> Namespaces
        {
            get { return _namespaces; }
            set
            {
                _namespaces = value;
                OnPropertyChanged("Namespaces");
            }
        }

        public AssemblyStruct(Assembly assembly)
        {
            Namespaces = new List<AssemblyNamespace>();
            namespacessDictionary = new Dictionary<string, AssemblyNamespace>();

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSealed && !type.IsGenericType && !type.IsNested)
                {
                    var extensionMethods = GetExtensionMethods(type);
                    var methodGroups = from method in extensionMethods
                                       group method by method.GetParameters()[0].ParameterType;

                    foreach (var methodGroup in methodGroups)
                    {
                        Type extensionType = methodGroup.Key;
                        AssemblyNamespace assemblyExtensionNamespace = GetOrAddNamespace(extensionType);
                        assemblyExtensionNamespace?.AddType(extensionType, methodGroup.ToArray());
                    }
                }

                AssemblyNamespace assemblyNamespace = GetOrAddNamespace(type);
                assemblyNamespace?.AddType(type);
            }
        }

        private AssemblyNamespace GetOrAddNamespace(Type type)
        {
            AssemblyNamespace assemblyNamespace;
            try
            {
                string namespaceName = type.Namespace ?? "<Without namespace>";
                if (!namespacessDictionary.TryGetValue(namespaceName, out assemblyNamespace))
                {
                    assemblyNamespace = new AssemblyNamespace(namespaceName);
                    namespacessDictionary.Add(namespaceName, assemblyNamespace);
                    Namespaces.Add(assemblyNamespace);
                }
            }
            catch
            {
                assemblyNamespace = null;
            }
            
            return assemblyNamespace;
        }

        public static IEnumerable<MethodInfo> GetExtensionMethods(Type type)
        {
            var query = from method in type.GetMethods(BindingFlags.Static
                            | BindingFlags.Public | BindingFlags.NonPublic)
                        where method.IsDefined(typeof(ExtensionAttribute), false)
                        select method;
            return query;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
