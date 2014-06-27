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
using Storwins_Mod.Core.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Storwins_Mod.Tests
{
    [TestClass]
    public class TestWotCtr
    {
        [TestMethod]
        public void TestWotExeVersion()
        {
            var wotCtr = new WotCtr();
            // Manual test. Expects wot to be here: C:\Games\World_Of_Tanks
            const String pathToWoT = @"C:\Games\World_Of_Tanks";
            var exeVer = wotCtr.GetExeVersion(pathToWoT);
            var actualVersion = String.Format("{0}, {1}, {2}, {3}", exeVer.MajorVersion, exeVer.MinorVersion,
                exeVer.PatchVersion, exeVer.DeveloperVersion);
            Assert.AreEqual(actualVersion, exeVer.ProductVersion);
        }

        [TestMethod]
        public void TestWotXmlVersion()
        {
            var wotCtr = new WotCtr();
            // Manual test. Expects wot to be here: C:\Games\World_Of_Tanks
            const String pathToWoT = @"C:\Games\World_Of_Tanks";
            var xmlVer = wotCtr.GetXmlVersion(pathToWoT);
            // Replace version with actual.
            Assert.AreEqual(" v.0.9.1 #719", xmlVer.Version);
        }
    }
}
