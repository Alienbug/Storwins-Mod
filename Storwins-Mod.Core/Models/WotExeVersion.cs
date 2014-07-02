// This file is part of ModList.
// 
// ModList is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// ModList is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with ModList.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace Storwins_Mod.Core.Models
{
    public class WotExeVersion
    {
        private const String ResMod = "/res_mods/";
        public Int32 MajorVersion { get; set; }
        public Int32 MinorVersion { get; set; }
        public Int32 PatchVersion { get; set; }
        public Int32 DeveloperVersion { get; set; }
        public String ProductVersion { get; set; }
        public String ProductName { get; set; }
        public String Path { get; set; }
        public String ResModPath {
            get { return Path + ResMod;}
        }

        public String ResModVerPath
        {
            get
            {
                return String.Format("{0}/{1}/{2}.{3}.{4}/",
                    Path, ResMod, MajorVersion, MinorVersion, PatchVersion);
            }
        }
    }
}
