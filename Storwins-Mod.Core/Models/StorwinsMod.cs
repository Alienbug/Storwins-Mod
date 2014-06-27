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
