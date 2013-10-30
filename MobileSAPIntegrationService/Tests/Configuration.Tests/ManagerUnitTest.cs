using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Configuration;

namespace Configuration.Tests
{
    [TestClass]
    public class ManagerUnitTest
    {
        [TestMethod]
        public void GetConfigSettingsSuccess()
        {
            Manager manager = new Manager();
            SapConnection sapConnection = manager.GetSapConfigurationInformation();

            Assert.AreNotEqual(null, sapConnection.Host);
        }
    }
}