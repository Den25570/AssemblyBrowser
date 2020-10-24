using AssemblyBrowserLib.AssemblyData;
using AssemblyBrowserLib.AssemblyStruct.TypeMembers;
using System.Reflection;

namespace AssemblyBrowserLib.AssemblyStruct
{
    public class AssemblyTypeMember
    {
        public readonly bool IsGenerated;

        public TypeMember typeMember;

        public string GetFullName()
        {
            return typeMember.GetFullName();
        }

        public AssemblyTypeMember(FieldInfo fieldInfo, bool isGenerated)
        {
            IsGenerated = isGenerated;

            typeMember = new TypeField(fieldInfo);
        }

        public AssemblyTypeMember(PropertyInfo propertyInfo, bool isGenerated)
        {
            IsGenerated = isGenerated;

            typeMember = new TypeProperty(propertyInfo);
        }

        public AssemblyTypeMember(MethodInfo methodInfo, bool isGenerated, bool isExtension = false)
        {
            IsGenerated = isGenerated;

            typeMember = new TypeMethod(methodInfo, isExtension);
        }
    }
}
