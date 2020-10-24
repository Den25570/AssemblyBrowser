using AssemblyBrowserLib.AssemblyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib.AssemblyStruct.TypeMembers
{
    class TypeProperty : TypeMember
    {
        public readonly string name;
        public readonly PropertyType.PropertyTypeEnum getMethodAccessModificator;
        public readonly PropertyType.PropertyTypeEnum setMethodAccessModificator;

        public readonly PropertyInfo propertyInfo;

        public override string GetFullName()
        {
            string getString = PropertyType.GetGetString(getMethodAccessModificator);
            string setString = PropertyType.GetSetString(setMethodAccessModificator);

            return
                AssemblyDataType.GetTypeGenericName(propertyInfo.PropertyType) + " " +
                name + " { " +
                (getString != "" ? getString + "; " : "") +
                (setString != "" ? setString + "; }" : " }");
        }

        public TypeProperty(PropertyInfo propertyInfo)
        {
            name = propertyInfo.Name;
            this.propertyInfo = propertyInfo;

            getMethodAccessModificator = PropertyType.GetGetPropertyType(propertyInfo);
            setMethodAccessModificator = PropertyType.GetSetPropertyType(propertyInfo);
        }
    }
}
