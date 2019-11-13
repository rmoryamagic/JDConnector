using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectorModel.Model
{

    public class StandardInvoice
    {
        public string InvoiceId { get; set; }
        public string LocationId { get; set; }
        public string TermId { get; set; }
        public string Currency { get; set; }
        public string ClassId { get; set; }
        public string DepartmentId { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DiscountDueDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public string AccountId { get; set; }
        public string PayStatus { get; set; }
        public string  VendorId { get; set; }
        public string Memo { get; set; }
        public List<Expens> expenses { get; set; }
        public DateTime LastSavedAt { get; set; }
    }
    public class GetStandardInvoiceEntity
    {

        public string InvoiceId { get; set; }
        public string LocationId { get; set; }
        public string TermId { get; set; }
        public string Currency { get; set; }
        public string ClassId { get; set; }
        public string DepartmentId { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DiscountDueDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public string VendorId { get; set; }
        public string AccountId { get; set; }
        public DateTime LastSavedAt { get; set; }
        public string PayStatus { get; set; }
        public string Memo { get; set; }
    }

}
