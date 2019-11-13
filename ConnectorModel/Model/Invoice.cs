using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectorModel.Model
{

    public class Invoice
    {
        public int id { get; set; }
        public string termId { get; set; }
        public decimal currency { get; set; }
        public string classificationId { get; set; }
        public string departmentId { get; set; }
        public string locationId { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime transactionDate { get; set; }
        public string invoiceNumber { get; set; }
        public decimal amount { get; set; }
        public decimal balance { get; set; }
        public string memo { get; set; }
        public string poNumber { get; set; }
        public string state { get; set; }
        public string vendorId { get; set; }
        public List<Expens> expenses { get; set; }
        public List<LineItems> items { get; set; }
    }
    public class GetInvoicEntity
    {
        public int TERMS_ID { get; set; }
        public string INVOICE_CURRENCY_CODE { get; set; }
        public DateTime DUE_DATE { get; set; }
        public DateTime INVOICE_DATE { get; set; }
        public int INVOICE_NUM { get; set; }
        public string INVOICE_AMOUNT { get; set; }
        public int PARTY_ID { get; set; }
        public string VENDOR_ID { get; set; }
        public string PO_NUMBER { get; set; }
        public int INVOICE_ID { get; set; }
        public decimal TOTAL_TAX_AMOUNT { get; set; }
        public int INVENTORY_ITEM_ID { get; set; }
        public string PA_QUANTITY { get; set; }
        public decimal UNIT_PRICE { get; set; }
        public string HISTORICAL_FLAG { get; set; }
        public string ITEM_DESCRIPTION { get; set; }
        public string DESCRIPTION { get; set; }
        public int TAX_RATE { get; set; }
        public decimal AMOUNT { get; set; }
        public string LINE_NUMBER { get; set; }
        public string BALANCE { get; set; }
        public string VENDOR_NAME { get; set; }
        public string VENDOR_TYPE { get; set; }
        public string VENDOR_SITE { get; set; }
        public string PAY_GROUP { get; set; }
        public string INV_TYPE { get; set; }
        public string VOUCHER { get; set; }
        public DateTime CREATION_DATE { get; set; }
    }
}
