using log4net;
using ConnectorLib.API;
using ConnectorLib.Processing.Actions.ConnectorActions;

namespace ConnectorLib.Processing.Actions.ActionHandlers
{
    class UpsertVendorConnectorActionHandler : IConnectorActionHandler<UpsertVendorConnectorAction>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UpsertVendorConnectorActionHandler));
        public void Handle(UpsertVendorConnectorAction action)
        {
            try
            {
                //using (var api = new ConnectorApi(action.source))
                //{
                //    Log.InfoFormat("Open Sage50 company: \"{0}\"", action.companyName);
                //    api.OpenCompany(action.companyName);
                //    Log.Info("Create Vendor Data to Sage50 ...");
                //    api.CreateVendor(action.payload.vendor);
                //    Log.Info($"Successfully created Vendor: {action.payload.vendor.Name}");

                //}
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
    }
}
