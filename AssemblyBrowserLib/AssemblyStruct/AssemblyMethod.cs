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
    public class AssemblyMethod : INotifyPropertyChanged
    {
        private string _name;
        private string _fullName;
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("name"); } }
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged("FullName");
            }
        }

        public AssemblyMethod(MethodInfo methodInfo)
        {
            Name = methodInfo.Name;
            FullName = GetFullName(methodInfo);
        }

        private string GetFullName(MethodInfo methodInfo)
        {
            return (
                methodInfo.IsPublic ? "public " : "private ") + (
                methodInfo.ReturnType.Name + " ") + 
                methodInfo.Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
