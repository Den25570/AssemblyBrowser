using AssemblyBrowserLib.AssemblyData;
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
        public List<AssemblyTypeMember> Fields;
        public bool IsGenerated;
        public string Name;

        public Type type;
        public DataAccessModificator.DataAccessModificatorEnum accessModificator;
        public DataAttribute.DataTypeAttributeEnum typeAttribute;
        public DataTypeClass.DataTypeClassEnum typeClass;

        public string FullName { 
            get {
                string accessModificatorString = DataAccessModificator.GetString(accessModificator);
                string typeAttributeString = DataAttribute.GetString(typeAttribute);
                string typeClassString = DataTypeClass.GetString(typeClass);
                return
                    (accessModificatorString != "" ? (accessModificatorString + " ") : "") +
                    (typeAttributeString != "" ? (typeAttributeString + " ") : "") +
                    (typeClassString != "" ? (typeClassString + " ") : "") +
                    GetTypeGenericName(type);
            } 
        }

        public AssemblyDataType(Type type)
        {
            Name = type.Name;
            accessModificator = DataAccessModificator.GetTypeModifiers(type);
            typeAttribute = DataAttribute.GetTypeAtributes(type);
            typeClass = DataTypeClass.GetTypeClass(type);
            this.type = type;

            Fields = new List<AssemblyTypeMember>();
            IsGenerated = IsCompilerGenerated(type);

            var flags = BindingFlags.Instance |
                        BindingFlags.Static |
                        BindingFlags.NonPublic |
                        BindingFlags.Public | 
                        BindingFlags.DeclaredOnly;

            foreach (var fieldInfo in type.GetFields(flags))
            {
                Fields.Add(new AssemblyTypeMember(fieldInfo, IsCompilerGenerated(fieldInfo)));
            }

            foreach (var properyInfo in type.GetProperties(flags))
            {
                Fields.Add(new AssemblyTypeMember(properyInfo, IsCompilerGenerated(properyInfo)));
            }

            foreach (var methodInfo in type.GetMethods(flags))
            {
                if (!methodInfo.IsDefined(typeof(ExtensionAttribute), false))
                {
                    Fields.Add(new AssemblyTypeMember(methodInfo, IsCompilerGenerated(methodInfo)));
                }                
            }
        }

        public AssemblyDataType(Type extendedType, MethodInfo[] extensionMethods)
        {
            Name = extendedType.Name;
            accessModificator = DataAccessModificator.GetTypeModifiers(extendedType);
            typeAttribute = DataAttribute.GetTypeAtributes(extendedType);
            typeClass = DataTypeClass.GetTypeClass(extendedType);
            this.type = extendedType;

            Fields = new List<AssemblyTypeMember>();
            foreach (var methodInfo in extensionMethods)
            {
                Fields.Add(new AssemblyTypeMember(methodInfo, IsCompilerGenerated(methodInfo), true));
            }
        }

        public static string GetTypeGenericName(Type type)
        {
            string result = type.Name;

            var genericArguments = type.GetGenericArguments();
            if (genericArguments.Length > 0)
            {
                result = result.Substring(0, result.IndexOf('`'));
                result += "<" + GetGenericType(genericArguments) + ">";
            }

            return result;
        }

        private static string GetGenericType(Type[] types)
        {
            string result = "";
            bool isFirst = true;
            foreach (var genericType in types)
            {
                if (!isFirst)
                    result += ", ";
                else
                    isFirst = false;
                if (genericType.IsGenericType)
                    result += genericType.Name.Substring(0, genericType.Name.IndexOf('`')) + "<" + GetGenericType(genericType.GenericTypeArguments) + ">";
                else
                    result += genericType.Name;
            }

            return result;
        }      

        bool IsCompilerGenerated(MemberInfo member)
        {
            bool compilerGenerated = false;

            compilerGenerated |= (Attribute.GetCustomAttribute(member, typeof(CompilerGeneratedAttribute)) != null);


            if (member is MethodInfo)
            {
                compilerGenerated |= (member as MethodInfo).IsSpecialName;
            }
            else if (member is PropertyInfo)
            {
                compilerGenerated |= (member as PropertyInfo).IsSpecialName;
            }

            return compilerGenerated;
        }
    }
}
