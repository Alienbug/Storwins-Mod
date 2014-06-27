using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Storwins_Mod.Core.Models;

namespace Storwins_Mod.Core.Controllers
{
    public class WotCtr
    {
        public WotExeVersion GetExeVersion(String path)
        {
            var pathToExe = new StringBuilder();
            pathToExe.AppendFormat(@"{0}\{1}", path, "WorldOfTanks.exe");

            var fileInfo = FileVersionInfo.GetVersionInfo(pathToExe.ToString());
            var wotVersion = new WotExeVersion
            {
                FileVersion = fileInfo.FileVersion,
                ProductName = fileInfo.ProductName,
                ProductVersion = fileInfo.ProductVersion
            };
            return wotVersion;
        }

        public WotXmlVersion GetXmlVersion(String path)
        {
            var pathToXml = new StringBuilder();
            pathToXml.AppendFormat(@"{0}\{1}", path, "version.xml");
            var deserializer = new XmlSerializer(typeof(WotXmlVersion));
            var tmpMod = (WotXmlVersion)deserializer.Deserialize(File.OpenRead(pathToXml.ToString()));
            return tmpMod;
        }
    }
}
