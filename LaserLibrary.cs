using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace LaserParamsConverterBlazor
{

    public enum LaserType
    {
        [Display(Name = "CO2 Gantry")]
        CO2,
        [Display(Name = "Fiber Laser")]
        Fiber
    };

    public enum LibraryType { EZCAD2, EZCAD3, LightBurn, CSV };

    static class LaserHelper
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string? name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo? field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute? attr =
                                 Attribute.GetCustomAttribute(field,
                                     typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return value.ToString();
        }

        public static int ParseToInt(string value)
        {
            return (int)Math.Round(float.Parse(value));
        }

        public static int ParseToInt(string value, NumberFormatInfo nfi)
        {
            return (int)Math.Round(float.Parse(value, nfi));
        }

        public static int ParseToInt(string value, NumberStyles styles, NumberFormatInfo nfi)
        {
            return (int)Math.Round(float.Parse(value, styles, nfi));
        }

    }

    public class LaserLibrary
    {

    }

    public class LaserParam
    {
        public int Speed { get; set; }
        public int Power { get; set; }

        public LaserParam(int speed, int power)
        {
            Speed = speed;
            Power = power;
        }

        public void Convert(LaserType laser, int maxPower, int inLens, int inWatts, int outLens, int outWatts)
        {
            Decimal powerMod;   // modifier = (power * outputLens) / inputLens
            Decimal speedMod;   // speedMod = power / outPower
            int outPower;       // outPower = (inputWattage * modifier) / outputWattage
            int outSpeed;       // if (outPower > maxPower) (inSpeed * speedMod)

            if (laser == LaserType.CO2)
            {
                inLens = 1;
                outLens = 1;
            }

            powerMod = ((Decimal)Power * (Decimal)outLens) / (Decimal)inLens;
            outPower = Decimal.ToInt32((inWatts * powerMod) / (Decimal)outWatts);
            outSpeed = Speed;

            if (outPower > maxPower)
            {
                speedMod = (Decimal)Power / (Decimal)outPower;
                outSpeed = Decimal.ToInt32(Speed * speedMod);
                outPower = maxPower;
            }

            Speed = outSpeed;
            Power = outPower;
        }
    }
}
