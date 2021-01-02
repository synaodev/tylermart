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
		public IActionResult Index([FromRoute] int LocationID) {
			if (!this.IsCustomerLoggedIn()) {
				return Redirect("/Customer/Logout");
			}
			Customer customer = this.GetCurrentCustomer(Db);
			if (!Db.Locations.Exists(LocationID)) {
				return Redirect("/Customer/Index");
			}
			Location location = Db.Locations.Get(LocationID);
			return View();
		}
		[HttpGet]
		public IActionResult AddToCart(OrderViewModel model) {
			if (model.Inventory.Any(kv => kv.Value == model.Selection)) {
				Product p = model.Inventory.Keys.Single(p => p.ID == model.Selection);
				model.Inventory[p]--;
				model.ShoppingCart.Add(p);
				model.Selection = 0;
			}
			return View("Order", model);
		}
		[HttpGet]
		public IActionResult RemoveFromCart(OrderViewModel model) {
			if (model.Inventory.Any(kv => kv.Value == model.Selection)) {
				Product p1 = model.Inventory.Keys.Single(p => p.ID == model.Selection);
				model.Inventory[p1]++;
				Product p2 = model.ShoppingCart.FindLast(p => p.ID == model.Selection);
				model.ShoppingCart.Remove(p2);
				model.Selection = 0;
			}
			return View("Order", model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult SendOrder(OrderViewModel model) {
			if (!ModelState.IsValid) {
				Logger.LogDebug("Invalid model state!");
				Logger.LogDebug(model.ToString());
				return View("Order", model);
			}
			return Redirect("/Customer/Index");
		}
	}
}
