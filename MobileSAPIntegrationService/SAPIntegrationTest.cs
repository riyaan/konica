using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace KonicaMinolta.SAP.Integration
{
    class SAPIntegrationTest
    {
        static void Main(string[] args)
        {

            /*#region BAPI_ALM_ORDER_MAINTAIN_IT_HEADER

            SAPFunction BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM = new SAPFunction();

            BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.setName("BAPI_GOODSMVT_CREATE");
            BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddInputParameter("GOODSMVT_CODE:GM_CODE", "03");//Not harcoded (Example: "DR311K")
            BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddInputParameter("GOODSMVT_ITEM:MATERIAL", "DR311K");//Not harcoded (Example: "DR311K")
            BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddInputParameter("GOODSMVT_ITEM:PLANT", "1010");//Not hardcoded (Example: "1020")
            BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddInputParameter("GOODSMVT_ITEM:STGE_LOC", "J518");//Not Hardcoded (Example: "JM15")
            BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddInputParameter("GOODSMVT_ITEM:ORDERID", "000010912095");//Not Hardcoded (Example: "10909960")
            BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddInputParameter("GOODSMVT_ITEM:ENTRY_QNT", "0000001.000");//Not Hardcoded (Example: "1")
            BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddInputParameter("GOODSMVT_ITEM:ENTRY_UOM", "EA");//Can be hard coded
            BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddInputParameter("GOODSMVT_ITEM:MOVE_TYPE", "261");// can be hardcoded
            BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddOutputParameter("RETURN:MESSAGE");
            BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddOutputParameter("RETURN:LOG_NO");
            BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddOutputParameter("RETURN:LOG_MSG_NO");
            BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddOutputParameter("RETURN:MESSAGE_V1");
            BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddOutputParameter("RETURN:MESSAGE_V2");
            BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddOutputParameter("RETURN:MESSAGE_V3");
            BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM.AddOutputParameter("RETURN:MESSAGE_V4");
            SAPProcessRemoteFunction p1 = new SAPProcessRemoteFunction();
            p1.ProcessSAPFunction(BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM);

            #endregion*/
            
            
            /*#region BAPI_ALM_ORDER_MAINTAIN_IT_HEADER

            SAPFunction BAPI_ALM_ORDER_MAINTAIN_IT_HEADER = new SAPFunction();
            BAPI_ALM_ORDER_MAINTAIN_IT_HEADER.setName("BAPI_ALM_ORDER_MAINTAIN_IT_HEADER");
            BAPI_ALM_ORDER_MAINTAIN_IT_HEADER.AddInputParameter("ORDERID", "???");//Not Hardcoded (Example: "10909960") -> String
            BAPI_ALM_ORDER_MAINTAIN_IT_HEADER.AddInputParameter("SHORT_TEXT", "???");//Not Hardcoded (Example: "Accepted") -> String

            SAPProcess p2 = new SAPProcess();
            p2.ProcessSAPFunction(BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM);

            #endregion

            #region BAPI_ALM_ORDER_MAINTAIN_IT_METHODS

            SAPFunction BAPI_ALM_ORDER_MAINTAIN_IT_METHODS = new SAPFunction();
            BAPI_ALM_ORDER_MAINTAIN_IT_METHODS.setName("BAPI_ALM_ORDER_MAINTAIN_IT_METHODS");
            BAPI_ALM_ORDER_MAINTAIN_IT_METHODS.AddInputParameter("OBJECTKEY", "???");//Not Hardcoded (Example: "10909960") -> String
            BAPI_ALM_ORDER_MAINTAIN_IT_METHODS.AddInputParameter("REFNUMBER", "000001");//Not Hardcoded (Example: "000001" / "000002") -> String
            BAPI_ALM_ORDER_MAINTAIN_IT_METHODS.AddInputParameter("OBJECTTYPE", "USERSTATUS");//Not Hardcoded (Example: "USERSTATUS" / "TECHNICALCOMPLETE") -> String
            BAPI_ALM_ORDER_MAINTAIN_IT_METHODS.AddInputParameter("METHOD", "CHANGE");//Not Hardcoded (Example: "CHANGE" / "UPDATE" /  "SAVE") -> String

            SAPProcess p3 = new SAPProcess();
            p3.ProcessSAPFunction(BAPI_ALM_ORDER_MAINTAIN_IT_METHODS);

            #endregion

            #region BAPI_ALM_ORDER_MAINTAIN_IT_USERSTATUS

            SAPFunction BAPI_ALM_ORDER_MAINTAIN_IT_USERSTATUS = new SAPFunction();
            BAPI_ALM_ORDER_MAINTAIN_IT_USERSTATUS.setName("BAPI_ALM_ORDER_MAINTAIN_IT_USERSTATUS");
            BAPI_ALM_ORDER_MAINTAIN_IT_USERSTATUS.AddInputParameter("USER_ST_TEXT", "???");//Not Hardcoded (Example: "10909960")
            BAPI_ALM_ORDER_MAINTAIN_IT_USERSTATUS.AddInputParameter("LANGU", "E");//Can be hard coded
            BAPI_ALM_ORDER_MAINTAIN_IT_USERSTATUS.AddInputParameter("INACTIVE", " ");//Can be hard coded, BUT needs to be a space " "
            BAPI_ALM_ORDER_MAINTAIN_IT_USERSTATUS.AddInputParameter("CHANGE_EVENT", "01");//can be hard coded

            SAPProcess p5 = new SAPProcess();
            p5.ProcessSAPFunction(BAPI_GOODSMVT_CREATE_GOODSMVT_ITEM);
            #endregion*/

            #region BAPI_ALM_CONF_CREATE_TIMETICKETS

            /*SAPFunction BAPI_ALM_CONF_CREATE_TIMETICKETS = new SAPFunction();
            BAPI_ALM_CONF_CREATE_TIMETICKETS.setName("BAPI_ALM_CONF_CREATE");
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddInputParameter("OPERATION", "TVLTEC");//Not Hardcoded (Example: "TVLTEC") -> String
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddInputParameter("ORDERID", "10909960");//Not Hardcoded (Example: "10909960") -> String
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddInputParameter("ACT_WORK", "12");//Not Hardcoded (Example: "12") -> BigDecimal
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddInputParameter("UN_WORK", "10909960");//Not Hardcoded (Example: "10909960")
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddInputParameter("FIN_CONF", " ");//Can be Hardcoded (Value: " ") -> String
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddInputParameter("CONF_TEXT", "I broke the machine again, blah");//Not Hardcoded (Example: "I broke the machine again, blah") - > String
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddInputParameter("EXEC_START_TIME", "10909960");//Not Used (Example: "10909960") -> Timestamp
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddInputParameter("EXEC_START_DATE", "10909960");//Not Used (Example: "10909960") -> Timestamp
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddInputParameter("EXEC_FIN_TIME", "10909960");//Not Used (Example: "10909960") -> Timestamp
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddInputParameter("EXEC_FIN_DATE", "10909960");//Not Used (Example: "10909960") -> Timestamp
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddInputParameter("ACT_TYPE", "TVLTEC");//Not Hardcoded (Example: "TVLTEC" / "DST-KM" / "LABTEC") -> String
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddOutputParameter("RETURN:MESSAGE");
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddOutputParameter("RETURN:LOG_NO");
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddOutputParameter("RETURN:LOG_MSG_NO");
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddOutputParameter("RETURN:MESSAGE_V1");
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddOutputParameter("RETURN:MESSAGE_V2");
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddOutputParameter("RETURN:MESSAGE_V3");
            BAPI_ALM_CONF_CREATE_TIMETICKETS.AddOutputParameter("RETURN:MESSAGE_V4");
            SAPProcessRemoteFunction p6 = new SAPProcessRemoteFunction();
            p6.ProcessSAPFunction(BAPI_ALM_CONF_CREATE_TIMETICKETS);*/


            SAPFunction BAPI_ALM_CONF_CREATE_TIMETICKETS_V2 = new SAPFunction();
            
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.setName("BAPI_ALM_CONF_CREATE");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddInputParameter("TIMETICKETS:OPERATION", "0010");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddInputParameter("TIMETICKETS:ORDERID", "10909960");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddInputParameter("TIMETICKETS:ACT_WORK", "0012");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddInputParameter("TIMETICKETS:ACT_WORK_2", "00012");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddInputParameter("TIMETICKETS:FIN_CONF", " ");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddInputParameter("TIMETICKETS:CONF_TEXT", "I broke the machine again, blah");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddInputParameter("TIMETICKETS:EXEC_START_DATE", "0000-00-00");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddInputParameter("TIMETICKETS:EXEC_START_TIME", "00:00:00");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddInputParameter("TIMETICKETS:EXEC_FIN_DATE", "0000-00-00");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddInputParameter("TIMETICKETS:EXEC_FIN_TIME", "00:00:00");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("RETURN:MESSAGE");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("RETURN:LOG_NO");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("RETURN:LOG_MSG_NO");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("RETURN:MESSAGE_V1");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("RETURN:MESSAGE_V2");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("RETURN:MESSAGE_V3");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("RETURN:MESSAGE_V4");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("DETAIL_RETURN:TYPE");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("DETAIL_RETURN:MESSAGE_ID");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("DETAIL_RETURN:MESSAGE_NUMBER");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("DETAIL_RETURN:MESSAGE");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("DETAIL_RETURN:LOG_NUMBER");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("DETAIL_RETURN:LOG_MSG_NO");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("DETAIL_RETURN:MESSAGE_V1");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("DETAIL_RETURN:MESSAGE_V2");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("DETAIL_RETURN:MESSAGE_V3");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("DETAIL_RETURN:MESSAGE_V4");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("DETAIL_RETURN:PARAMETER");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("DETAIL_RETURN:ROW");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("DETAIL_RETURN:FIELD");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("DETAIL_RETURN:SYSTEM");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("DETAIL_RETURN:FLG_LOCKED");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("DETAIL_RETURN:CONF_NO");
            BAPI_ALM_CONF_CREATE_TIMETICKETS_V2.AddOutputParameter("DETAIL_RETURN:CONF_CNT");
            
            SAPProcessRemoteFunction p7 = new SAPProcessRemoteFunction();
            p7.ProcessSAPFunction(BAPI_ALM_CONF_CREATE_TIMETICKETS_V2);



            #endregion

            Console.WriteLine("Press any key to finish.");
            Console.ReadLine();
            System.Environment.Exit(0);
        }
    }
}

