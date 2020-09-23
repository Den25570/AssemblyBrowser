using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib.AssemblyStruct
{
    public class AssemblyDataType : INotifyPropertyChanged
    {
        private string _name;
        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("name");
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

        public AssemblyDataType(Type type)
        {
            this.name = type.Name;
        }

        //  public ObservableCollection<AssemblyField> Fields;
        //  public ObservableCollection<AssemblyMethod> Methods;


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
