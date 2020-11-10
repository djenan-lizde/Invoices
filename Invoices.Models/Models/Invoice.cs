using System;
using System.ComponentModel.DataAnnotations;

namespace Invoices.Models.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime PaymentDate { get; set; }
        public string UserId { get; set; }
        public string RecipientName { get; set; }
    }
}
