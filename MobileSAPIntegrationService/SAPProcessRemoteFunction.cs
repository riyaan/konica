using System;
using System.Collections.Generic;
using System.Linq;
using SAP.Middleware.Connector;
using System.Data;
using Diagnostics;
using Common.Configuration;


namespace KonicaMinolta.SAP.Integration
{ 
    /// <summary>
    /// Handles all communication with SAP
    /// </summary>
    class SAPProcessRemoteFunction
    {
        RfcDestination _desination = null;
        private RfcDestination Destination
        {
            get { return _desination; }
            set { _desination = value; }
        }

        /// <summary>
        /// Initialises connectivity with SAP
        /// </summary>
        public SAPProcessRemoteFunction()
        {
            SAPSystemConnect sapCfg = new SAPSystemConnect();

            //this.Destination = RfcDestinationManager.GetDestination(sapCfg.GetParameters("DEV"));
            Manager manager = new Common.Configuration.Manager();
            SapEnvironment sapEnvironment = manager.GetSapEnvironmentName();
            this.Destination = RfcDestinationManager.GetDestination(sapCfg.GetParameters(sapEnvironment.EnvironmentName));
        }

        /// <summary>
        /// Coordinates all requests and responses with SAP required to call a function, set parameters and retrieve the response
        /// </summary>
        /// <param name="function"></param>
        public void ProcessSAPFunction(SAPFunction function)
        {
            Logger.Instance.Log.Trace("ProcessSAPFunction method");

            //LogParameters(function.InputParameters, "Before preparing input.");

            RfcRepository repo = Destination.Repository;
            RfcFunctionMetadata meta = repo.GetFunctionMetadata(function.getName());
            IRfcFunction rfc = meta.CreateFunction();            

            PrepareInputParameters(function, meta, rfc);

            //LogParameters(function.InputParameters, "After input has been prepared.");

            PrepareOutputParameters(function, meta, rfc);
            InvokeSAP(function, rfc);
            ProcessResults(function, meta, rfc);
        }

        /// <summary>
        /// Logs the parameters passed on to the SAP function call.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="message">The message.</param>
        private void LogParameters(IDictionary<string, string> dictionary, string message)
        {
            //Logger.Instance.Log.Trace(message);

            //StringBuilder sb = new StringBuilder();
            //foreach (KeyValuePair<string, string> kvp in dictionary)
            //{
            //    sb.AppendLine(String.Format("Key: {0}", kvp.Key));
            //    sb.AppendLine(String.Format("Value: {0}", kvp.Value));
            //}

            //Logger.Instance.Log.Trace(sb.ToString());
            //sb.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="function"></param>
        private void LogException(Exception ex, SAPFunction function)
        {
            Logger.Instance.Log.LogException(NLog.LogLevel.Error, String.Format("Function Name: {0}", function.getName()), ex);
        }

        /// <summary>
        /// Call the SAP remote function
        /// </summary>
        /// <param name="function"></param>
        /// <param name="rfc"></param>
        private void InvokeSAP( SAPFunction function, IRfcFunction rfc)
        {
            try
            {
                DateTime start = DateTime.Now;
                rfc.Invoke(this.Destination);
                DateTime end = DateTime.Now;
                TimeSpan total = end.Subtract(start);
                double seconds = total.TotalSeconds;
                double milliseconds = total.TotalMilliseconds;
            }
            catch (Exception ex)
            {
                LogException(ex, function);
            }
        }

        /// <summary>
        /// Get teh response from SAP and dynamically generate DataTables based on the responses metadata
        /// </summary>
        /// <param name="function"></param>
        /// <param name="meta"></param>
        /// <param name="rfc"></param>
        private void ProcessResults(SAPFunction function, RfcFunctionMetadata meta, IRfcFunction rfc)
        {
            foreach (var output in function.OutputParameters)
            {
                String paramName = output;
                String secondLevelStructure = string.Empty;
                int index = -1;

                string[] paramDetail = paramName.Split(':');
                if (paramDetail.Length == 1)
                {
                    index = meta.TryNameToIndex(paramName);
                }
                else if (paramDetail.Length == 2)
                {
                    secondLevelStructure = paramDetail[0];
                    paramName = paramDetail[1];
                    index = meta.TryNameToIndex(secondLevelStructure);
                }

                if (index != -1)
                {
                    RfcElementMetadata elementMeta = rfc.GetElementMetadata(index);
                    RfcDataType type = elementMeta.DataType;
                    String dataType = type.ToString();
                    if (dataType.Equals(SAPFunction.STRUCTURE))
                    {
                        DataTable table = null;
                        if (function.Results.Tables[secondLevelStructure] == null)
                        {
                            table = function.Results.Tables.Add(secondLevelStructure);

                            IRfcStructure rfcStructure = rfc.GetStructure(secondLevelStructure);
                            RfcStructureMetadata structureMetaData = rfcStructure.Metadata;
                            for (int x = 0; x < rfcStructure.ElementCount; x++)
                            {
                                DataColumn dc = table.Columns.Add(structureMetaData[x].Name, typeof(String));
                                dc.DataType = ConvertDataType(type);
                            }

                            DataRow dr = table.NewRow();
                            foreach (var column in table.Columns)
                            {
                                dr[column.ToString()] = rfcStructure.GetValue(column.ToString());
                            }
                            table.Rows.Add(dr);
                        }
                    }
                    else if (dataType.Equals(SAPFunction.TABLE))
                    {
                        DataTable table = null;
                        if (function.Results.Tables[secondLevelStructure] == null)
                        {
                            table = function.Results.Tables.Add(secondLevelStructure);

                            IRfcTable rfcTable = rfc.GetTable(secondLevelStructure);
                            RfcTableMetadata tableMetaData = rfcTable.Metadata;
                            for (int x = 0; x < tableMetaData.LineType.FieldCount; x++)
                            {
                                DataColumn dc = table.Columns.Add(tableMetaData.LineType[x].Name, typeof(String));
                                dc.DataType = ConvertDataType( type );
                            }

                            foreach(var row in rfcTable)
                            {
                                DataRow dr = table.NewRow();
                                foreach(var column in table.Columns)
                                {
                                    dr[column.ToString()] = row.GetValue(column.ToString());
                                }
                                table.Rows.Add(dr);
                            }
                        }
                    }
                    else
                    {
                        DataTable table = null;
                        if (function.Results.Tables["Root"] == null)
                        {
                            table = function.Results.Tables.Add("Root");
                            function.Results.Tables["Root"].NewRow();
                        } 
                        else
                        {
                            table = function.Results.Tables[paramName];
                        }

                        table.Columns.Add(paramName);
                        table.Rows[0][paramName] = rfc;
                    }
                }
            }
        }

        /// <summary>
        /// convert SAP .net connector datatypes to c# datatypes
        /// </summary>
        /// <param name="rfcDataType"></param>
        /// <returns></returns>
        private Type ConvertDataType(RfcDataType rfcDataType)
        {
            Type systemType = null;
            try
            {
                switch (rfcDataType)
                {
                    case RfcDataType.NUM:
                        systemType = typeof(double);
                        break;
                    case RfcDataType.DATE:
                        systemType = typeof(DateTime);
                        break;
                    default:
                        systemType = typeof(String);
                        break;
                }
            }
            catch (Exception)
            {
                systemType = typeof(String);
            }
            return systemType;
        }

        /// <summary>
        /// Notify SAP which output parameters are required i.e. SAP will only respond with the data that has been requested
        /// </summary>
        /// <param name="function"></param>
        /// <param name="meta"></param>
        /// <param name="rfc"></param>
        private void PrepareOutputParameters(SAPFunction function, RfcFunctionMetadata meta, IRfcFunction rfc)
        {
            foreach (var iout in function.OutputParameters)
            {
                String paramName = iout;
                String secondLevelStructure = string.Empty;
                int index = -1;

                string[] paramDetail = paramName.Split(':');
                if (paramDetail.Length == 1)
                {
                    index = meta.TryNameToIndex(paramName);
                }
                else if (paramDetail.Length == 2)
                {
                    secondLevelStructure = paramDetail[0];
                    paramName = paramDetail[1];
                    index = meta.TryNameToIndex(secondLevelStructure);
                }

                if (index != -1)
                {
                    RfcElementMetadata elementMeta = rfc.GetElementMetadata(index);
                    RfcDataType type = elementMeta.DataType;
                    String dataType = type.ToString();
                    function.DataType.Add(paramName + ":" + secondLevelStructure, ConvertDataType(type));

                    IRfcDataContainer container = null;
                    if (dataType.Equals(SAPFunction.STRUCTURE))
                    {
                        RfcStructureMetadata structureMeta = elementMeta.ValueMetadataAsStructureMetadata;
                        container = rfc.GetStructure(secondLevelStructure);
                    }
                    else if (dataType.Equals(SAPFunction.TABLE))
                    {
                        RfcTableMetadata tableMeta = elementMeta.ValueMetadataAsTableMetadata;
                        container = rfc.GetTable(secondLevelStructure);

                        if (tableMeta.Name.Equals(String.Empty))
                        {
                            String lineType = tableMeta.LineType.ContainerType.ToString();
                            if (lineType.Equals("STRUCTURE"))
                            {
                                container = tableMeta.LineType.CreateStructure();
                            }
                        }
                    }
                    else
                    {
                        rfc.GetString(secondLevelStructure);
                    }
                }
            }
        }

        /// <summary>
        /// Pass input parameters to SAP remote function call
        /// </summary>
        /// <param name="function"></param>
        /// <param name="meta"></param>
        /// <param name="rfc"></param>
        private void PrepareInputParameters( SAPFunction function, RfcFunctionMetadata meta, IRfcFunction rfc)
        {
            Dictionary<String,IRfcTable> _table = new Dictionary<String,IRfcTable>();
            Dictionary<String,IRfcStructure> _structure = new Dictionary<String,IRfcStructure>();
            foreach (var iin in function.InputParameters)
            {
                String paramName = iin.Key;
                String paramValue = iin.Value;
                String secondLevelStructure = string.Empty;
                int sapFieldIndex = -1;
                int tableRowIndex = -1;

                string[] paramDetail = paramName.Split(':');
                if(paramDetail.Length == 1) {
                    sapFieldIndex = meta.TryNameToIndex(paramName);
                } else if (paramDetail.Length == 2) {
                    secondLevelStructure = paramDetail[0];
                    paramName = paramDetail[1];

                    if (secondLevelStructure.Contains('[') && secondLevelStructure.Contains(']'))
                    {
                        sapFieldIndex = meta.TryNameToIndex(secondLevelStructure.Split('[')[0]);
                        string arrayIndex = secondLevelStructure.Split('[')[1].Split(']')[0];
                        Int32.TryParse(arrayIndex, out tableRowIndex);
                    }
                    else
                    {
                        sapFieldIndex = meta.TryNameToIndex(secondLevelStructure);
                    }
                }

                if (sapFieldIndex != -1)
                {
                    RfcElementMetadata elementMeta = rfc.GetElementMetadata(sapFieldIndex);
                    RfcDataType type = elementMeta.DataType;
                    String dataType = type.ToString();
                    function.DataType.Add(secondLevelStructure + ":" + paramName, ConvertDataType(type));
                    if (dataType.Equals(SAPFunction.STRUCTURE))
                    {
                        IRfcStructure structure;
                        if (!_structure.TryGetValue(secondLevelStructure, out structure))
                        {
                            structure = rfc.GetStructure(secondLevelStructure);
                            _structure.Add(secondLevelStructure, structure);
                        }
                        structure.SetValue(paramName, paramValue);
                        function.Length.Add(paramName + ":" + secondLevelStructure, structure.GetElementMetadata(paramName).NucLength);
                    }
                    else if (dataType.Equals(SAPFunction.TABLE))
                    {
                        if (secondLevelStructure.Contains('[') && secondLevelStructure.Contains(']'))
                        {
                            IRfcTable table;
                            if (!_table.TryGetValue(secondLevelStructure.Split('[')[0], out table))
                            {
                                table = rfc.GetTable(secondLevelStructure.Split('[')[0]);
                                _table.Add(secondLevelStructure.Split('[')[0], table);
                            }

                            try
                            {
                                table.ElementAt(tableRowIndex);
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                table.Insert(1, tableRowIndex);
                            }
                            table.CurrentIndex = tableRowIndex;

                            //rfc.Invoke(this.Destination);

                            RfcElementMetadata em = table.GetElementMetadata(paramName);

                            string s = String.Format("{0, " + table.GetElementMetadata(paramName).NucLength + "}", paramValue);

                            table.SetValue(paramName, s.Trim());
                            function.Length.Add(paramName + ":" + secondLevelStructure, table.GetElementMetadata(paramName).NucLength);
                        }
                    }
                    else
                    {
                        rfc.SetValue(paramName, paramValue);
                        function.Length.Add(paramName, rfc.GetElementMetadata( paramName ).NucLength);
                    }
                }
            }
        }
    }
}
