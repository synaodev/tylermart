using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using TylerMart.Domain.Models;
using TylerMart.Client.Models;
using TylerMart.Client.Services;
using TylerMart.Client.Utility;

namespace TylerMart.Client.Controllers {
	public class OrderController : Controller {
		private static readonly string MSG_CREATE_ORDER_FAILURE = "Was not able to create order!";
		private static readonly string MSG_RETRIEVE_ORDER_FAILURE = "Was not able to retrieve order from timestamp!";
		private static readonly string MSG_DESTROY_ORDER_FAILURE = "Was not able to destroy order after transaction failure! Situation requires administrative assistance!";
		private static readonly string MSG_ROLLBACK_FAILURE = "Was not able to complete order transaction! Transaction was rolled back!";
		private readonly ILogger<OrderController> Logger;
		private readonly DatabaseService Db;
		private List<Product> GetProductList(Dictionary<Product, int> inventory) {
			List<int> keys = HttpContext.Session.GetFromJson<List<int>>("Cart");
			List<Product> shoppingCart = new List<Product>();
			if (keys != null) {
				foreach (var key in keys) {
					Product p = inventory.Keys.Single(kv => kv.ID == key);
					inventory[p] -= 1;
					shoppingCart.Add(p);
				}
			}
			return shoppingCart;
		}
		public OrderController(ILogger<OrderController> logger, DatabaseService db) {
			Logger = logger;
			Db = db;
		}
		[HttpGet]
		public IActionResult Index() {
			if (!this.IsCustomerLoggedIn()) {
				return Redirect("/Customer/Logout");
			}
			List<Location> locations = Db.Locations.All();
			return View(locations);
		}
		[HttpGet]
		public IActionResult Location([FromRoute] int ID) {
			if (!this.IsCustomerLoggedIn()) {
				return Redirect("/Customer/Logout");
			}
			if (!Db.Locations.Exists(ID)) {
				return Redirect("/Order/Index");
			}
			this.HttpContext.Session.SetInt32("LocationID", ID);
			return Redirect("/Order/Create");
		}
		[HttpGet]
		public IActionResult History([FromRoute] int ID) {
			if (!this.IsCustomerLoggedIn()) {
				return Redirect("/Customer/Logout");
			}
			if (!Db.Locations.Exists(ID)) {
				Logger.LogDebug($"Couldn't find Location with ID = {ID}!");
				return Redirect("/Order/Index");
			}
			Location location = Db.Locations.Get(ID);
			ViewBag.LocationName = location.Name;
			List<Order> orders = Db.Orders.FindFromLocationWithDetails(location);
			return View(orders);
		}
		[HttpGet]
		public IActionResult Create() {
			if (!this.IsCustomerLoggedIn()) {
				return Redirect("/Customer/Logout");
			}
			OrderViewModel model = new OrderViewModel();
			model.Customer = this.GetCurrentCustomer(Db);
			if (!this.IsLocationAssigned()) {
				return Redirect("/Order/Index");
			}
			model.Location = this.GetCurrentLocation(Db);
			model.Inventory = Db.Products.CountAtLocation(model.Location);
			model.ShoppingCart = this.GetProductList(model.Inventory);
			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(OrderViewModel model) {
			if (!this.IsCustomerLoggedIn()) {
				return Redirect("/Customer/Logout");
			}
			model.Customer = this.GetCurrentCustomer(Db);
			if (!this.IsLocationAssigned()) {
				return Redirect("/Order/Index");
			}
			model.Location = this.GetCurrentLocation(Db);
			model.Inventory = Db.Products.CountAtLocation(model.Location);
			model.ShoppingCart = this.GetProductList(model.Inventory);
			if (!model.ManuallyValidate(Logger)) {
				Logger.LogDebug("Invalid model state!");
				Logger.LogDebug(model.ToString());
				return Redirect("/Order/Create");
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
				return Redirect("/Order/Create");
			}
			Order order = Db.Orders.GetByTimestamp(now);
			if (order == null) {
				Logger.LogDebug(MSG_RETRIEVE_ORDER_FAILURE);
				Logger.LogDebug($"Timestamp: {now}");
				ViewBag.Error = MSG_RETRIEVE_ORDER_FAILURE;
				return Redirect("/Order/Create");
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
				return Redirect("/Order/Create");
			}
			HttpContext.Session.Remove("Cart");
			HttpContext.Session.Remove("LocationID");
			return Redirect("/Customer/Index");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Add(OrderViewModel model) {
			if (!this.IsCustomerLoggedIn()) {
				return Redirect("/Customer/Logout");
			}
			if (!this.IsLocationAssigned()) {
				return Redirect("/Order/Index");
			}
			List<int> list = HttpContext.Session.GetFromJson<List<int>>("Cart");
			if (list == null) {
				list = new List<int>();
			}
			list.Add(model.Selection);
			HttpContext.Session.SetAsJson<List<int>>("Cart", list);
			return Redirect("/Order/Create");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Remove(OrderViewModel model) {
			if (!this.IsCustomerLoggedIn()) {
				return Redirect("/Customer/Logout");
			}
			if (!this.IsLocationAssigned()) {
				return Redirect("/Order/Index");
			}
			List<int> list = HttpContext.Session.GetFromJson<List<int>>("Cart");
			if (list == null) {
				list = new List<int>();
			}
			list.Remove(model.Selection);
			HttpContext.Session.SetAsJson<List<int>>("Cart", list);
			return Redirect("/Order/Create");
		}
	}
}
