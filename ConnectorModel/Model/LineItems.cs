using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectorModel.Model
{
    public class LineItems
    {
        public string companyItemId { get; set; }
        public string poId { get; set; }
        public int quantityReceived { get; set; }
        public int billedQuantity { get; set; }
        public int quantity { get; set; }
        public string classificationId { get; set; }
        public string departmentId { get; set; }
        public string locationId { get; set; }
        public string glAccountId { get; set; }
        public decimal amount { get; set; }
        public decimal amountDue { get; set; }
        public decimal unitCost { get; set; }
        public bool closed { get; set; }
        public string lineNumber { get; set; }
        public string itemName { get; set; }
        public string description { get; set; }
        public object memo { get; set; }

    }

}
