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
    public class AssemblyTypeMember
    {
        public bool IsExtensionMethod;
        public string Name;
        public string FullName;

        public AssemblyTypeMember(FieldInfo fieldInfo)
        {
            Name = fieldInfo.Name;
            FullName = GetFullName(fieldInfo);
        }

        public AssemblyTypeMember(PropertyInfo propertyInfo)
        {
            Name = propertyInfo.Name;
            FullName = GetFullName(propertyInfo);
        }

        public AssemblyTypeMember(MethodInfo methodInfo, bool isExtension = false)
        {
            Name = methodInfo.Name;
            FullName = GetFullName(methodInfo);
            IsExtensionMethod = isExtension;
        }

        private string GetFullName(FieldInfo fieldInfo)
        {
            string result = (fieldInfo.IsPublic ? "public " : "private ") +
                AssemblyDataType.GetTypeGenericName(fieldInfo.FieldType) + " " +
                fieldInfo.Name;

            return result;
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
                    (parameter.IsOut ? "out " : parameter.IsIn ? "in " : parameter.ParameterType.IsByRef ? "ref " : "") + 
                    parameter.ParameterType.Name + " " + parameter.Name;
            }
            paramsString += ")";

            return (methodInfo.IsPublic ? "public " : "private ") + 
                (methodInfo.IsAbstract ? "abstarct " : "") + 
                (methodInfo.IsStatic ? "static " : "") +
                AssemblyDataType.GetTypeGenericName(methodInfo.ReturnType) + " " +
                methodInfo.Name + paramsString;
        }

        private string GetFullName(PropertyInfo type)
        {
            return AssemblyDataType.GetTypeGenericName(type.PropertyType) + " " + type.Name + " " + (
                type.CanRead ? type.GetGetMethod(false) != null ? "{ public get; " : "{ private get; " : "{") + (
                type.CanWrite ? type.GetSetMethod(false) != null ? "public set;} " : "private set;}" : "}");

        }
    }
}
