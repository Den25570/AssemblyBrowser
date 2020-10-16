using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib.AssemblyStruct
{
    public class AssemblyDataType 
    {
        public string Name;
        public string FullName;
        public List<AssemblyTypeMember> Fields;
        

        public AssemblyDataType(Type type)
        {
            Name = type.Name;

            FullName = GetFullName(type);

            Fields = new List<AssemblyTypeMember>();

            var flags = BindingFlags.Instance |
                        BindingFlags.Static |
                        BindingFlags.NonPublic |
                        BindingFlags.Public;

            foreach (var fieldInfo in type.GetFields(flags))
            {
                Fields.Add(new AssemblyTypeMember(fieldInfo));
            }

            foreach (var properyInfo in type.GetProperties(flags))
            {
                Fields.Add(new AssemblyTypeMember(properyInfo));
            }

            foreach (var methodInfo in type.GetMethods(flags))
            {
                if (!methodInfo.IsDefined(typeof(ExtensionAttribute), false))
                {
                    Fields.Add(new AssemblyTypeMember(methodInfo));
                }                
            }
        }

        public AssemblyDataType(Type extendedType, MethodInfo[] extensionMethods)
        {
            Name = extendedType.Name;
            FullName = GetFullName(extendedType);

            Fields = new List<AssemblyTypeMember>();
            foreach (var methodInfo in extensionMethods)
            {
                Fields.Add(new AssemblyTypeMember(methodInfo, true));
            }
        }

        private string GetFullName(Type type)
        {
            string result = GetTypeModifiers(type) + GetTypeAtributes(type) + GetTypeClass(type) + GetTypeGenericName(type);
            return result;
        }

        public static string GetTypeGenericName(Type type)
        {
            string result = type.Name;
            var genericArguments = type.GetGenericArguments();

            if (genericArguments.Length > 0)
            {
                result += "<" + GetGenericType(genericArguments) + ">";
            }

            return result;
        }

        private static string GetGenericType(Type[] t)
        {
            string result = "";
            foreach (var genericType in t)
            {
                if (genericType.IsGenericType)
                    result += genericType.Name + "<" + GetGenericType(genericType.GenericTypeArguments) + ">";
                else
                    result += genericType.Name + " ";
            }

            return result;
        }

        public string GetTypeModifiers(Type typeInfo)
        {
            return typeInfo.IsNestedPrivate ? "private " :
            typeInfo.IsNestedFamily ? "protected " :
            typeInfo.IsNestedAssembly ? "internal " :
            typeInfo.IsNestedFamORAssem ? "protected internal " :
            typeInfo.IsNestedFamANDAssem ? "protected private " :
            typeInfo.IsNestedPublic || typeInfo.IsPublic ? "public " :
            typeInfo.IsNotPublic ? "private " : "public ";
        }

        private string GetTypeAtributes(Type typeInfo)
        {
            return typeInfo.IsAbstract && typeInfo.IsSealed ? "static ":
            typeInfo.IsSealed ? "sealed ":
            typeInfo.IsAbstract ? "abstract ": "";
        }

        private string GetTypeClass(Type typeInfo)
        {
            return typeInfo.IsInterface ? "interface " :
            typeInfo.IsEnum ? "enum " :
            typeInfo.IsValueType ? "struct " :
            (typeInfo.BaseType == typeof(MulticastDelegate)) ? "delegate " :
            typeInfo.IsClass ? "class " :  "";
        }
    }
}
