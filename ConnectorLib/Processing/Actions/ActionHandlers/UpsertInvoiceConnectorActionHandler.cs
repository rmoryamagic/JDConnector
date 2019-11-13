using log4net;
using ConnectorLib.API;
using ConnectorLib.Processing.Actions.ConnectorActions;

namespace ConnectorLib.Processing.Actions.ActionHandlers
{
    public class UpsertInvoiceConnectorActionHandler : IConnectorActionHandler<UpsertInvoiceConnectorAction>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UpsertInvoiceConnectorActionHandler));

        public void Handle(UpsertInvoiceConnectorAction action)
        {
            //using (var api = new ConnectorApi(action.source))
            //{
            //    Log.InfoFormat("Open Sage50 company: \"{0}\"", action.companyName);
            //    api.OpenCompany(action.companyName);

            //    Log.Info("Upsert Invoice Data to Sage50 ...");
            //    api.UpsertInvoice(action.payload.invoice);
            //    Log.Info($"Successfully upserted invoice: {action.payload.invoice.ReferenceNumber}");
            //}
        }
    }
}
