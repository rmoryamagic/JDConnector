using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectorModel.Model
{

    public class GetPaymentEntity
    {

        public int PAYMENT_ID { get; set; }
        public string FUNDMETHOD { get; set; }
        public double AMOUNT { get; set; }
        public DateTime? TRANSACTION_DATE { get; set; }
        public DateTime ACCOUNTING_DATE { get; set; }
        public string ACCOUNTING_PERIOD { get; set; }
        public int INVOICE_ID { get; set; }
        public decimal AppliedPaymentAmount { get; set; }
        public int CHECK_NUMBER { get; set; }
        
    }
}
