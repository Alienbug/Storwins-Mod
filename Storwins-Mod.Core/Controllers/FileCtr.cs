using System;
using System.Collections.Generic;
using System.IO;
using Storwins_Mod.Core.Models;

namespace Storwins_Mod.Core.Controllers
{
    public class FileCtr
    {
        public int ModsFilesTooMany(string wotExePath, List<String> tmpModFiles)
        {
            var wotCtr = new WotCtr();
            var wotPath = wotCtr.GetExeVersion(wotExePath);
            var tmpDirFiles = DirSearch(wotPath.ResModPath);
            foreach (var file in tmpModFiles)
            {
                tmpDirFiles.Remove(wotPath.ResModPath + file);
            }
            tmpDirFiles.Remove(wotPath.ResModPath + "mods.xml");
            tmpDirFiles.Remove(wotPath.ResModVerPath + "readme.txt");
            return tmpDirFiles.Count;
        }

        public int ModsFilesTooLittle(string wotExePath, List<String> tmpModFiles)
        {
            var wotCtr = new WotCtr();
            WotExeVersion wotPath = wotCtr.GetExeVersion(wotExePath);
            List<String> tmpDirFiles = DirSearch(wotPath.ResModPath);
            foreach (var file in tmpDirFiles)
            {
                tmpModFiles.Remove(file.Substring(wotPath.ResModPath.Length));
            }
            return tmpModFiles.Count;
        }

        public List<string> RemoveFilesList(String wotExePath, List<String> dirList, List<String> modList)
        {
            var wotCtr = new WotCtr();
            WotExeVersion wotPath = wotCtr.GetExeVersion(wotExePath);
            foreach (var file in modList)
            {
                dirList.Remove(wotPath.ResModPath + file);
            }
            dirList.Remove(wotPath.ResModPath + "mods.xml");
            dirList.Remove(wotPath.ResModVerPath + "readme.txt");
            return dirList;
        }

        public List<string> RemoveDirFilesList(String wotExePath, List<String> dirList, List<String> modList)
        {
            var wotCtr = new WotCtr();
            WotExeVersion wotPath = wotCtr.GetExeVersion(wotExePath);
            foreach (var file in dirList)
            {
                modList.Remove(file.Substring(wotPath.ResModPath.Length));
            }
            return modList;
        }

        public void DeleteAllFiles(string wotPath)
        {
            foreach (string fileName in Directory.GetFiles(wotPath))
            {
                Console.WriteLine(fileName);
                File.Delete(fileName);
            }
        }

        public void WriteXmlConfig(String wotExePath, String xcPath)
        {
            var wotCtr = new WotCtr();
            WotExeVersion wotPath = wotCtr.GetExeVersion(wotExePath);
            xcPath = xcPath.Replace("\\", "/");
            var line = "${\"" + xcPath.Replace("/configs", "") + "\":\".\"}";
            if (!Directory.Exists(wotPath.ResModPath + "xvm/"))
            {
                Directory.CreateDirectory(wotPath.ResModPath + "xvm");
            }
            var xvmFile = new StreamWriter(wotPath.ResModPath + "xvm/configs/xvm.xc", false);
            xvmFile.WriteLine(line);
            xvmFile.Close();
        }

        public List<String> DirSearch(string sDir)
        {
            var files = new List<String>();
            try
            {
                files.AddRange(Directory.GetFiles(sDir));
                foreach (var d in Directory.GetDirectories(sDir))
                {
                    files.AddRange(DirSearch(d));
                }
            }
            catch (Exception excpt)
            {
                Console.Write(excpt.Message);
            }

            return files;
        }
    }
}
