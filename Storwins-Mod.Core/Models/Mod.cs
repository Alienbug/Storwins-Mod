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
    [XmlRoot("mod")]
    public class Mod
    {
        public Mod(String modName, String modVersion, String modDescription, Boolean isDefault, ModTypes modType)
        {
            Name = modName;
            Version = modVersion;
            Description = modDescription;
            IsDefault = isDefault;
            TypeOfMod = modType;
            Files = new List<string>();
        }

        public Mod()
        {
            Name = "Unset";
            Version = "Unset";
            Description = "Unset";
            IsDefault = false;
            TypeOfMod = ModTypes.Misc;
            Files = new List<string>();
        }

        [XmlElement("name")]
        public String Name { get; set; }
        [XmlElement("last_update")]
        public DateTime LastUpdate { get; set; }

        [XmlElement("version")]
        public String Version { get; set; }

        [XmlElement("description")]
        public String Description { get; set; }

        [XmlElement("is_installed")]
        public Boolean IsInstalled { get; set; }

        [XmlElement("type")]
        public ModTypes TypeOfMod { get; set; }
   
        public Int32 FileCount
        {
            get { return Files.Count; }
        }

        [XmlArray("files")]
        [XmlArrayItem("filename")]
        public List<String> Files { get; set; }

        [XmlArray("optionals")]
        public List<ModOptional> Optionals { get; set; }

        [XmlElement("zipfile")]
        public String ZipFile { get; set; }

        [XmlElement("is_default")]
        public Boolean IsDefault { get; set; }
    }
}
