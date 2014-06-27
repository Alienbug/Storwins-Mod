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
        public StorwinsMod ImportXml(string fileName)
        {
            var tempMod = new StorwinsMod();
            if (File.Exists(fileName))
            {
                using (var testFileStream = File.OpenRead(fileName))
                {
                    var deserializer = new XmlSerializer(typeof(StorwinsMod));
                    tempMod = (StorwinsMod)deserializer.Deserialize(testFileStream);
                }
            }
            tempMod.Mods = tempMod.Mods.OrderBy(m => m.TypeOfMod).ToList();
            return tempMod;
        }

        public void ExportXml(string fileName, StorwinsMod storwinsMod)
        {
            storwinsMod.UpdateDate = DateTime.UtcNow;
            using (var testFileStream = File.Create(fileName))
            {
                var serializer = new XmlSerializer(typeof(StorwinsMod));
                serializer.Serialize(testFileStream, storwinsMod);
                testFileStream.Close();
            }
        }

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

        public StorwinsMod UpdateMods(StorwinsMod instStorwinsMods, StorwinsMod newStorwinsMods)
        {
            newStorwinsMods.InstallDate = instStorwinsMods.InstallDate;
            foreach (var newModPart in instStorwinsMods.Mods.Where(mp => mp.IsInstalled).SelectMany(mp => newStorwinsMods.Mods.Where(newModPart => mp.TypeOfMod == newModPart.TypeOfMod && mp.Name.Equals(newModPart.Name))))
            {
                newModPart.IsInstalled = true;
            }
            return newStorwinsMods;
        }

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
