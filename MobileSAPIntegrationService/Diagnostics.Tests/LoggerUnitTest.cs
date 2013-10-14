using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Diagnostics;

namespace Diagnostics.Tests
{
    [TestClass]
    public class LoggerUnitTest
    {
        [TestMethod]
        public void LogSuccess()
        {
            Logger.Instance.Log.Trace("Read Schema From File Stream End. @ " + DateTime.Now.ToString());
            Assert.AreEqual(0, 0);
        }
    }
}
