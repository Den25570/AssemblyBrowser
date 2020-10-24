using AssemblyBrowserLib.AssemblyStruct;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

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

        public AssemblyNamespaceView(AssemblyNamespace assemblyNamespace, bool HideGenerated)
        {
            Name = assemblyNamespace.Name;
            DataTypes = assemblyNamespace.DataTypes
                .Where(dataType => !(dataType.IsGenerated && HideGenerated)).ToList()
                .ConvertAll<AssemblyDataTypeView>(dataType => new AssemblyDataTypeView(dataType, HideGenerated));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
