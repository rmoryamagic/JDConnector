using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectorModel.Model
{

    public class TwowayInvoice
    {
        public string InvoiceId { get; set; }
        public string TermId { get; set; }
        public string ClassId { get; set; }
        public string DepartmentId { get; set; }
        public string LocationId { get; set; }
        public string Currency { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DiscountDueDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public string PayStatus { get; set; }
        public string  VendorId { get; set; }
        public string Memo { get; set; }
        public List<Expens> expenses { get; set; }
        public List<LineItems> LineItems { get; set; }
        public DateTime LastSavedAt { get; set; }
    }
    public class GetTwowayInvoiceEntity
    {

        public string billId { get; set; }
        public string billTermId { get; set; }
        public string billClassId { get; set; }
        public string billDepartementId { get; set; }
        public string billLocationId { get; set; }
        public DateTime billDueDate { get; set; }
        public DateTime billDiscountDueDate { get; set; }
        public DateTime billDate { get; set; }
        public string invoiceNumber { get; set; }
        public string billCurrency { get; set; }
        public decimal billAmount { get; set; }
        public decimal billRemainingAmount { get; set; }
        public string billVendorId { get; set; }
        public string billPayStatus { get; set; }
        public string itemId { get; set; }
        public string itemName { get; set; }
        public string itemLineNumber { get; set; }
        public string itemClassId { get; set; }
        public string itemDepartmentId { get; set; }
        public string itemLocationId { get; set; }
        public string orderType { get; set; }
        public string glAccountId { get; set; }
        public int orderquantity { get; set; }
        public decimal unitCost { get; set; }
        public string taxRate { get; set; }
        public decimal taxAmount { get; set; }
        public decimal extendedCost { get; set; }
        public string Currency { get; set; }
        public string description { get; set; }
        public DateTime LastSavedAt { get; set; }
        public decimal amountopen { get; set; }
        public string itemPoNumber { get; set; }

    }

}
