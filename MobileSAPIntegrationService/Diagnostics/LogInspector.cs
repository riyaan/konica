using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;

namespace Diagnostics
{
    public class LogInspector : IClientMessageInspector, IDispatchMessageInspector
    {

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            Logger.Instance.Log.Trace("After receive reply");
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            Logger.Instance.Log.Trace("BeforeSendRequest Start");

            MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
            Message message = buffer.CreateMessage();
            ////XmlTextWriter xtw = new XmlTextWriter(Console.Out);
            //XmlTextWriter xtw = new XmlTextWriter(@"C:\Work\3Fifteen\Konica\MobileSAPIntegrationService\Logs\log.log", null);
            //xtw.Formatting = Formatting.Indented;
            //message.WriteMessage(xtw);
            //xtw.Flush();
            //xtw.Close();

            Logger.Instance.Log.Debug<Message>(message);

            request = buffer.CreateMessage();

            Logger.Instance.Log.Trace("BeforeSendRequest End");

            return null;
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            Logger.Instance.Log.Trace("After receive request Start");

            MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
            Message message = buffer.CreateMessage();            

            StringBuilder sb = new StringBuilder();
            using (System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(sb))
            {
                message.WriteMessage(xw);
                xw.Close();
            }
            Logger.Instance.Log.Trace("Message Received:\n{0}", sb.ToString());

            request = buffer.CreateMessage();

            Logger.Instance.Log.Trace("After receive request End");

            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            Logger.Instance.Log.Trace("Before send reply");
        }
    }
}
