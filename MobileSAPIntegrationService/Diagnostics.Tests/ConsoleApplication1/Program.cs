using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Logger.Instance.Log.Log(NLog.LogLevel.Debug, testing);
                Logger.Instance.Log.Log(NLog.LogLevel.Debug, "test");
                Logger.Instance.Log.Trace("Read Schema From File Stream End. @ " + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {

            }
        }
    }
}
