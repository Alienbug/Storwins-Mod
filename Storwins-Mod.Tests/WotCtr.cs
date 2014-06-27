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
