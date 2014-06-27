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
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Serialization;
using Ionic.Zip;
using Storwins_Mod.Core.Models;

namespace Storwins_Mod.Core.Controllers
{
    public class ModCtr
    {
        /// <summary>
        /// Import the local version of the mod.
        /// </summary>
        /// <param name="filePath">Path to local version.</param>
        /// <returns>Installed mod</returns>
        public StorwinsMod ImportXml(string filePath)
        {
            //Todo: Consider using WotExeVersion for path.
            var tempMod = new StorwinsMod();
            if (File.Exists(filePath))
            {
                using (var testFileStream = File.OpenRead(filePath))
                {
                    var deserializer = new XmlSerializer(typeof(StorwinsMod));
                    tempMod = (StorwinsMod)deserializer.Deserialize(testFileStream);
                }
            }
            tempMod.Mods = tempMod.Mods.OrderBy(m => m.TypeOfMod).ToList();
            return tempMod;
        }
        /// <summary>
        /// Export the mod to a local version.
        /// </summary>
        /// <param name="filePath">Path to local version.</param>
        /// <param name="storwinsMod">Mod to be exported</param>
        public void ExportXml(string filePath, StorwinsMod storwinsMod)
        {
            //Todo: Consider using WotExeVersion for path.
            storwinsMod.UpdateDate = DateTime.UtcNow;
            using (var testFileStream = File.Create(filePath))
            {
                var serializer = new XmlSerializer(typeof(StorwinsMod));
                serializer.Serialize(testFileStream, storwinsMod);
                testFileStream.Close();
            }
        }

        /// <summary>
        /// Check remote mod, from the remote url provided by mod.
        /// </summary>
        /// <param name="urlToMod">Uri to modsource</param>
        /// <returns>Remote mod</returns>
        public StorwinsMod CheckXml(Uri urlToMod)
        {
            var tmpMod = new StorwinsMod();
            using (var client = new WebClient())
            {
                using (var data = client.OpenRead(urlToMod))
                {
                    var deserializer = new XmlSerializer(typeof(StorwinsMod));
                    if (data == null) return tmpMod;
                    tmpMod = (StorwinsMod)deserializer.Deserialize(data);
                }
            }
            return tmpMod;
        }

        /// <summary>
        /// For use with Github, enables reading of .atom from git commits.
        /// </summary>
        /// <param name="atomUri">Url to master.atom</param>
        /// <returns>List of Entries.</returns>
        public List<GitEntry> GetUpdateInfo(String atomUri)
        {
            SyndicationFeed feed;
            using (var reader = XmlReader.Create(atomUri))
            {
                feed = SyndicationFeed.Load(reader);
                if (feed == null)
                {
                    return new List<GitEntry>();
                }
            }
            return feed.Items.Select(entry => new GitEntry
            {
                Id = entry.Id,
                Title = entry.Title.Text,
                Content = ((TextSyndicationContent)entry.Content).Text,
                Updated = entry.LastUpdatedTime.DateTime
            }).ToList();
        }

        /// <summary>
        /// Update storwins mods, uses old mods to install new ones.  
        /// </summary>
        /// <param name="instStorwinsMods">Old installed mods</param>
        /// <param name="newStorwinsMods">New updated mods.</param>
        /// <returns>List of newly installed mods.</returns>
        public StorwinsMod UpdateMods(StorwinsMod instStorwinsMods, StorwinsMod newStorwinsMods)
        {
            newStorwinsMods.InstallDate = instStorwinsMods.InstallDate;
            foreach (var newModPart in instStorwinsMods.Mods.Where(mp => mp.IsInstalled).SelectMany(mp => newStorwinsMods.Mods.Where(newModPart => mp.TypeOfMod == newModPart.TypeOfMod && mp.Name.Equals(newModPart.Name))))
            {
                newModPart.IsInstalled = true;
            }
            return newStorwinsMods;
        }

        /// <summary>
        /// Unpacks a downloaded mod, zips are expected to be downloaded to /Updates/ in WoT folder.
        /// </summary>
        /// <param name="zipToUnpack">Zipfile that has been downloaded.</param>
        /// <param name="unpackDirectory">Path to World of tanks where mods are installed.</param>
        /// <returns>Files of unpacked zip.</returns>
        public List<String> ExtractMod(string zipToUnpack, string unpackDirectory)
        {
            var files = new List<String>();
            using (var zip1 = ZipFile.Read(zipToUnpack))
            {
                // here, we extract every entry, but we could extract conditionally
                // based on entry name, size, date, checkbox status, etc.  
                foreach (var e in zip1)
                {
                    if (!e.IsDirectory)
                    {
                        files.Add(e.FileName);
                    }
                    e.Extract(unpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                }
            }
            return files;
        }

        /// <summary>
        /// Discover files in the zip file.
        /// </summary>
        /// <param name="zipToRead">Path to zipfile.</param>
        /// <returns>Files</returns>
        public List<String> DiscoverMod(string zipToRead)
        {
            using (var zip1 = ZipFile.Read(zipToRead))
            {
                var files = zip1.EntriesSorted.Where(e => !e.IsDirectory).Select(e => e.FileName).ToList();
                return files;
            }
        }
    }
}
