using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KonicaMinolta.SAP.Integration;

namespace SAP.Integration.Tests
{
    [TestClass]
    public class ProcessSAPFunctionTest
    {
        [TestMethod]
        public void Success()
        {
            //SAPFunction BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM = new SAPFunction();

            //BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.setName("BAPI_GOODSMVT_CREATE");
            //BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddInputParameter("GOODSMVT_CODE:GM_CODE", "03");//Not harcoded (Example: "DR311K")
            //BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddInputParameter("GOODSMVT_ITEM:MATERIAL", "DR311K");//Not harcoded (Example: "DR311K")
            //BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddInputParameter("GOODSMVT_ITEM:PLANT", "1010");//Not hardcoded (Example: "1020")
            //BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddInputParameter("GOODSMVT_ITEM:STGE_LOC", "J518");//Not Hardcoded (Example: "JM15")
            //BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddInputParameter("GOODSMVT_ITEM:ORDERID", "000010912095");//Not Hardcoded (Example: "10909960")
            //BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddInputParameter("GOODSMVT_ITEM:ENTRY_QNT", "0000001.000");//Not Hardcoded (Example: "1")
            //BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddInputParameter("GOODSMVT_ITEM:ENTRY_UOM", "EA");//Can be hard coded
            //BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddInputParameter("GOODSMVT_ITEM:MOVE_TYPE", "261");// can be hardcoded
            //BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddOutputParameter("RETURN:MESSAGE");
            //BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddOutputParameter("RETURN:LOG_NO");
            //BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddOutputParameter("RETURN:LOG_MSG_NO");
            //BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddOutputParameter("RETURN:MESSAGE_V1");
            //BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddOutputParameter("RETURN:MESSAGE_V2");
            //BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddOutputParameter("RETURN:MESSAGE_V3");
            //BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddOutputParameter("RETURN:MESSAGE_V4");
            //SAPProcessRemoteFunction p1 = new SAPProcessRemoteFunction();
            //p1.ProcessSAPFunction(BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM);
        }
    }
}
