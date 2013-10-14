using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAP.Middleware.Connector;

namespace KonicaMinolta.SAP.Integration
{
    public class SAPSystemConnect : IDestinationConfiguration
    {

        // TODO: Read these values from the config file
        /// <summary>
        /// 
        /// </summary>
        /// <param name="destinationName"></param>
        /// <returns></returns>
        public RfcConfigParameters GetParameters(String destinationName) {

            // 172.100.8.80
            String Host = "172.100.8.33";
            String SystemNumber = "00";
            String User = "Mobile";
            String Password = "mobile123";
            String Client = "100";
            String Language = "EN";

            RfcConfigParameters parms = new RfcConfigParameters();
            parms.Add(RfcConfigParameters.AppServerHost, Host);
            parms.Add(RfcConfigParameters.SystemNumber, SystemNumber);
            parms.Add(RfcConfigParameters.User, User);
            parms.Add(RfcConfigParameters.Password, Password);
            parms.Add(RfcConfigParameters.Client, Client);
            parms.Add(RfcConfigParameters.Language, Language);
            parms.Add(RfcConfigParameters.PoolSize, "125");
            parms.Add(RfcConfigParameters.MaxPoolSize, "250");
            parms.Add(RfcConfigParameters.Name, destinationName);

            return parms;
        }

        public bool ChangeEventsSupported()
        {
            return false;
        }

        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

    }
}
