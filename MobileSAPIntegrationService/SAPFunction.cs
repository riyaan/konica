using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using SAP.Middleware.Connector;
using System.Data;

namespace KonicaMinolta.SAP.Integration
{
    class SAPFunction
    {
        private String name;
        public static String STRUCTURE = "STRUCTURE";
        public static String TABLE = "TABLE";

        public DataSet Results
        {
            get;
            set;
        }
        public IDictionary<String, String> InputParameters
        {
            get;
            set;
        }
        public List<String> OutputParameters
        {
            get;
            set;
        }
        public IDictionary<String, Int32> Length
        {
            get;
            set;
        }
        public IDictionary<String, Type> DataType
        {
            get;set;
        }

        public SAPFunction()
        {
            InputParameters = new Dictionary<String, String>();
            OutputParameters = new List<String>();
            Length = new Dictionary<String, int>();
            DataType = new Dictionary<String, Type>();
            Results = new DataSet();
        }

        public String getName()
        {
            return this.name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public void AddInputParameter(String key, String value) {
            InputParameters.Add(new KeyValuePair<string, string>(key, value));
        }

        public void AddOutputParameter(String key)
        {
            OutputParameters.Add(key);
        }
    }
}
