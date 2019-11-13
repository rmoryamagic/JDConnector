using System;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConnectorLib.Processing.Actions.ConnectorActions.Factory
{
    /// <summary>
    /// Creates Sage Actions from JSON strings
    /// </summary>
    public class ActionFromJsonFactory : IActionFactory
    {
        public static readonly ILog Log = LogManager.GetLogger(typeof(ActionFromJsonFactory));

        public ConnectorAction Create(string jsonString)
        {
            Log.DebugFormat("Creating Sage50 Action from JSON: {0}", jsonString);

            dynamic connectorAction = JObject.Parse(jsonString);

            var actionTypePrefix = (string)connectorAction.type;
            var actionType = ConnectorAction.GetActionClassType(actionTypePrefix);
            if (actionType == null)
            {
                throw new Exception($"Can not found action type for '{actionTypePrefix}' type");
            }

            return (ConnectorAction)JsonConvert.DeserializeObject(jsonString, actionType);
        }

    }
}