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

        public AssemblyField(MethodInfo methodInfo)
        {
            Name = methodInfo.Name;
            FullName = GetFullName(methodInfo);
        }

        private string GetFullName(FieldInfo type)
        {
            return (
                type.IsPublic ? "public " : "private ") +
                type.DeclaringType.Name + " " +
                type.Name;
        }

        private string GetFullName(MethodInfo methodInfo)
        {
            string paramsString = "(";
            foreach(var parameter in methodInfo.GetParameters())
            {
                if (paramsString != "(")
                {
                    paramsString += " ,";
                }
                paramsString += 
                    parameter.IsOut ? "out " : parameter.IsIn ? "in " : parameter.ParameterType.IsByRef ? "ref " : "" + 
                    parameter.ParameterType.Name + " " + parameter.Name;
            }
            paramsString += ")";
            return (
                methodInfo.IsPublic ? "public " : "private ") + (
                methodInfo.IsAbstract ? "abstarct " : "") + (
                methodInfo.IsStatic ? "static " : "") + (
                methodInfo.ReturnType.Name + " ") +
                methodInfo.Name + paramsString;
        }

        private string GetFullName(PropertyInfo type)
        {
            return type.DeclaringType.Name + " " + type.Name + " " + (
                type.CanRead ? type.GetGetMethod(false) != null ? "{ public get; " : "{ private get; " : "{") + (
                type.CanWrite ? type.GetSetMethod(false) != null ? "public set;} " : "private set;}" : "}");

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
