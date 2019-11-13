using log4net;
using ConnectorLib.API;
using ConnectorLib.Processing.Actions.ConnectorActions;

namespace ConnectorLib.Processing.Actions.ActionHandlers
{
    class UpsertPurchaseOrderConnectorActionHandler : IConnectorActionHandler<UpsertPurchaseOrderConnectorAction>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UpsertPurchaseOrderConnectorAction));

        public void Handle(UpsertPurchaseOrderConnectorAction action)
        {
            //using (var api = new ConnectorApi(action.source))
            //{
            //    Log.InfoFormat("Open Sage50 company: \"{0}\"", action.companyName);
            //    api.OpenCompany(action.companyName);
            //    Log.Info("Create Vendor Data to Sage50 ...");
            //    api.UpsertPurchaseOrder(action.payload.purchaseOrder);
            //    Log.Info($"Successfully Updated Purchase Order: {action.payload.purchaseOrder.PONumber}");

            //}
        }
    }
}
