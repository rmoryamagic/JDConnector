using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectorModel.Model
{

    public class VoucherTwoWay
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
    public class GetVoucherTwoWayEntity
    {
        public string billId { get; set; }
        public string termId { get; set; }
        public string currency { get; set; }
        public string classificationId { get; set; }
        public string departmentId { get; set; }
        public string locationId { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime transactionDate { get; set; }
        public string invoiceNumber { get; set; }
        public decimal amount { get; set; }
        public decimal balance { get; set; }
        public decimal totalTaxAmount { get; set; }
        public string memo { get; set; }
        public string poNumber { get; set; }
        public string state { get; set; }
        public string vendorId { get; set; }
    }
}
