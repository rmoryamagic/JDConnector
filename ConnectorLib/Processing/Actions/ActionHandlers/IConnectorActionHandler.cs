using System;
using ConnectorLib.Processing.Actions.ConnectorActions;

namespace ConnectorLib.Processing.Actions.ActionHandlers
{
    /// <summary>
    /// Provides methods to handle (process) Sage Actions
    /// </summary>
    public interface IConnectorActionHandler<in TAction> where TAction: ConnectorAction
  {
        void Handle(TAction action);
    }
}