using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowserLib.AssemblyData
{
    public static class DataAttribute
    {
        public enum DataTypeAttributeEnum
        {
            SealedAttribute,
            AbstractAttribute,
            StaticAttribute,
            VirtualAttribute,
            NotDefined,
        }

        private static Dictionary<DataTypeAttributeEnum, string> enumStringRepresentation = new Dictionary<DataTypeAttributeEnum, string>
        {
            {DataTypeAttributeEnum.SealedAttribute,  "sealed"},
            {DataTypeAttributeEnum.AbstractAttribute,  "abstract"},
            {DataTypeAttributeEnum.StaticAttribute,  "static"},
            {DataTypeAttributeEnum.VirtualAttribute,  "virtual"},
            {DataTypeAttributeEnum.NotDefined,  ""},
        };

        public static string GetString(DataTypeAttributeEnum modifier)
        {
            string result;
            if (enumStringRepresentation.TryGetValue(modifier, out result))
            {
                return result;
            }
            return null;
        }

        public static DataTypeAttributeEnum GetTypeAtributes(Type typeInfo)
        {
            return typeInfo.IsAbstract && typeInfo.IsSealed ?
                DataTypeAttributeEnum.StaticAttribute :
            typeInfo.IsAbstract ?
                DataTypeAttributeEnum.AbstractAttribute :
            typeInfo.IsSealed ?
                DataTypeAttributeEnum.SealedAttribute :
                DataTypeAttributeEnum.NotDefined;
        }

        public static DataTypeAttributeEnum GetAttributes(MethodInfo methodInfo)
        {
            return methodInfo.IsVirtual ?
                DataTypeAttributeEnum.VirtualAttribute :
            methodInfo.IsAbstract ?
                DataTypeAttributeEnum.AbstractAttribute :
            methodInfo.IsStatic ?
                DataTypeAttributeEnum.StaticAttribute :
                DataTypeAttributeEnum.NotDefined;
        }
    }
}
