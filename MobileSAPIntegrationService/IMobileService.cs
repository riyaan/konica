using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

namespace KonicaMinolta.SAP.Integration.Service
{
    [ServiceContract]
    public interface IMobileService
    {

        [OperationContract]
        bool SavePDF(string docbinaryarray, string docname);

        [OperationContract]
        byte[] GetPDF(string DocumentName);

        [OperationContract]
        DataSet doZaBAPI(String wsOrderNumber, String wsBWClose, String wsBWOpen, String wsColClose, String wsColOpen, String wsLoginUser, String wsPDFDirectory, String wsCreatedDate, List<GOODSMVT_ITEM> wsGoodsMvtItem, List<TIMETICKETS> wsTimeTickets);
    }
}
