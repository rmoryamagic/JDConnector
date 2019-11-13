using ConnectorModel;
using ConnectorModel.Model;
using EBS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;


namespace ConnectorLib.API
{
    /// <summary>
    /// ConnectorApi
    /// </summary>
    public class ConnectorApi
    {
        private readonly string actionSource;
        public ConnectorApi()
        {

        }
        public ConnectorApi(string actionSource)
        {
            this.actionSource = actionSource;
        }
        //public async void UpsertVendor(Vendor customer)
        //{
        //    // TODO

        //}
        //public async void UpsertInvoice(Invoice invoice)
        //{
        //    //TODO
        //}
        //public async void UpsertPurchaseOrder(PurchaseOrder po)
        //{
        //    //TODO

        //}
    }
}
