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

        private ObservableCollection<AssemblyNamespace> _namespaces;

        public ObservableCollection<AssemblyNamespace> Namespaces
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
            Namespaces = new ObservableCollection<AssemblyNamespace>();
            namespacessDictionary = new Dictionary<string, AssemblyNamespace>();
            foreach (Type type in assembly.GetTypes())
            {
                AssemblyNamespace assemblyNamespace;
                if (!namespacessDictionary.TryGetValue(type.Namespace, out assemblyNamespace))
                {
                    assemblyNamespace = new AssemblyNamespace(type.Namespace);
                    namespacessDictionary.Add(type.Namespace, assemblyNamespace);
                    Namespaces.Add(assemblyNamespace);
                }
                assemblyNamespace.AddType(type);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
