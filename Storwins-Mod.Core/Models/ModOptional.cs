using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Storwins_Mod.Core.Models
{
    [Serializable]
    [XmlRoot("storwins_mod")]
    public class ModOptional
    {
        [XmlElement("name")]
        public String Name { get; set; }

        [XmlElement("types")]
        public String Types { get; set; }

        [XmlElement("is_installed")]
        public Boolean IsInstalled { get; set; }

        [XmlArray("files")]
        [XmlArrayItem("filename")]
        public List<String> Files { get; set; }
    }
}