using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib.AssemblyStruct
{
    public class AssemblyNamespace : INotifyPropertyChanged
    {
        private string _name;
        public string name { get { return _name; } set { _name = value; OnPropertyChanged("name"); } }

        private ObservableCollection<AssemblyNamespace> _namespaces;
        public ObservableCollection<AssemblyNamespace> Namespaces
        {
            get { return _namespaces; }
            set {
                _namespaces = value;
                OnPropertyChanged("assemblyname");
            }
        }

        //   public ObservableCollection<AssemblyDataType> DataTypes;

        public AssemblyNamespace(Type type)
        {
            this.name = type.Namespace;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
