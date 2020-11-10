using System;
using System.Collections.Generic;

namespace Invoices.Web.ViewModels
{
    public class InvoiceItem
    {
        public string Description { get; set; }
        public int QuantitySold { get; set; }
        public float ItemPrice { get; set; }
        public float ItemChargePrice { get; set; }
    }
    public class InvoiceOutputViewModel
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime PaymentDate { get; set; }
        public string RecipientName { get; set; }
        public string User { get; set; }
        public float TotalPriceWithTax { get; set; }
        public float TotalPriceWOTax { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
