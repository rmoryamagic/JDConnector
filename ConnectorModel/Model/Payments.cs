using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectorModel.Model
{

    public class GetPaymentsEntity
    {

        public string paymentId { get; set; }
        public string fundMethod { get; set; }
        public decimal paymentAmount { get; set; }
        public DateTime? transactionDate { get; set; }
        public string invoiceId { get; set; }
        public string vendorId { get; set; }
        public decimal invoiceAmount { get; set; }
        public decimal discountAmount { get; set; }
        public DateTime LastSavedAt { get; set; }
    }
        public class Payments
    {
        public string paymentId { get; set; }
        public string vendorId { get; set; }
        public string fundMethod { get; set; }
        public decimal amount { get; set; }
        public DateTime? transactionDate { get; set; }
        public List<Bill> bills { get; set; }
        public DateTime LastSavedAt { get; set; }
    }
}
