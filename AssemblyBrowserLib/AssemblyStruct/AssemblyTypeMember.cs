using AssemblyBrowserLib.AssemblyData;
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
        public bool IsGenerated;

        private string name;
        private DataAccessModificator.DataAccessModificatorEnum accessModificator;
        private DataAttribute.DataTypeAttributeEnum typeAttribute;
        private PropertyType.PropertyTypeEnum getMethodAccessModificator;
        private PropertyType.PropertyTypeEnum setMethodAccessModificator;

        private MemberInfo memberInfo;

        public string FullName {
            get {
                if (memberInfo is FieldInfo)
                {
                    return GetFullName(memberInfo as FieldInfo);
                }
                else if(memberInfo is PropertyInfo)
                {
                    return GetFullName(memberInfo as PropertyInfo);
                }
                else if(memberInfo is MethodInfo)
                {
                    return GetFullName(memberInfo as MethodInfo);
                }
                return "";
            } 
        }

        public AssemblyTypeMember(FieldInfo fieldInfo, bool isGenerated)
        {
            name = fieldInfo.Name;
            IsGenerated = isGenerated;
            memberInfo = fieldInfo;

            accessModificator = DataAccessModificator.GetTypeModifiers(fieldInfo);
        }

        public AssemblyTypeMember(PropertyInfo propertyInfo, bool isGenerated)
        {
            name = propertyInfo.Name;
            IsGenerated = isGenerated;
            memberInfo = propertyInfo;

            getMethodAccessModificator = PropertyType.GetGetPropertyType(propertyInfo);
            setMethodAccessModificator = PropertyType.GetSetPropertyType(propertyInfo);
        }

        public AssemblyTypeMember(MethodInfo methodInfo, bool isGenerated, bool isExtension = false)
        {
            name = methodInfo.Name;
            IsGenerated = isGenerated;
            IsExtensionMethod = isExtension;
            memberInfo = methodInfo;

            accessModificator = DataAccessModificator.GetTypeModifiers(methodInfo);
            typeAttribute = DataAttribute.GetAttributes(methodInfo);
        }

        private string GetFullName(FieldInfo fieldInfo)
        {


            string result = 
                DataAccessModificator.GetString(accessModificator) + " " +
                AssemblyDataType.GetTypeGenericName(fieldInfo.FieldType) + " " +
                name;

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
                    AssemblyDataType.GetTypeGenericName(parameter.ParameterType) + " " + parameter.Name;
            }
            paramsString += ")";

            string typeAttributeString = DataAttribute.GetString(typeAttribute);

            return
                DataAccessModificator.GetString(accessModificator) + " " +
               (typeAttributeString != "" ? typeAttributeString + " " : "") + 
                AssemblyDataType.GetTypeGenericName(methodInfo.ReturnType) + " " +
                name + paramsString;
        }

        private string GetFullName(PropertyInfo type)
        {
            string getString = PropertyType.GetGetString(getMethodAccessModificator);
            string setString = PropertyType.GetSetString(setMethodAccessModificator);

            return
                AssemblyDataType.GetTypeGenericName(type.PropertyType) + " " +
                name + " { " +
                (getString != "" ? getString + "; " : "") +
                (setString != "" ? setString + "; }" : " }");

        }
    }
}
