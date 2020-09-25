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
        private string _fullName;
        private ObservableCollection<AssemblyField> _fields;
        private ObservableCollection<AssemblyMethod> _methods;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged("FullName");
            }
        }

        public ObservableCollection<AssemblyField> Fields
        {
            get { return _fields; }
            set
            {
                _fields = value;
                OnPropertyChanged("Fields");
            }
        }

        public ObservableCollection<AssemblyMethod> Methods
        {
            get { return _methods; }
            set
            {
                _methods = value;
                OnPropertyChanged("Methods");
            }
        }

        public AssemblyDataType(Type type)
        {
            Name = type.Name;

            FullName = GetFullName(type);

            Fields = new ObservableCollection<AssemblyField>();
            Methods = new ObservableCollection<AssemblyMethod>();

            foreach(var fieldInfo in type.GetFields())
            {
                Fields.Add(new AssemblyField(fieldInfo));
            }

            foreach (var properyInfo in type.GetProperties())
            {
                Fields.Add(new AssemblyField(properyInfo));
            }

            foreach (var methodInfo in type.GetMethods())
            {
                Methods.Add(new AssemblyMethod(methodInfo));
            }
        }

        private string GetFullName(Type type)
        {
            return (
                type.IsPublic ? "public " : "private ") + (
                type.IsAbstract ? "abstract " : "") + (
                type.IsSealed ? "sealed " : "") + (
                type.IsClass ? "class " : type.IsValueType ? "struct " : "") +
                type.Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
