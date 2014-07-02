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
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Storwins_Mod.Core.Models
{
    [Serializable]
    [XmlRoot("storwins_mod")]
    public class ModList
    {
        public ModList()
        {
            Mods = new List<Mod>();
        }

        [XmlElement("mod_version")]
        public string Version { get; set; }

        [XmlElement("last_update")]
        public DateTime UpdateDate { get; set; }

        [XmlElement("last_install")]
        public DateTime InstallDate { get; set; }

        [XmlElement("wot_version")]
        public string WotVersion { get; set; }

        [XmlElement("remote_path")]
        public string RemotePath { get; set; }

        [XmlArray("mods")]
        [XmlArrayItem("mod")]
        public List<Mod> Mods {get; set; }
    }
}
