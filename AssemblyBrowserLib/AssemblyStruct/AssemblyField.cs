using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib.AssemblyStruct
{
    public class AssemblyField : INotifyPropertyChanged
    {
        private string _name;
        private string _fullName;
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }

        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged("FullName");
            }
        }

        public AssemblyField(FieldInfo fieldInfo)
        {
            Name = fieldInfo.Name;
            FullName = GetFullName(fieldInfo);
        }

        public AssemblyField(PropertyInfo propertyInfo)
        {
            Name = propertyInfo.Name;
            FullName = GetFullName(propertyInfo);
        }

        private string GetFullName(FieldInfo type)
        {
            return (
                type.IsPublic ? "public " : "private ") +
                type.Name;
        }

        private string GetFullName(PropertyInfo type)
        {
            return type.Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
