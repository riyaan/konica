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

            LogMessage(message);

            request = buffer.CreateMessage();

            Logger.Instance.Log.Trace("BeforeSendRequest End");

            return null;
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            Logger.Instance.Log.Trace("After receive request Start");

            MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
            Message message = buffer.CreateMessage();

            LogMessage(message);

            request = buffer.CreateMessage();

            Logger.Instance.Log.Trace("After receive request End");

            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            Logger.Instance.Log.Trace("Before send reply");
        }

        private void LogMessage(Message message)
        {
            StringBuilder sb = new StringBuilder();
            using (System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(sb))
            {
                message.WriteMessage(xw);
                xw.Close();
            }
            Logger.Instance.Log.Trace("Message :\n{0}", sb.ToString());
        }
    }
}
