using log4net;
using ConnectorLib.API;
using ConnectorLib.Processing.Actions.ConnectorActions;

namespace ConnectorLib.Processing.Actions.ActionHandlers
{
    class UpsertPaymentConnectorActionHandler : IConnectorActionHandler<UpsertPaymentConnectorAction>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UpsertPaymentConnectorActionHandler));

        public void Handle(UpsertPaymentConnectorAction action)
        {
            //using (var api = new ConnectorApi(action.source))
            //{
            //    Log.InfoFormat("Open Sage50 company: \"{0}\"", action.companyName);
            //    api.OpenCompany(action.companyName);
            //    Log.Info("Create Payment Data to Sage50 ...");
            //    api.CreatePayment(action.payload);
            //    Log.Info($"Successfully created payment for invoice: {action.payload.invoice.ReferenceNumber}");
            //}
        }
    }
}
    