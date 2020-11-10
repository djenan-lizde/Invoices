using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Invoices.Models.Models;
using Invoices.Web.Services;
using Invoices.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Invoices.Web.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _serviceInvoiceItems;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IData<Item> _serviceItem;
        private readonly IData<Invoice> _serviceInvoice;

        [Import(typeof(ICalculateService))]
        private readonly ICalculateService calculateService = new CalculateService();

        public InvoiceController(UserManager<IdentityUser> userManager, IInvoiceService serviceInvoiceItems,
            IData<Item> serviceItem, IData<Invoice> serviceInvoice)
        {
            _userManager = userManager;
            _serviceInvoice = serviceInvoice;
            _serviceItem = serviceItem;
            _serviceInvoiceItems = serviceInvoiceItems;
        }

        public IActionResult Index()
        {
            try
            {
                var user = _userManager.GetUserAsync(HttpContext.User);
                return View(_serviceInvoiceItems.GetInvoices(user.Result.Id, user.Result.UserName));
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        public IActionResult NewInvoice()
        {
            return View(new InvoiceCreateViewModel
            {
                Items = _serviceItem.Get().Select(x => new SelectListItem
                {
                    Text = $"{ x.Description } - {x.Price}",
                    Value = x.Id.ToString()
                }).ToList(),
                Taxes = new List<SelectListItem>
                {
                    new SelectListItem{ Text="BIH Tax - 17%", Value = "17" },
                    new SelectListItem{ Text="CRO Tax - 25%", Value = "25" }
                }.ToList()
            });
        }
        public IActionResult Save(InvoiceCreateViewModel viewModel)
        {
            if(viewModel.Tax == null)
            {
                return View("Error");
            }
            Random rnd = new Random();
            var invoice = _serviceInvoice.Insert(new Invoice
            {
                DateCreation = DateTime.Now,
                InvoiceNumber = GenerateProductNumber(10, rnd),
                RecipientName = viewModel.RecipientName,
                PaymentDate = DateTime.Now.AddDays(7),
                UserId = _userManager.GetUserId(HttpContext.User)
            });

            foreach (var item in viewModel.SelectedItems)
            {
                var itemInDb = _serviceItem.Get(x => x.Id == item.Id);
                _serviceInvoiceItems.Insert(new InvoiceItems
                {
                    InvoiceId = invoice.Id,
                    ItemId = item.Id,
                    QuantitySold = item.Quantity,
                    PriceWithTax = calculateService.CalculatePrice(itemInDb.Price, item.Quantity, viewModel.Tax),
                    PriceWithoutTax = calculateService.CalculatePrice(itemInDb.Price, item.Quantity),
                    Tax = viewModel.Tax
                });
            }

            return RedirectToAction(nameof(Index), "Home");
        }
        public static string GenerateProductNumber(int length, Random random)
        {
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }
    }
}
