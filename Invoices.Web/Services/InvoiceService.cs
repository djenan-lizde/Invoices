using AutoMapper;
using Invoices.Models.Models;
using Invoices.Web.Database;
using Invoices.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Invoices.Web.Services
{
    public interface IInvoiceService : IData<InvoiceItems>
    {
        List<InvoiceOutputViewModel> GetInvoices(string userId, string userName);
    }
    public class InvoiceService : Data<InvoiceItems>, IInvoiceService
    {
        public InvoiceService(AppDbContext context, IMapper mapper) : base(context, mapper) { }

        public List<InvoiceOutputViewModel> GetInvoices(string userId, string userName)
        {
            var list = _context.Invoices
                .Where(x => x.UserId == userId)
                .Select(x => new InvoiceOutputViewModel
                {
                    InvoiceId = x.Id,
                    InvoiceNumber = x.InvoiceNumber,
                    DateCreation = x.DateCreation,
                    PaymentDate = x.PaymentDate,
                    RecipientName = x.RecipientName,
                    User = userName,
                    InvoiceItems = _context.InvoiceItems.Where(y => y.InvoiceId == x.Id)
                        .Select(y => new InvoiceItem
                        {
                            Description = y.Item.Description,
                            ItemPrice = y.Item.Price,
                            QuantitySold = y.QuantitySold,
                            ItemChargePrice = y.Item.Price * y.QuantitySold
                        }).ToList(),
                    TotalPriceWOTax = _context.InvoiceItems.Where(z => z.InvoiceId == x.Id)
                            .Sum(z => z.PriceWithoutTax),
                    TotalPriceWithTax = _context.InvoiceItems.Where(z => z.InvoiceId == x.Id)
                            .Sum(z => z.PriceWithTax)
                }).ToList();

            return list;
        }
    }
}
