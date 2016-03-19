using System;
using System.Text;
using System.Collections.Generic;
using InstaSync.Models.Models;
using InstaSync.UOW.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InstaSync.Test.UOW
{
    [TestClass]
    public class HelpersTest
    { 
        [TestMethod]
        public void T_CreateXml_Xml()
        {
            Settings settings = new Settings()
            {
                User = "user1",
                Path = @"C...///"
            };

            XmlHelper.SaveXml(settings);

            //Assert.IsNotNull(xml);
        }

        [TestMethod]
        public void Path_GetXml_Xml()
        {
            //string path = "Settings.xml";

            //var xml = XmlHelper.GetXml(path);

            //Assert.IsNotNull(xml);
        }
        [TestMethod]
        public void Path_GetModel_SettingsModel()
        {
            string path = "Settings.xml";

            var model = XmlHelper.GetModel(path, new Settings());

            Assert.IsNotNull(model);
        }
    }
}
