using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
