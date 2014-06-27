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
using System.Xml.Serialization;

namespace Storwins_Mod.Core.Models
{
    [XmlRoot(ElementName="version.xml")]
    public class WotXmlVersion {
        [XmlElement(ElementName="appname")]
        public String Appname;

        [XmlElement(ElementName="version")]
        public String Version;
        
        [XmlElement(ElementName="showLicense")]
        public String ShowLicense;
        
        [XmlElement(ElementName="ingameHelpVersion")]
        public String IngameHelpVersion;
        
        [XmlElement(ElementName="meta")]
        public Meta Meta;
    }
    
    [XmlRoot(ElementName = "meta")]
    public class Meta
    {
        [XmlElement(ElementName = "client")]
        public String Client;

        [XmlElement(ElementName = "overrides")]
        public String Overrides;

        [XmlElement(ElementName = "localization")]
        public String Localization;
    }
}
