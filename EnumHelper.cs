using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;

namespace LaserParamsConverterBlazor
{
    public static class EnumHelper
    {
        public static IEnumerable<string> GetEnumDescriptions(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("The specified type is not an enum.");
            }

            var descriptions = new List<string>();

            foreach (var value in Enum.GetValues(enumType))
            {
                var fieldInfo = enumType.GetField(value.ToString());

                var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));

                if (descriptionAttribute != null)
                {
                    descriptions.Add(descriptionAttribute.Description);
                }
                else
                {
                    descriptions.Add(value.ToString());
                }
            }

            return descriptions;
        }

        public static string GetEnumDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));            

            return descriptionAttribute?.Description ?? descriptionAttribute.Description;
        }

        public static LaserType GetLaserTypeFromDescription(string description)
        {
            var enumType = typeof(LaserType);

            foreach (var value in Enum.GetValues(enumType))
            {
                var fieldInfo = enumType.GetField(value.ToString());
                var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));

                if (descriptionAttribute != null && descriptionAttribute.Description == description)
                {
                    return (LaserType)value;
                }
            }

            throw new ArgumentException("No matching enum value found for the given description.", nameof(description));
        }
    }
}
