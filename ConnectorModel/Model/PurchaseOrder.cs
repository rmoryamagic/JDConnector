using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectorModel.Model
{
    public class PurchaseOrder
    {
        public string poNumber { get; set; }
        public string departmentId { get; set; }
        public string classificationId { get; set; }
        public string locationId { get; set; }
        public string termId { get; set; }
        public string vendorId { get; set; }
        public bool active { get; set; }
        public string poType { get; set; }
        public string state { get; set; }
        public string description { get; set; }
        public string memo { get; set; }
        public DateTime dueDate { get; set; }
        public List<Expens> expens { get; set; }
        public List<LineItems> items { get; set; }

        public DateTime LastSavedAt { get; set; }
    }
    public class GetPurchaseOrderEntity
    {
        public string poNumber { get; set; }
        public DateTime dueDate { get; set; }
        public string vendorId { get; set; }
        public string termId { get; set; }
        public string poType { get; set; }
        public string departmentId { get; set; }
        public string locationId { get; set; }
        public string companyItemId { get; set; }
        public bool closed { get; set; }
        public int quantity { get; set; }
        public int quantityRecieved { get; set; }
        public int billedQuantity { get; set; }
        public string description { get; set; }
        public string lineNumber { get; set; }
        public decimal amount { get; set; }
        public decimal unitCost { get; set; }
        public DateTime LastSavedAt { get; set; }
        public string ItemName { get; set; }
            
    }
   
  
}