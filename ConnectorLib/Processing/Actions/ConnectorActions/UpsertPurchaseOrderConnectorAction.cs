
using ConnectorModel.Payloads;

// ReSharper disable InconsistentNaming

namespace ConnectorLib.Processing.Actions.ConnectorActions
{
    public class UpsertPurchaseOrderConnectorAction : ConnectorAction
  {
        public PurchaseOrderPayload payload { get; set; }
    }
}