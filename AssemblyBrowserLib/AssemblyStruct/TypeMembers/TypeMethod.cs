using AssemblyBrowserLib.AssemblyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib.AssemblyStruct.TypeMembers
{
    public class TypeMethod : TypeMember
    {
        public readonly string name;
        public readonly DataAccessModificator.DataAccessModificatorEnum accessModificator;
        public readonly DataAttribute.DataTypeAttributeEnum typeAttribute;
        public readonly bool IsExtensionMethod;

        public readonly MethodInfo methodInfo;

        public override string GetFullName()
        {
            string paramsString = "(";
            foreach (var parameter in methodInfo.GetParameters())
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

        public TypeMethod(MethodInfo methodInfo, bool isExtension = false)
        {
            name = methodInfo.Name;
            IsExtensionMethod = isExtension;
            this.methodInfo = methodInfo;

            accessModificator = DataAccessModificator.GetTypeModifiers(methodInfo);
            typeAttribute = DataAttribute.GetAttributes(methodInfo);
        }
    }
}
