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
        private ObservableCollection<AssemblyNamespace> _namespaces;
        public ObservableCollection<AssemblyNamespace> Namespaces
        {
            get { return _namespaces; }
            set
            {
                _namespaces = value;
                OnPropertyChanged("assemblyname");
            }
        }

        private Dictionary<string, AssemblyNamespace> namespacessDictionary;

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
