using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using TylerMart.Domain.Models;
using TylerMart.Client.Models;
using TylerMart.Client.Services;
using TylerMart.Client.Utility;

namespace TylerMart.Client.Controllers {
	public class ShoppingController : Controller {
		private readonly ILogger<ShoppingController> Logger;
		private readonly DatabaseService Db;
		public ShoppingController(ILogger<ShoppingController> logger, DatabaseService db) {
			Logger = logger;
			Db = db;
		}
		[HttpGet]
		public IActionResult Index(OrderViewModel model) {
			if (!this.IsCustomerLoggedIn()) {
				return Redirect("/Customer/Logout");
			}
			Customer customer = this.GetCurrentCustomer(Db);
			if (!this.IsLocationAssigned()) {
				return Redirect("/Customer/Index");
			}
			Location location = this.GetCurrentLocation(Db);
			if (model == null) {
				model = new OrderViewModel(Db, customer, location);
			}
			return View("Index", model);
		}
		[HttpGet]
		public IActionResult Add(OrderViewModel model) {
			if (model.Inventory.Any(kv => kv.Value == model.Selection)) {
				Product p = model.Inventory.Keys.Single(p => p.ID == model.Selection);
				model.Inventory[p]--;
				model.ShoppingCart.Add(p);
				model.Selection = 0;
			}
			return View("Index", model);
		}
		[HttpGet]
		public IActionResult Remove(OrderViewModel model) {
			if (model.Inventory.Any(kv => kv.Value == model.Selection)) {
				Product p1 = model.Inventory.Keys.Single(p => p.ID == model.Selection);
				model.Inventory[p1]++;
				Product p2 = model.ShoppingCart.FindLast(p => p.ID == model.Selection);
				model.ShoppingCart.Remove(p2);
				model.Selection = 0;
			}
			return View("Index", model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Order(OrderViewModel model) {
			if (!ModelState.IsValid) {
				Logger.LogDebug("Invalid model state!");
				Logger.LogDebug(model.ToString());
				return View("Index", model);
			}
			HttpContext.Session.Remove("LocationID");
			return Redirect("/Customer/Index");
		}
	}
}
