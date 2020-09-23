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

        private ObservableCollection<AssemblyDataType> _dataTypes;
        public ObservableCollection<AssemblyDataType> DataTypes
        {
            get { return _dataTypes; }
            set
            {
                _dataTypes = value;
                OnPropertyChanged("DataTypes");
            }
        } 

        public AssemblyNamespace(string typeName)
        {
            this.DataTypes = new ObservableCollection<AssemblyDataType>();
            this.name = typeName;
        }

        public void AddType(Type type)
        {
            DataTypes.Add(new AssemblyDataType(type));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
