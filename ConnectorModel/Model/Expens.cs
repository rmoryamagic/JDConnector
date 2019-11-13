using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectorModel.Model
{
    public class Expens
    {
        public string classificationId { get; set; }
        public string departmentId { get; set; }
        public string locationId { get; set; }
        public string glAccountId { get; set; }
        public decimal amount { get; set; }
        public bool closed { get; set; }
        public string lineNumber { get; set; }
        public string memo { get; set; }

    }
}
