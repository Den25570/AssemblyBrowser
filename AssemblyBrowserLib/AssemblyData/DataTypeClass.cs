using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib.AssemblyData
{
    public static class DataTypeClass
    {
        public enum DataTypeClassEnum
        {
            InterfaceType,
            EnumType,
            StructType,
            DelegateType,
            ClassType,
            NotDefined,
        }

        private static Dictionary<DataTypeClassEnum, string> enumStringRepresentation = new Dictionary<DataTypeClassEnum, string>
        {
            {DataTypeClassEnum.InterfaceType,  "interface"},
            {DataTypeClassEnum.EnumType,  "enum"},
            {DataTypeClassEnum.StructType,  "struct"},
            {DataTypeClassEnum.DelegateType,  "delegate"},
            {DataTypeClassEnum.ClassType,  "class"},
            {DataTypeClassEnum.NotDefined,  ""},
        };

        public static string GetString(DataTypeClassEnum modifier)
        {
            string result;
            if (enumStringRepresentation.TryGetValue(modifier, out result))
            {
                return result;
            }
            return null;
        }

        public static DataTypeClassEnum GetTypeClass(Type typeInfo)
        {
            return typeInfo.IsInterface ?
                DataTypeClassEnum.InterfaceType :
            typeInfo.IsEnum ?
                DataTypeClassEnum.EnumType :
            typeInfo.IsValueType ?
                DataTypeClassEnum.StructType :
            (typeInfo.BaseType == typeof(MulticastDelegate)) ?
                DataTypeClassEnum.DelegateType :
            typeInfo.IsClass ?
                DataTypeClassEnum.ClassType :
                DataTypeClassEnum.NotDefined;
        }
    }
}
