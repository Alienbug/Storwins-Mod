// This file is part of StorwinsMod.
// 
// StorwinsMod is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// StorwinsMod is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with StorwinsMod.  If not, see <http://www.gnu.org/licenses/>.

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
        /// <summary>
        /// Gets the version of WoT will keep path.
        /// </summary>
        /// <param name="path">Path to world of tanks exe.</param>
        /// <returns>WotExeVersion</returns>
        public WotExeVersion GetExeVersion(String path)
        {
            var pathToExe = new StringBuilder();
            pathToExe.AppendFormat(@"{0}\{1}", path, "WorldOfTanks.exe");

            var fileInfo = FileVersionInfo.GetVersionInfo(pathToExe.ToString());
            var wotVersion = new WotExeVersion
            {
                ProductName = fileInfo.ProductName,
                ProductVersion = fileInfo.ProductVersion,
                MajorVersion = fileInfo.ProductMajorPart,
                MinorVersion = fileInfo.ProductMinorPart,
                PatchVersion = fileInfo.ProductBuildPart,
                DeveloperVersion = fileInfo.ProductPrivatePart,
                Path = path
            };
            return wotVersion;
        }

        /// <summary>
        /// Read XML version, this is used to identify subversions of the game.
        /// </summary>
        /// <param name="path">Path to world of tanks exe.</param>
        /// <returns>WotXmlVersion</returns>
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
