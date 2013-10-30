using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAP.Middleware.Connector;
using Common.Configuration;

namespace KonicaMinolta.SAP.Integration
{
    public class SAPSystemConnect : IDestinationConfiguration
    {
        public RfcConfigParameters GetParameters(String destinationName) {

            Manager manager = new Manager();
            SapConnection sapConnection = manager.GetSapConfigurationInformation();            

            RfcConfigParameters parms = new RfcConfigParameters();
            parms.Add(RfcConfigParameters.AppServerHost, sapConnection.Host);
            parms.Add(RfcConfigParameters.SystemNumber, sapConnection.SystemNumber);
            parms.Add(RfcConfigParameters.User, sapConnection.User);
            parms.Add(RfcConfigParameters.Password, sapConnection.Password);
            parms.Add(RfcConfigParameters.Client, sapConnection.Client);
            parms.Add(RfcConfigParameters.Language, sapConnection.Language);
            parms.Add(RfcConfigParameters.PoolSize, sapConnection.PoolSize);
            parms.Add(RfcConfigParameters.MaxPoolSize, sapConnection.MaxPoolSize);
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
