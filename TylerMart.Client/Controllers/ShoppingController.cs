using System;
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
		private static readonly string MSG_CREATE_ORDER_FAILURE = "Was not able to create order!";
		private static readonly string MSG_RETRIEVE_ORDER_FAILURE = "Was not able to retrieve order from timestamp!";
		private static readonly string MSG_PRODUCTS_ADD_FAILURE = "Was not able to add products to order!";
		private static readonly string MSG_PRODUCTS_REMOVE_FAILURE = "Was not able to remove products from location!";
		private static readonly string MSG_ROLLBACK_FAILURE = "Was not able to rollback transaction! Situation requires admininstrator assistance!";
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
			if (!this.IsCustomerLoggedIn()) {
				return Redirect("/Customer/Logout");
			}
			if (!this.IsLocationAssigned()) {
				return Redirect("/Customer/Index");
			}
			DateTime now = DateTime.Now;
			bool createdOrder = Db.Orders.Create(new Order() {
				Complete = false,
				CreatedAt = now,
				CustomerID = model.Customer.ID,
				LocationID = model.Location.ID
			});
			if (!createdOrder) {
				Logger.LogDebug(MSG_CREATE_ORDER_FAILURE);
				Logger.LogDebug(model.ToString());
				ViewBag.Error = MSG_CREATE_ORDER_FAILURE;
				return View("Index", model);
			}
			Order order = Db.Orders.GetByTimestamp(now);
			if (order == null) {
				Logger.LogDebug(MSG_RETRIEVE_ORDER_FAILURE);
				Logger.LogDebug($"Timestamp: {now}");
				ViewBag.Error = MSG_RETRIEVE_ORDER_FAILURE;
				return View("Index", model);
			}
			bool productsAddedToOrder = Db.Orders.AddProducts(order, model.ShoppingCart);
			if (!productsAddedToOrder) {
				Logger.LogDebug(MSG_PRODUCTS_ADD_FAILURE);
				Logger.LogDebug($"Order ID: {order.ID}");
				ViewBag.Error = MSG_PRODUCTS_ADD_FAILURE;
				return View("Index", model);
			}
			bool productsRemovedFromLocation = Db.Locations.RemoveProducts(model.Location, model.ShoppingCart);
			if (!productsRemovedFromLocation) {
				Logger.LogDebug(MSG_PRODUCTS_REMOVE_FAILURE);
				Logger.LogDebug($"Location ID: {model.Location.ID}");
				ViewBag.Error = MSG_PRODUCTS_REMOVE_FAILURE;

				bool rollback = Db.Orders.RemoveProducts(order, model.ShoppingCart);
				if (!rollback) {
					Logger.LogCritical(MSG_ROLLBACK_FAILURE);
					return Redirect("/Customer/Logout");
				}
				return View("Index", model);
			}
			HttpContext.Session.Remove("LocationID");
			return Redirect("/Customer/Index");
		}
	}
}
