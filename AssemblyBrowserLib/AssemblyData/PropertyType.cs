using System.Reflection;

namespace AssemblyBrowserLib.AssemblyData
{
    public static class PropertyType
    {
        public enum PropertyTypeEnum
        {
            NonExist,
            Public,
            Private,
        }

        public static string GetGetString(PropertyTypeEnum propertyType)
        {
            switch(propertyType)
            {
                case PropertyTypeEnum.NonExist: return "";
                case PropertyTypeEnum.Public: return "public get";
                case PropertyTypeEnum.Private: return "private get";
            }
            return null;
        }

        public static string GetSetString(PropertyTypeEnum propertyType)
        {
            switch (propertyType)
            {
                case PropertyTypeEnum.NonExist: return "";
                case PropertyTypeEnum.Public: return "public set";
                case PropertyTypeEnum.Private: return "private set";
            }
            return null;
        }

        public static PropertyTypeEnum GetGetPropertyType(PropertyInfo propertyInfo)
        {
            return propertyInfo.CanRead ? propertyInfo.GetGetMethod(false) != null ? 
                PropertyTypeEnum.Public : 
                PropertyTypeEnum.Private : 
                PropertyTypeEnum.NonExist;
        }

        public static PropertyTypeEnum GetSetPropertyType(PropertyInfo propertyInfo)
        {
            return propertyInfo.CanWrite ? propertyInfo.GetSetMethod(false) != null ?
                PropertyTypeEnum.Public :
                PropertyTypeEnum.Private :
                PropertyTypeEnum.NonExist;
        }
    }
}
