using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectorModel.Model;
namespace ODBCConnector.PopulateModul
{
    public static class PopulateModule
    {
        public static List<PurchaseOrder> PopulatePoModel(List<GetPurchaseOrderEntity> objectList)
        {

            var poNumberList = objectList.GroupBy(c => c.poNumber).ToList();
            List<PurchaseOrder> purchaseOrderList = new List<PurchaseOrder>();
            List<Expens> expensList = new List<Expens>();
            foreach (var poNumber in poNumberList)
            {
                List<GetPurchaseOrderEntity> listItem = objectList.Where(x => x.poNumber == poNumber.Key).ToList();
                GetPurchaseOrderEntity header = listItem.FirstOrDefault();

                List<LineItems> lineItemList = new List<LineItems>();
                PurchaseOrder perOrder = new PurchaseOrder();
                perOrder.poNumber = header.poNumber;
                perOrder.poType = header.poType;
                perOrder.termId = header.termId;
                perOrder.vendorId = header.vendorId;
                perOrder.dueDate = header.dueDate;
                perOrder.departmentId = header.departmentId;
                perOrder.termId = header.termId;
                perOrder.locationId = header.locationId;
                perOrder.LastSavedAt = header.LastSavedAt;

                foreach (var item in listItem)
                {
                    LineItems lineItem = new LineItems();
                    lineItem.companyItemId = item.companyItemId;
                    lineItem.itemName = item.ItemName;
                    lineItem.amount = item.amount;
                    lineItem.departmentId = item.departmentId;
                    lineItem.locationId = item.locationId;
                    lineItem.billedQuantity = item.billedQuantity;
                    lineItem.quantity = item.quantity;
                    lineItem.amountDue = item.amount;
                    lineItem.unitCost = item.unitCost;
                    lineItem.lineNumber = item.lineNumber;
                    lineItem.description = item.description;
                    lineItemList.Add(lineItem);
                }
                perOrder.items = lineItemList;
                purchaseOrderList.Add(perOrder);
            }

            return purchaseOrderList;
        }
        public static List<StandardInvoice> PopulateInvoiceModel(List<GetStandardInvoiceEntity> objectList)
        {
            var InvoiceIdList = objectList.GroupBy(c => c.InvoiceId).ToList();
            List<StandardInvoice> standardInvoiceList = new List<StandardInvoice>();
            List<Expens> expensList = new List<Expens>();
            foreach (var invoiceId in InvoiceIdList)
            {
                List<GetStandardInvoiceEntity> listItems = objectList.Where(x => x.InvoiceId == invoiceId.Key).ToList();
                GetStandardInvoiceEntity header = listItems.FirstOrDefault();

                List<Expens> expensItemList = new List<Expens>();
                StandardInvoice perStandardInvoice = new StandardInvoice();

                perStandardInvoice.InvoiceId = header.InvoiceId;
                perStandardInvoice.InvoiceDate = header.InvoiceDate;
                perStandardInvoice.InvoiceNumber = header.InvoiceNumber;
                perStandardInvoice.VendorId = header.VendorId;
                perStandardInvoice.DepartmentId = header.DepartmentId;
                perStandardInvoice.LocationId = header.LocationId;
                perStandardInvoice.TermId = header.TermId;
                perStandardInvoice.DiscountDueDate = header.DiscountDueDate;
                perStandardInvoice.DueDate = header.DueDate;
                perStandardInvoice.ClassId = header.ClassId;
                perStandardInvoice.Memo = header.Memo;
                perStandardInvoice.PayStatus = header.PayStatus;
                perStandardInvoice.Currency = header.Currency;
                perStandardInvoice.LastSavedAt = header.LastSavedAt;


                foreach (var expens in listItems)
                {
                    Expens expensItem = new Expens();
                    expensItem.glAccountId = expens.AccountId;
                    expensItem.amount = expens.InvoiceAmount;
                    expensItem.memo = expens.Memo;
                    expensItemList.Add(expensItem);
                    perStandardInvoice.InvoiceAmount += expens.InvoiceAmount;

                }
                perStandardInvoice.RemainingAmount = perStandardInvoice.InvoiceAmount;
                perStandardInvoice.expenses = expensItemList;
                standardInvoiceList.Add(perStandardInvoice);


            }
            return standardInvoiceList;
        }
        public static List<TwowayInvoice> PopulateTwowayInvoiceModel(List<GetTwowayInvoiceEntity> objectList)
        {
            var billIdList = objectList.GroupBy(c => c.billId).ToList();
            List<TwowayInvoice> twowayInvoiceList = new List<TwowayInvoice>();
            List<LineItems> itemsList = new List<LineItems>();
            foreach (var billId in billIdList)
            {
                List<GetTwowayInvoiceEntity> listItems = objectList.Where(x => x.billId == billId.Key).ToList();
                GetTwowayInvoiceEntity header = listItems.FirstOrDefault();

                TwowayInvoice perTwowayInvoice = new TwowayInvoice();

                perTwowayInvoice.InvoiceId = header.billId;
                perTwowayInvoice.InvoiceDate = header.billDate;
                perTwowayInvoice.InvoiceNumber = header.invoiceNumber;
                perTwowayInvoice.TermId = header.billTermId;
                perTwowayInvoice.VendorId = header.billVendorId;
                perTwowayInvoice.DepartmentId = header.billDepartementId;
                perTwowayInvoice.DiscountDueDate = header.billDiscountDueDate;
                perTwowayInvoice.DueDate = header.billDueDate;
                perTwowayInvoice.ClassId = header.billClassId;
                perTwowayInvoice.PayStatus = header.billPayStatus;
                perTwowayInvoice.LocationId = header.billLocationId;
                perTwowayInvoice.Currency = header.billCurrency;
                perTwowayInvoice.LastSavedAt = header.LastSavedAt;

                itemsList.Clear();
                foreach (var lineItem in listItems)
                {
                    LineItems perlineItem = new LineItems();
                    perlineItem.poId = lineItem.itemPoNumber;
                    perlineItem.companyItemId = lineItem.itemId;
                    perlineItem.lineNumber = lineItem.itemLineNumber;
                    perlineItem.locationId = lineItem.itemLocationId;
                    perlineItem.departmentId = lineItem.itemDepartmentId;
                    perlineItem.itemName = lineItem.itemName;
                    perlineItem.classificationId = lineItem.itemClassId;
                    perlineItem.quantity = lineItem.orderquantity;
                    perlineItem.unitCost = lineItem.unitCost;
                    perlineItem.description = lineItem.description;
                    perlineItem.amount = lineItem.orderquantity * lineItem.unitCost;
                    perlineItem.amountDue = perlineItem.amount;
                    perTwowayInvoice.InvoiceAmount += perlineItem.amount;
                    itemsList.Add(perlineItem);
                    

                }
                perTwowayInvoice.RemainingAmount = perTwowayInvoice.InvoiceAmount;
                perTwowayInvoice.LineItems = itemsList;
                twowayInvoiceList.Add(perTwowayInvoice);
                


            }
            return twowayInvoiceList;
        }
        public static List<Payments> PopulatePaymentModel(List<GetPaymentsEntity> objectList)
        {
            var PaymentIdList = objectList.GroupBy(c => c.paymentId).ToList();
            List<Payments> paymentList = new List<Payments>();
            List<Bill> billList = new List<Bill>();
            foreach (var paymentId in PaymentIdList)
            {
                List<GetPaymentsEntity> listItems = objectList.Where(x => x.paymentId == paymentId.Key).ToList();
                GetPaymentsEntity header = listItems.FirstOrDefault();
                List<Bill> billItemList = new List<Bill>();
                Payments perPayment = new Payments();

                perPayment.paymentId = header.paymentId;
                perPayment.fundMethod = header.fundMethod;
                perPayment.transactionDate = header.transactionDate;
                perPayment.amount = header.paymentAmount;
                perPayment.vendorId = header.vendorId;
                perPayment.LastSavedAt = header.LastSavedAt;
             
                foreach (var bills in listItems)
                {
                    Bill bill = new Bill();
                    bill.invoiceId = bills.invoiceId;
                    bill.appliedPaymentAmount = bills.invoiceAmount;
                    bill.appliedDiscountAmount = bills.discountAmount;
                    billItemList.Add(bill);
                }
                perPayment.bills = billItemList;
                paymentList.Add(perPayment);

            }
            return paymentList;
        }

        //public static List<Invoice> PopulateInvoiceModel(List<GetInvoicEntity> objectList)
        //{
        //    List<Invoice> InvoiceList = new List<Invoice>();
        //    Invoice invoiceObj = new Invoice();
        //    Bills bill = new Bills();
        //    List<Item> items = new List<Item>();
        //    Item newVal = new Item();
        //    foreach (var item in objectList)
        //    {
        //        bill.id = item.INVOICE_ID;
        //        //bill.AMOUNT = item.INVOICE_AMOUNT;
        //        // bill.CREATION_DATE = item.INVOICE_DATE;
        //        bill.memo = item.DESCRIPTION;
        //        bill.poNumber = item.PO_NUMBER;
        //        bill.vendor = new Vendor { id = item.PARTY_ID };
        //        bill.totalTaxAmount = new TotalTaxAmount { amount = item.TOTAL_TAX_AMOUNT };
        //        bill.balance = new Balance { };
        //        bill.amount = new Amount { amount = item.AMOUNT };
        //        bill.term = new Term { id = item.TERMS_ID };
        //        bill.currency = item.INVOICE_CURRENCY_CODE;
        //        bill.dueDate = item.DUE_DATE;
        //        bill.transactionDate = item.INVOICE_DATE;
        //        bill.invoiceNumber = item.INVOICE_NUM;
        //        List<GetInvoicEntity> listItem = objectList.Where(x => x.INVOICE_ID == item.INVOICE_ID).ToList();
        //        foreach (var getItem in listItem)
        //        {
        //            newVal.companyItem = new CompanyItem { id = getItem.INVENTORY_ITEM_ID };
        //            newVal.name = getItem.DESCRIPTION;
        //            newVal.taxRate = new TaxRate { amount = getItem.TAX_RATE };
        //            newVal.lineNumber = getItem.LINE_NUMBER;
        //            newVal.cost = new Cost { amount = getItem.UNIT_PRICE };
        //            newVal.taxAmount = new TaxAmount { amount = getItem.TOTAL_TAX_AMOUNT };
        //            newVal.netAmount = new NetAmount { amount = getItem.AMOUNT };
        //            items.Add(newVal);
        //        }
        //        bill.items = items;
        //        invoiceObj.bill = bill;
        //        InvoiceList.Add(invoiceObj);
        //    }
        //    return InvoiceList;
        //}
        //public static List<Payment> PopulatePaymentModel(List<GetPaymentEntity> objectList)
        //{
        //    List<Payment> paymentList = new List<Payment>();
        //    Payment payment = new Payment();
        //    List<Bill> bills = new List<Bill>();
        //    Bill bill = new Bill();
        //    foreach (var item in objectList)
        //    {
        //        payment.paymentMethod = new PaymentMethod { id = item.PAYMENT_ID };
        //        payment.fundMethod = new FundMethod { type = item.FUNDMETHOD.ToString() };
        //        bill = new Bill { id = item.INVOICE_ID, appliedPaymentAmount = new AppliedPaymentAmount { amount = item.AppliedPaymentAmount } };
        //        bills.Add(bill);
        //        payment.bills = bills;
        //        payment.checkNumber = item.CHECK_NUMBER.ToString();
        //        paymentList.Add(payment);
        //    }
        //    return paymentList;
        //}
    }
}
