using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib.AssemblyData
{
    public static class DataAccessModificator
    {
        public enum DataAccessModificatorEnum
        {
            PublicModificator,
            PrivateModificator,
            ProtectedModificator,
            InternalModificator,
            ProtectedInternalModificator,
            ProtectedPrivateModificator,
            NotDefined,
        }

        private static Dictionary<DataAccessModificatorEnum, string> enumStringRepresentation = new Dictionary<DataAccessModificatorEnum, string>
        {
            {DataAccessModificatorEnum.PublicModificator,  "public"},
            {DataAccessModificatorEnum.PrivateModificator,  "private"},
            {DataAccessModificatorEnum.ProtectedModificator,  "protected"},
            {DataAccessModificatorEnum.InternalModificator,  "internal"},
            {DataAccessModificatorEnum.ProtectedInternalModificator,  "protected internal"},
            {DataAccessModificatorEnum.ProtectedPrivateModificator,  "protected private"},
            {DataAccessModificatorEnum.NotDefined,  "not defined modifier"},
        };

        public static string GetString(DataAccessModificatorEnum modifier)
        {
            string result;
            if (enumStringRepresentation.TryGetValue(modifier, out result))
            {
                return result;
            }
            return null;               
        }

        public static DataAccessModificatorEnum GetTypeModifiers(Type typeInfo)
        {
            return typeInfo.IsNestedPrivate ?
                DataAccessModificatorEnum.PrivateModificator :
            typeInfo.IsNestedFamily ?
                DataAccessModificatorEnum.ProtectedModificator :
            typeInfo.IsNestedAssembly ?
                DataAccessModificatorEnum.InternalModificator :
            typeInfo.IsNestedFamORAssem ?
                DataAccessModificatorEnum.ProtectedInternalModificator :
            typeInfo.IsNestedFamANDAssem ?
                DataAccessModificatorEnum.ProtectedPrivateModificator :
            typeInfo.IsNestedPublic || typeInfo.IsPublic ?
                DataAccessModificatorEnum.PublicModificator :
            typeInfo.IsNotPublic ?
                DataAccessModificatorEnum.PrivateModificator :
                DataAccessModificatorEnum.PublicModificator;
        }

        public static DataAccessModificatorEnum GetTypeModifiers(MethodInfo memberInfo)
        {
            return memberInfo.IsPrivate ? 
                DataAccessModificatorEnum.PrivateModificator :
            memberInfo.IsFamily ? 
                DataAccessModificatorEnum.ProtectedModificator :
            memberInfo.IsAssembly ? 
                DataAccessModificatorEnum.InternalModificator :
            memberInfo.IsFamilyOrAssembly ? 
                DataAccessModificatorEnum.ProtectedInternalModificator :
            memberInfo.IsFamilyAndAssembly ? 
                DataAccessModificatorEnum.ProtectedPrivateModificator :
            memberInfo.IsPublic ? 
                DataAccessModificatorEnum.PublicModificator : 
                DataAccessModificatorEnum.NotDefined;
        }

        public static DataAccessModificatorEnum GetTypeModifiers(FieldInfo fieldInfo)
        {
            return fieldInfo.IsPrivate ?
                DataAccessModificatorEnum.PrivateModificator :
            fieldInfo.IsFamily ?
                DataAccessModificatorEnum.ProtectedModificator :
            fieldInfo.IsAssembly ?
                DataAccessModificatorEnum.InternalModificator :
            fieldInfo.IsFamilyOrAssembly ?
                DataAccessModificatorEnum.ProtectedInternalModificator :
            fieldInfo.IsFamilyAndAssembly ?
                DataAccessModificatorEnum.ProtectedPrivateModificator :
            fieldInfo.IsPublic ?
                DataAccessModificatorEnum.PublicModificator :
                DataAccessModificatorEnum.NotDefined;
        }
    }
}
