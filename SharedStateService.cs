using LaserParamsConverterBlazor.Shared;
using Microsoft.Extensions.Primitives;
using System.Xml;

namespace LaserParamsConverterBlazor
{
    public class SharedStateService
    {
        public int MaxPower { get; set; } = 80;
        public int InLens { get; set; }
        public int InWattage { get; set; }
        public int InSpeed { get; set; }
        public int InPower { get; set; }
        public int OutLens { get; set; }
        public int OutWattage { get; set; }
        public int OutSpeed { get; set; }
        public int OutPower { get; set; }

        public string? FileName { get; set; }
        public XmlDocument? XmlDoc { get; set; }

        public LaserType Laser { get; set; }
        public LibraryType Format { get; set; }

        private string RootNodeName
        {
            get
            {
                switch (Format)
                {
                    case LibraryType.EZCAD2:
                    case LibraryType.EZCAD3:
                        return "EZCADLibrary";
                    case LibraryType.LightBurn:
                        return "LightBurnLibrary";
                    default:
                        return "CSVLibrary";
                }
            }
        }

        public string RootPath
        {
            get { return string.Format("//{0}", RootNodeName); }
        }
        public string MaterialPath
        {
            get { return string.Format("//{0}/Material", RootNodeName); }
        }

        public string EntryPath
        {
            get { return string.Format("//{0}/Material/Entry", RootNodeName); }
        }

        public string CutSettingPath
        {
            get { return string.Format("//{0}/Material/Entry/CutSetting", RootNodeName); }
        }

    }
}
