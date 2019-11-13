using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectorModel.Model
{
    public class Bill
    {
        public string invoiceId { get; set; }
        public decimal appliedPaymentAmount { get; set; }
        public decimal appliedDiscountAmount { get; set; }
    }
}
