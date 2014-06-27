using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Storwins_Mod.Core.Models
{
    [Serializable]
    [XmlRoot("storwins_mod")]
    public class StorwinsMod
    {
        private string _version;
        private DateTime _updateDate;
        private DateTime _installDate;
        private string _wotVersion;
        private string _remotePath;
        private List<StorwinsModPart> _modsList;

        public StorwinsMod()
        {
            _modsList = new List<StorwinsModPart>();
        }

        [XmlElement("version")]
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        [XmlElement("last_update")]
        public DateTime UpdateDate
        {
            get { return _updateDate; }
            set { _updateDate = value; }
        }

        [XmlElement("last_install")]
        public DateTime InstallDate
        {
            get { return _installDate; }
            set { _installDate = value; }
        }

        [XmlElement("wot_version")]
        public string WotVersion
        {
            get { return _wotVersion; }
            set { _wotVersion = value; }
        }

        [XmlElement("remote_path")]
        public string RemotePath
        {
            get { return _remotePath; }
            set { _remotePath = value; }
        }

        [XmlArray("mods")]
        [XmlArrayItem("mod")]
        public List<StorwinsModPart> Mods
        {
            get { return _modsList; }
            set { _modsList = value; }
        }
    }
}
