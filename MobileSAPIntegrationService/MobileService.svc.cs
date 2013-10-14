using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.IO;

namespace KonicaMinolta.SAP.Integration.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class MobileService : IMobileService
    {

        public bool SavePDF(string docbinaryarray, string docname)
        {
            byte[] buffer = Convert.FromBase64String(docbinaryarray);
            FileStream fileStream = new FileStream(System.Configuration.ConfigurationManager.AppSettings["PDFSaveLocation"] + "\\" + docname, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(buffer, 0, buffer.Length);
            fileStream.Close();
            return true;
        }

        public byte[] GetPDF(string DocumentName)
        {
            FileStream fileStream = new FileStream(System.Configuration.ConfigurationManager.AppSettings["PDFSaveLocation"] + "\\" + DocumentName, FileMode.Open, FileAccess.Read);
            int count = (int)fileStream.Length;
            byte[] buffer = new byte[count];
            fileStream.Read(buffer, 0, count);
            fileStream.Close();
            return buffer;
        }

        public DataSet doZaBAPI(String wsOrderNumber,  
                             String wsBWClose, 
                             String wsBWOpen, 
                             String wsColClose, 
                             String wsColOpen, 
                             String wsLoginUser, 
                             String wsPDFDirectory,
                             String wsCreatedDate,
                             List<GOODSMVT_ITEM> wsGoodsMvtItem,
                             List<TIMETICKETS> wsTimeTickets)
        {
            SAPFunction ZBAPI = new SAPFunction();
            ZBAPI.setName("ZMOBILE_MASS_UPDATE_BAPI");

             //MEASUREMENTS
             ZBAPI.AddInputParameter("AUFNR", wsOrderNumber);  
             ZBAPI.AddInputParameter("BW_CLOSER", wsBWClose); 
             ZBAPI.AddInputParameter("BW_OPENR", wsBWOpen); 
             ZBAPI.AddInputParameter("COL_CLOSER", wsColClose); 
             ZBAPI.AddInputParameter("COL_OPENR", wsColOpen);

             ZBAPI.AddInputParameter("GOODSMVT_HEADER:PSTNG_DATE", wsCreatedDate); 
            
            //GOODS MOVEMENT CODE
             ZBAPI.AddInputParameter("GOODSMVT_CODE:GM_CODE", "03"); 
       
             //GOODS MOVEMENT ITEM - MULTIPLE (TABLE)
             for (int x = 0; x < wsGoodsMvtItem.Count; x++)
             {
                 ZBAPI.AddInputParameter("GOODSMVT_ITEM[" + x + "]:ORDERID", wsOrderNumber);
                 ZBAPI.AddInputParameter("GOODSMVT_ITEM[" + x + "]:MATERIAL", wsGoodsMvtItem[x].MATERIAL);
                 ZBAPI.AddInputParameter("GOODSMVT_ITEM[" + x + "]:PLANT", wsGoodsMvtItem[x].PLANT);
                 ZBAPI.AddInputParameter("GOODSMVT_ITEM[" + x + "]:STGE_LOC", wsGoodsMvtItem[x].STGE_LOC);
                 ZBAPI.AddInputParameter("GOODSMVT_ITEM[" + x + "]:ENTRY_UOM", "EA");
                 ZBAPI.AddInputParameter("GOODSMVT_ITEM[" + x + "]:MOVE_TYPE", "261");
                 ZBAPI.AddInputParameter("GOODSMVT_ITEM[" + x + "]:ENTRY_QNT", wsGoodsMvtItem[x].ENTRY_QNT);
             }

             //TIMETICKETS - MULTIPLE (TABLE)
             for (int x = 0; x < wsTimeTickets.Count; x++)
             {
                 ZBAPI.AddInputParameter("TIMETICKETS[" + x + "]:OPERATION", wsTimeTickets[x].OPERATION);
                 ZBAPI.AddInputParameter("TIMETICKETS[" + x + "]:ORDERID", wsOrderNumber);
                 ZBAPI.AddInputParameter("TIMETICKETS[" + x + "]:ACT_WORK", wsTimeTickets[x].ACT_WORK);
                 ZBAPI.AddInputParameter("TIMETICKETS[" + x + "]:ACT_WORK_2", wsTimeTickets[x].ACT_WORK_2);
                 ZBAPI.AddInputParameter("TIMETICKETS[" + x + "]:FIN_CONF", " ");
                 ZBAPI.AddInputParameter("TIMETICKETS[" + x + "]:CONF_TEXT", wsTimeTickets[x].CONF_TEXT);
                 ZBAPI.AddInputParameter("TIMETICKETS[" + x + "]:EXEC_START_DATE", wsTimeTickets[x].EXEC_START_DATE);
                 ZBAPI.AddInputParameter("TIMETICKETS[" + x + "]:EXEC_START_TIME", wsTimeTickets[x].EXEC_START_TIME);
                 ZBAPI.AddInputParameter("TIMETICKETS[" + x + "]:EXEC_FIN_DATE", wsTimeTickets[x].EXEC_FIN_DATE);
                 ZBAPI.AddInputParameter("TIMETICKETS[" + x + "]:EXEC_FIN_TIME", wsTimeTickets[x].EXEC_FIN_TIME);
                 ZBAPI.AddInputParameter("TIMETICKETS[" + x + "]:EX_CREATED_BY", "Mobile");
                 ZBAPI.AddInputParameter("TIMETICKETS[" + x + "]:EX_CREATED_DATE", wsTimeTickets[x].EX_CREATED_DATE);
                 ZBAPI.AddInputParameter("TIMETICKETS[" + x + "]:EX_CREATED_TIME", wsTimeTickets[x].EX_CREATED_TIME);
             }

             //PDF DATA (STRUCTURE)
             ZBAPI.AddInputParameter("DOCUMENTDATA:DOCUMENTNUMBER",  wsOrderNumber);
             ZBAPI.AddInputParameter("DOCUMENTDATA:DOCUMENTVERSION", "00"); 
             ZBAPI.AddInputParameter("DOCUMENTDATA:WSAPPLICATION1",  "PDF"); 
             ZBAPI.AddInputParameter("DOCUMENTDATA:DESCRIPTION", wsLoginUser); 
             ZBAPI.AddInputParameter("DOCUMENTDATA:DOCUMENTPART", "001"); 
             ZBAPI.AddInputParameter("DOCUMENTDATA:STATUSEXTERN", "RE"); 
             ZBAPI.AddInputParameter("DOCUMENTDATA:DOCFILE1", wsPDFDirectory); 

             //PDF FILES (TABLE)
             ZBAPI.AddInputParameter("DOCUMENTFILES:SOURCEDATACARRIER", "SAP-SYSTEM"); 
             ZBAPI.AddInputParameter("DOCUMENTFILES:STORAGECATEGORY", "SAP-SYSTEM"); 
             ZBAPI.AddInputParameter("DOCUMENTFILES:ORIGINALTYPE", "1"); 
             ZBAPI.AddInputParameter("DOCUMENTFILES:DOCUMENTNUMBER", wsOrderNumber);
             ZBAPI.AddInputParameter("DOCUMENTFILES:WSAPPLICATION", "PDF"); 
             ZBAPI.AddInputParameter("DOCUMENTFILES:DOCFILE", wsPDFDirectory); 

            ZBAPI.AddOutputParameter("RETURN:MESSAGE");
            ZBAPI.AddOutputParameter("RETURN:LOG_NO");
            ZBAPI.AddOutputParameter("RETURN:LOG_MSG_NO");
            ZBAPI.AddOutputParameter("RETURN:MESSAGE_V1");
            ZBAPI.AddOutputParameter("RETURN:MESSAGE_V2");
            ZBAPI.AddOutputParameter("RETURN:MESSAGE_V3");
            ZBAPI.AddOutputParameter("RETURN:MESSAGE_V4");
            ZBAPI.AddOutputParameter("DETAIL_RETURN:TYPE");
            ZBAPI.AddOutputParameter("DETAIL_RETURN:MESSAGE_ID");
            ZBAPI.AddOutputParameter("DETAIL_RETURN:MESSAGE_NUMBER");
            ZBAPI.AddOutputParameter("DETAIL_RETURN:MESSAGE");
            ZBAPI.AddOutputParameter("DETAIL_RETURN:LOG_NUMBER");
            ZBAPI.AddOutputParameter("DETAIL_RETURN:LOG_MSG_NO");
            ZBAPI.AddOutputParameter("DETAIL_RETURN:MESSAGE_V1");
            ZBAPI.AddOutputParameter("DETAIL_RETURN:MESSAGE_V2");
            ZBAPI.AddOutputParameter("DETAIL_RETURN:MESSAGE_V3");
            ZBAPI.AddOutputParameter("DETAIL_RETURN:MESSAGE_V4");
            ZBAPI.AddOutputParameter("DETAIL_RETURN:PARAMETER");
            ZBAPI.AddOutputParameter("DETAIL_RETURN:ROW");
            ZBAPI.AddOutputParameter("DETAIL_RETURN:FIELD");
            ZBAPI.AddOutputParameter("DETAIL_RETURN:SYSTEM");
            ZBAPI.AddOutputParameter("DETAIL_RETURN:FLG_LOCKED");
            ZBAPI.AddOutputParameter("DETAIL_RETURN:CONF_NO");
            ZBAPI.AddOutputParameter("DETAIL_RETURN:CONF_CNT");

            SAPProcessRemoteFunction p8 = new SAPProcessRemoteFunction();
            p8.ProcessSAPFunction(ZBAPI);

            return ZBAPI.Results;
        }
    }
}
