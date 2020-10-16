using AssemblyBrowserLib.AssemblyStruct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib.AssemblyStructView
{
    public class AssemblyNamespaceView : INotifyPropertyChanged
    {
        private string _name;
        private List<AssemblyDataTypeView> _dataTypes;

        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }

        public List<AssemblyDataTypeView> DataTypes
        {
            get { return _dataTypes; }
            set
            {
                _dataTypes = value;
                OnPropertyChanged("DataTypes");
            }
        }

        public AssemblyNamespaceView(AssemblyNamespace assemblyNamespace)
        {
            Name = assemblyNamespace.Name;
            DataTypes = assemblyNamespace.DataTypes.ConvertAll<AssemblyDataTypeView>(dataType => new AssemblyDataTypeView(dataType));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
