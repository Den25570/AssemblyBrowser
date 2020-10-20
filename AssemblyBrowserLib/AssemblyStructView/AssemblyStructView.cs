using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AssemblyBrowserLib.AssemblyStruct;

namespace AssemblyBrowserLib.AssemblyStructView
{
    public class AssemblyStructView : INotifyPropertyChanged
    {
        private List<AssemblyNamespaceView> _namespaces;

        public List<AssemblyNamespaceView> Namespaces
        {
            get { return _namespaces; }
            set
            {
                _namespaces = value;
                OnPropertyChanged("Namespaces");
            }
        }

        public AssemblyStructView(AssemblyStruct.AssemblyStruct assemblyStruct, bool HideGenerated)
        {
            Namespaces = assemblyStruct.Namespaces.ConvertAll<AssemblyNamespaceView>(assemblyNamespace => new AssemblyNamespaceView(assemblyNamespace, HideGenerated));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
