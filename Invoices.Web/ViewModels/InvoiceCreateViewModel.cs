using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoices.Web.ViewModels
{
    public class SelectedItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
    public class InvoiceCreateViewModel
    {
        public string RecipientName { get; set; }
        public List<SelectListItem> Items { get; set; }
        public List<SelectListItem> Taxes { get; set; }
        public int Tax { get; set; }
        public int QuantitySold { get; set; }
        public List<SelectedItem> SelectedItems { get; set; }
    }
}
