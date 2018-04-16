namespace Rc.Common
{
    using System;
    using System.ComponentModel;

    public class EnumService
    {
        public static string GetDescription(Enum obj)
        {
            string name = obj.ToString();
            DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) obj.GetType().GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttributes[0].Description;
        }

        public static string GetDescription<T>(int value)
        {
            string name = Enum.GetName(typeof(T), value);
            if (name == null)
            {
                return "";
            }
            return GetDescription<T>(name);
        }

        public static string GetDescription<T>(string name)
        {
            Enum enum2 = GetEnum<T>(name) as Enum;
            return GetDescription(enum2);
        }

        public static T GetEnum<T>(string v)
        {
            return (T) Enum.Parse(typeof(T), v);
        }
    }
}

