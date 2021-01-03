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
		private static readonly string MSG_DESTROY_ORDER_FAILURE = "Was not able to destroy order after transaction failure! Situation requires administrative assistance!";
		private static readonly string MSG_ROLLBACK_FAILURE = "Was not able to complete order transaction! Transaction was rolled back!";
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
			if (model.LackingData()) {
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
			bool operationSuccess = Db.Products.ForwardOperation(
				model.ShoppingCart,
				order,
				model.Location
			);
			if (!operationSuccess) {
				Logger.LogDebug(MSG_ROLLBACK_FAILURE);
				ViewBag.Error = MSG_ROLLBACK_FAILURE;
				if (!Db.Orders.Delete(order)) {
					Logger.LogCritical(MSG_DESTROY_ORDER_FAILURE);
					Logger.LogCritical($"Order ID: {order.ID}");
					Logger.LogCritical($"Order Timestamp: {now}");
				}
				return View("Index", model);
			}
			HttpContext.Session.Remove("LocationID");
			return Redirect("/Customer/Index");
		}
	}
}
