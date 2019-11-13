using System;
using System.Collections.Generic;
using System.Configuration;


namespace WebClientUtility.Core
{
    /// Serivce Configuration Wrapper
    static partial class ApplicationConfig
    {
        

        /// <summary>
        /// Sage50Connector REST Api Base Url
        /// </summary>
        public static Uri JDEConnectorBaseUri => new Uri(ConfigurationManager.AppSettings["JDEConnectorBaseUrl"]);

        /// <summary>
        /// Customers LastSavedAt value filter
        /// </summary>
        public static DateTime CustomersLastSavedAt
        {
            get => GetAppSettingsValue<DateTime>(CustomersLastSavedAtKey);
            set => SetAppSettingsProperty(CustomersLastSavedAtKey, TypeUtil.DateToUTC(value));
        }

        private const string CustomersLastSavedAtKey = "Customers_LastSavedAt";
    }
}
