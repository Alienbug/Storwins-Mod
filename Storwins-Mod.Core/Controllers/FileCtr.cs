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
using Storwins_Mod.Core.Models;

namespace Storwins_Mod.Core.Controllers
{
    public class FileCtr
    {
        /// <summary>
        /// Checks if there are files not belonging to the given mod.
        /// </summary>
        /// <param name="wotExe">Wot exe path</param>
        /// <param name="modFiles">Mod files that is suppose to be there</param>
        /// <returns>List of files that are not part of the mod</returns>
        public List<String> ModsFilesTooMany(WotExeVersion wotExe, List<String> modFiles)
        {
            var tmpDirFiles = DirSearch(wotExe.ResModPath);
            foreach (var file in modFiles)
            {
                tmpDirFiles.Remove(wotExe.ResModPath + file);
            }
            tmpDirFiles.Remove(wotExe.ResModPath + "mods.xml");
            tmpDirFiles.Remove(wotExe.ResModVerPath + "readme.txt");
            return tmpDirFiles;
        }

        /// <summary>
        /// Checks if files are missing from the mod
        /// </summary>
        /// <param name="wotExe">Wot exe path</param>
        /// <param name="modFiles">Mod files that is suppose to be there</param>
        /// <returns>Files that are missing from the mod</returns>
        public List<String> ModsFilesTooLittle(WotExeVersion wotExe, List<String> modFiles)
        {
            List<String> tmpDirFiles = DirSearch(wotExe.ResModPath);
            foreach (var file in tmpDirFiles)
            {
                modFiles.Remove(file.Substring(wotExe.ResModPath.Length));
            }
            return modFiles;
        }

        /// <summary>
        /// This will delete all files in res_mods of the world of tanks folder.
        /// </summary>
        /// <param name="wotExe">Wot exe path</param>
        public void DeleteAllFiles(WotExeVersion wotExe)
        {
            foreach (string fileName in Directory.GetFiles(wotExe.ResModPath))
            {
                Console.WriteLine(fileName);
                File.Delete(fileName);
            }
        }

        /// <summary>
        /// Rewrite the xvm config file.
        /// </summary>
        /// <param name="wotExe">Wot exe path</param>
        /// <param name="xcPath">The path to the new xvm config file</param>
        public void WriteXmlConfig(WotExeVersion wotExe, String xcPath)
        {
            xcPath = xcPath.Replace("\\", "/");
            var line = "${\"" + xcPath.Replace("/configs", "") + "\":\".\"}";
            if (!Directory.Exists(wotExe.ResModPath + "xvm/"))
            {
                Directory.CreateDirectory(wotExe.ResModPath + "xvm");
            }
            var xvmFile = new StreamWriter(wotExe.ResModPath + "xvm/configs/xvm.xc", false);
            xvmFile.WriteLine(line);
            xvmFile.Close();
        }

        /// <summary>
        /// Returns all files in a dir.
        /// Will visit all subdirs.
        /// </summary>
        /// <param name="sDir">Directory to look through.</param>
        /// <returns>List of files in the Directory</returns>
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
