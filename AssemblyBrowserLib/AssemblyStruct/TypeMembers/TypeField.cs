using AssemblyBrowserLib.AssemblyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib.AssemblyStruct.TypeMembers
{
    public class TypeField : TypeMember
    {
        public readonly string name;
        public readonly DataAccessModificator.DataAccessModificatorEnum accessModificator;

        public readonly FieldInfo fieldInfo;

        public override string GetFullName()
        {
            string result =
                DataAccessModificator.GetString(accessModificator) + " " +
                AssemblyDataType.GetTypeGenericName(fieldInfo.FieldType) + " " +
                name;

            return result;
        }

        public TypeField(FieldInfo fieldInfo)
        {
            name = fieldInfo.Name;
            this.fieldInfo = fieldInfo;

            accessModificator = DataAccessModificator.GetTypeModifiers(fieldInfo);
        }
    }
}
