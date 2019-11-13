using log4net;
using log4net.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ConnectorLib.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConnectorLib.Processing.Actions;
using ConnectorEntity;
using WebClientUtility.Core;
using WebClientUtility.API;
using ConnectorModel.Model;
using EBS;
using ODBCConnector.PopulateModul;

namespace ConnectorUI
{
    public class Connector
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Connector));
        public static frmMain mainForm;
        public static async Task<LogRecord> start(ConnectorConfiguration configuration)
        {
            log4net.Config.XmlConfigurator.Configure();
            Log.Info("*************************************************Sync Started*******************************************************************");
            LogRecord jdeConnectorLog = new LogRecord();
            try
            {
                Config config = new Config();
                Log.Info("Config Request call");
                WebClientUtility.API.JDEConnectorApi jdeAPIConnectorApi = new WebClientUtility.API.JDEConnectorApi(new WebClientHttpUtility(), config, configuration.connectorKey);
                config = jdeAPIConnectorApi.GetConnectorConfig();

                try
                {
                    jdeConnectorLog.Date = DateTime.Now;
                    SetProgress(20, "Sync started retrieving connector config ...");
                    Log.Info("Retrieved Connector Config ...");
                    foreach (QueryRequest queryRequest in config.autoScheduleRequest)
                    {
                        await JDERequest(configuration, queryRequest, jdeAPIConnectorApi);
                    }
                    jdeConnectorLog.Status = "Success";
                    SetProgress(100, string.Empty);
                }
                catch (Exception ex)
                {
                    SetProgress(0, string.Empty);
                    Log.FatalFormat("Request failed {0}. Trace: {1}", ex.Message, ex);
                }

                Log.Info("*********************************************************End************************************************************************");
                return jdeConnectorLog;
            }
            catch (Exception ex)
            {
                Log.FatalFormat("{0} sync failed:  Trace: {1}", ex.Message, ex);
                jdeConnectorLog.Status = "Failed";
                jdeConnectorLog.Message = string.Format("Sync failed: {0}. Trace: {1}", ex.Message, ex);
                jdeConnectorLog.Date = DateTime.Now;

            }
            return jdeConnectorLog;
        }
        private static async Task JDERequest(ConnectorConfiguration configuration, QueryRequest queryRequest, WebClientUtility.API.JDEConnectorApi ConnectorApi)
        {
            try
            {

                Log.Info(string.Format("*************************************************Sync {0} Started*******************************************************************", queryRequest.Module));
                SetProgress(60, string.Format("Fetching {0} data from JDE database...", queryRequest.Module));
                List<string> list = await GetApiResult(queryRequest, configuration.connectionString);

                if (list.Count == 0)
                {

                    Log.Info("No Data Found................");
                }

                for (var i = 0; i < list.Count; i++)
                {
                    Log.Info(string.Format("Sync data {0}", list[i]));
                    Log.FatalFormat(string.Format("Posting {0} data on xpc server...", queryRequest.Module));
                    SetProgress((i / list.Count) * 100, string.Format("Posting {0} data on xpc server...", queryRequest.Module));
                    var response = ConnectorApi.PostResponse(queryRequest.Module, list[i]);
                    var statusResult = (JObject)JsonConvert.DeserializeObject(response);
                    SetProgress((i / list.Count) * 100, string.Format("Request Id {0} ", i + 1));
                    Log.FatalFormat(string.Format("Request Success Id {0} ", i + 1));
                }

                Log.Info(string.Format("*************************************************Sync {0} End *******************************************************************", queryRequest.Module));

            }
            catch (Exception ex)
            {
                SetProgress(0, string.Empty);
                Log.FatalFormat("Request failed {0}. Trace: {1}", ex.Message, ex);
            }
        }
        private async static Task<List<string>> GetApiResult(QueryRequest queryRequest, string connectionString)
        {

            queryRequest.ChunkSize = queryRequest.ChunkSize > 0 ? queryRequest.ChunkSize : 100;

            string LastSync = string.Empty;
            if (queryRequest.PoolingColumnType == "DateTime")
            {
                LastSync = Convert.ToDateTime(queryRequest.LastSync).ToString("yyyy-MM-dd HH:mm:ss");
            }
            Log.Info(string.Format("LastSync '{0}'  ", LastSync));
            string commandText = queryRequest.SQLquery.Replace("[[LastSync]]", LastSync);
            Log.FatalFormat("Request mudule {0}. Query: {1}", queryRequest.Module, commandText);
            List<string> result = new List<string>();
            switch (queryRequest.Module)
            {

                case "Location":
                case "Term":
                case "Classification":
                case "Department":
                case "PaymentMethod":
                case "Items":
                case "GLAccount":
                case "Vendor":
                    {
                        result = await ODBCDataAccess.GetResult(connectionString, commandText, queryRequest.ChunkSize);
                    }
                    break;
                case "PurchaseOrder":
                    {

                        var poData = await ODBCDataAccess.GetResult(connectionString, commandText);
                        List<GetPurchaseOrderEntity> objectListPO = Deserialize<GetPurchaseOrderEntity>(poData);
                        List<PurchaseOrder> list = PopulateModule.PopulatePoModel(objectListPO);
                        var serializeData = JsonConvert.SerializeObject(list);
                        Log.Info(string.Format("**************************PO Formated Data. {0} Total {1}", serializeData, list.Count));
                        var chunks = Split<PurchaseOrder>(list, queryRequest.ChunkSize);
                        foreach (var item in chunks)
                        {
                         result.Add(JsonConvert.SerializeObject(item));
                        }

                    }
                    break;
                case "StandardInvoice":
                    {
                        var StandardInvoiceData = await ODBCDataAccess.GetResult(connectionString, commandText);
                        List<GetStandardInvoiceEntity> objectListInvoice = Deserialize<GetStandardInvoiceEntity>(StandardInvoiceData);
                        List<StandardInvoice> list = PopulateModule.PopulateInvoiceModel(objectListInvoice);
                        var serializeData = JsonConvert.SerializeObject(list);
                        Log.Info(string.Format("**************************StandardInvoice Formated Data. {0} Total {1}", serializeData, list.Count));
                        var chunks = Split<StandardInvoice>(list, queryRequest.ChunkSize);
                        foreach (var item in chunks)
                        {
                            result.Add(JsonConvert.SerializeObject(item));
                        }


                    }
                    break;
                case "TwowayInvoice":
                    {
                        var TwowayInvoiceData = await ODBCDataAccess.GetResult(connectionString, commandText);
                        List<GetTwowayInvoiceEntity> objectListInvoice = Deserialize<GetTwowayInvoiceEntity>(TwowayInvoiceData);
                        List<TwowayInvoice> list = PopulateModule.PopulateTwowayInvoiceModel(objectListInvoice);
                        var serializeData = JsonConvert.SerializeObject(list);
                        Log.Info(string.Format("**************************Twoway Invoice Formated Data. {0} Total {1}", serializeData, list.Count));
                        var chunks = Split<TwowayInvoice>(list, queryRequest.ChunkSize);
                        foreach (var item in chunks)
                        {
                            result.Add(JsonConvert.SerializeObject(item));
                        }


                    }
                    break;
                case "Payment":
                    {
                        var PaymentData = await ODBCDataAccess.GetResult(connectionString, commandText);
                        List<GetPaymentsEntity> objectListPayment = Deserialize<GetPaymentsEntity>(PaymentData);
                        List<Payments> list = PopulateModule.PopulatePaymentModel(objectListPayment);
                        var serializeData = JsonConvert.SerializeObject(list);
                        Log.Info(string.Format("**************************Payment Formated Data. {0} Total {1}", serializeData, list.Count));
                        var chunks = Split<Payments>(list, queryRequest.ChunkSize);
                        foreach (var item in chunks)
                        {
                            result.Add(JsonConvert.SerializeObject(item));
                        }

                    }                 
                    break;
                default:
                    {

                    }
                    break;
            }
            return result;
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Info("********************************************************************************************************************");
            Log.FatalFormat("* UnhandledException: ExceptionObject = {0}, IsTerminating = {1}", e.ExceptionObject, e.IsTerminating);
            Log.Info("********************************************************************************************************************");
        }
        protected void OnStop()
        {
            Log.Info("********************************************************************************************************************");
            Log.Info("* Sage50Connector Service stopped");
            Log.Info("********************************************************************************************************************");
        }
        public static void SetProgress(int value, string message)
        {
            mainForm.applicationProgress.Invoke((Action)(() => mainForm.applicationProgress.Value = value));
            mainForm.lblApplicationProgress.Invoke((Action)(() => mainForm.lblApplicationProgress.Text = value.ToString() + "%"));
            mainForm.lblProgressLog.Invoke((Action)(() => mainForm.lblProgressLog.Text = string.Format("{0}", value == 0 ? "" : message)));
        }
        public static List<T> Deserialize<T>(string result)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //public static List<List<T>> Split<T>(List<T> collection, int size)
        //{
        //    var chunks = new List<List<T>>();
        //    var chunkCount = collection.Count / size;

        //    if (collection.Count % size > 0)
        //        chunkCount++;

        //    for (var i = 0; i < chunkCount; i++)
        //        chunks.Add(collection.Skip(i * size).Take(size).ToList());

        //    return chunks;
        //}
        public static List<List<T>> Split<T>(List<T> collection, int size)
        {
            return collection.Select((x, i) => new { Index = i, Value = x })
                 .GroupBy(x => x.Index / size)
                 .Select(x => x.Select(v => v.Value).ToList())
                 .ToList();
        }
    }

}
