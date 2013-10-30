﻿using System.Configuration;

namespace Common.Configuration
{
    public class SapConnection
    {
        public string Host { get; set; }
        public string SystemNumber { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Client { get; set; }
        public string Language { get; set; }
        public string PoolSize { get; set; }
        public string MaxPoolSize { get; set; }
    }

    public class Manager
    {
        public SapConnection GetSapConfigurationInformation()
        {
            SapConnection sapConnection = new SapConnection();

            sapConnection.Host = ConfigurationManager.AppSettings["Host"];
            sapConnection.SystemNumber = ConfigurationManager.AppSettings["SystemNumber"];
            sapConnection.User = ConfigurationManager.AppSettings["User"];
            sapConnection.Password = ConfigurationManager.AppSettings["Password"];
            sapConnection.Client = ConfigurationManager.AppSettings["Client"];
            sapConnection.Language = ConfigurationManager.AppSettings["Language"];
            sapConnection.Language = ConfigurationManager.AppSettings["PoolSize"];
            sapConnection.Language = ConfigurationManager.AppSettings["MaxPoolSize"];

            return sapConnection;
        }
    }
}
