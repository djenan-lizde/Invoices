using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Invoices.Models.Models
{
    public class InvoiceItems
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        [ForeignKey(nameof(Invoice))]
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = ("Please enter valid integer Number"))]
        public int QuantitySold { get; set; }
        public float PriceWithoutTax { get; set; }
        public float PriceWithTax { get; set; }
        public int Tax { get; set; }
    }
}
