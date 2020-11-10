using Invoices.Models.Models;
using Invoices.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Web.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IData<Item> _itemService;

        public ItemController(IData<Item> itemService)
        {
            _itemService = itemService;
        }
        public IActionResult NewItem()
        {
            return View(new Item());
        }
        public IActionResult Save(Item model)
        {
            if (ModelState.IsValid)
            {
                _itemService.Insert(new Item { Description = model.Description, Price = model.Price });
                return RedirectToAction("Index", "Home");
            }
            return View(nameof(NewItem),model);
        }
    }
}
