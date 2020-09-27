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

namespace AssemblyBrowserLib.AssemblyStruct
{
    public class AssemblyNamespace : INotifyPropertyChanged
    {
        private string _name;
        private List<AssemblyDataType> _dataTypes;

        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }

        public List<AssemblyDataType> DataTypes
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
            this.DataTypes = new List<AssemblyDataType>();
            this.Name = typeName;
        }

        public void AddType(Type type)
        {
            DataTypes.Add(new AssemblyDataType(type));
        }

        public void AddType(Type extendedType, MethodInfo[] extensionMethods)
        {
            DataTypes.Add(new AssemblyDataType(extendedType, extensionMethods));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
             if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
