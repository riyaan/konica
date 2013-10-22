using ConsoleApplication1.ServiceReference1;
using Diagnostics;
using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TestMisc();
                //TestServiceCall();
                //TestLogging();
            }
            catch (Exception ex)
            {
            }
        }

        private static void TestMisc()
        {
            string p = "{0, " + 18 + "}";
            string q = String.Format("{0, " + 18 + "}", "1391-1016-01");

            string[] paramDetail = "GOODSMVT_ITEM[0]:MATERIAL".Split(':');

            string r = "{0, " + 2 + "}";
            string s = String.Format(r, "1391-1016-01");
        }

        private static void TestLogging()
        {
            Logger.Instance.Log.Log(NLog.LogLevel.Debug, "testing");
            Logger.Instance.Log.Log(NLog.LogLevel.Debug, "test @ " + DateTime.Now.ToString());
        }

        private static void TestServiceCall()
        {            
            GOODSMVT_ITEM[] gmvtItem = new GOODSMVT_ITEM[1];
            gmvtItem[0] = new GOODSMVT_ITEM()
            {
                ENTRY_QNT = "entQ",
                MATERIAL = "mat",
                PLANT = "Plant",
                STGE_LOC = "stge_log"
            };

            TIMETICKETS[] tt = new TIMETICKETS[1];
            tt[0] = new TIMETICKETS()
            {
                ACT_WORK = "aw",
                ACT_WORK_2 = "aw2",
                CONF_TEXT = "ct",
                EX_CREATED_BY = "riyaan",
                EX_CREATED_DATE = DateTime.Now.ToShortDateString(),
                EX_CREATED_TIME = DateTime.Now.ToShortTimeString()
            };

            ServiceReference1.MobileServiceClient client = new ServiceReference1.MobileServiceClient();
            client.doZaBAPI("orderNumber", "bwclose", "bwopen", "colClose", "colOpen",
                "riyaan", "c:\temp", DateTime.Now.ToString(), gmvtItem, tt);
        }
    }
}
