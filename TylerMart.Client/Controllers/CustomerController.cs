using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using TylerMart.Domain.Models;
using TylerMart.Client.Models;
using TylerMart.Client.Services;
using TylerMart.Client.Utility;

namespace TylerMart.Client.Controllers {
	/// <summary>
	/// Customer Controller
	/// </summary>
	public class CustomerController : Controller {
		private static readonly string MSG_REGISTER_EMAIL_FAILURE = "This email is already used by another customer! Please enter a different one.";
		private static readonly string MSG_REGISTER_UNKNOWN_FAILURE = "Customer registration failed for an unknown reason!";
		private static readonly string MSG_LOGIN_OBFUSCATED_FAILURE = "Either your email or password were incorrect!";
		private readonly ILogger<CustomerController> Logger;
		private readonly DatabaseService Db;
		/// <summary>
		/// Constructor that takes logger and instance of database context
		/// </summary>
		/// <param name="logger">Injected logger</param>
		/// <param name="db">Injected database context</param>
		public CustomerController(ILogger<CustomerController> logger, DatabaseService db) {
			Logger = logger;
			Db = db;
		}
		/// <summary>
		/// "History" Action (GET)
		/// </summary>
		/// <remarks>
		/// Redirects to "/Customer/Logout" if not logged in
		/// </remarks>
		[HttpGet]
		public IActionResult History() {
			if (!this.IsCustomerLoggedIn()) {
				return Redirect("/Customer/Logout");
			}
			Customer customer = this.GetCurrentCustomer(Db);
			ViewBag.Name = $"{customer.FirstName} {customer.LastName}";
			List<Order> orders = Db.Orders.FindFromCustomerWithDetails(customer);
			return View(orders);
		}
		/// <summary>
		/// "Index" Action (GET)
		/// </summary>
		/// <remarks>
		/// Redirects to "/Customer/Logout" if not logged in
		/// </remarks>
		[HttpGet]
		public IActionResult Index() {
			if (!this.IsCustomerLoggedIn()) {
				return Redirect("/Customer/Logout");
			}
			Customer c = this.GetCurrentCustomer(Db);
			ViewBag.Name = $"{c.FirstName} {c.LastName}";
			if (c.DefaultLocationID > 0) {
				Location location = Db.Locations.Get(c.DefaultLocationID);
				if (location != null) {
					ViewBag.Place = location.Name;
					ViewBag.Default = location.ID;
				}
			}
			return View();
		}
		/// <summary>
		/// "Search" Action (GET)
		/// </summary>
		/// <remarks>
		/// Redirects to "/Customer/Logout" if not logged in
		/// </remarks>
		[HttpGet]
		public IActionResult Search() {
			if (!this.IsCustomerLoggedIn()) {
				return Redirect("/Customer/Logout");
			}
			return View(new SearchViewModel());
		}
		/// <summary>
		/// "Search" Action (POST)
		/// </summary>
		/// <remarks>
		/// Redirects to "/Customer/Logout" if not logged in
		/// </remarks>
		[HttpPost]
		public IActionResult Search(SearchViewModel model) {
			if (!this.IsCustomerLoggedIn()) {
				return Redirect("/Customer/Logout");
			}
			bool firstEmpty = string.IsNullOrEmpty(model.FirstName);
			bool lastEmpty = string.IsNullOrEmpty(model.LastName);
			if (firstEmpty && lastEmpty) {
				ViewBag.Error = "Error! Neither field should be empty!";
				return View(model);
			} else if (!firstEmpty) {
				List<Customer> firstNameList = Db.Customers.FindByFirstName(model.FirstName);
				return View("Results", firstNameList);
			} else if (!lastEmpty) {
				List<Customer> lastNameList = Db.Customers.FindByLastName(model.LastName);
				return View("Results", lastNameList);
			}
			List<Customer> wholeNameList = Db.Customers.FindByWholeName(model.FirstName, model.LastName);
			return View("Results", wholeNameList);
		}
		/// <summary>
		/// "Register" Action (GET)
		/// </summary>
		[HttpGet]
		public IActionResult Register() {
			return View(new RegisterViewModel() {
				Locations = Db.Locations.All()
			});
		}
		/// <summary>
		/// "Register" Action (POST)
		/// </summary>
		[HttpPost]
		public IActionResult Register(RegisterViewModel model) {
			if (!ModelState.IsValid) {
				Logger.LogDebug("Invalid model state!");
				Logger.LogDebug(model.ToString());
				return View(model);
			}
			if (Db.Customers.ExistsWithEmail(model.Email)) {
				ViewBag.Error = MSG_REGISTER_EMAIL_FAILURE;
				return View(model);
			}
			Customer customer = model.Create();
			if (!Db.Customers.Create(customer)) {
				ViewBag.Error = MSG_REGISTER_UNKNOWN_FAILURE;
				return View(model);
			}
			return Redirect("/Customer/Login");
		}
		/// <summary>
		/// "Login" Action (GET)
		/// </summary>
		[HttpGet]
		public IActionResult Login() {
			return View();
		}
		/// <summary>
		/// "Login" Action (POST)
		/// </summary>
		[HttpPost]
		public IActionResult Login(LoginViewModel model) {
			if (!ModelState.IsValid) {
				Logger.LogDebug("Invalid model state!");
				Logger.LogDebug(model.ToString());
				return View(model);
			}
			Customer customer = Db.Customers.GetByEmail(model.Email);
			if (customer == null) {
				ViewBag.Error = MSG_LOGIN_OBFUSCATED_FAILURE;
				return View(model);
			}
			if (customer.Password != model.Password) {
				ViewBag.Error = MSG_LOGIN_OBFUSCATED_FAILURE;
				return View(model);
			}
			HttpContext.Session.SetInt32("CustomerID", customer.ID);
			return Redirect("/Customer/Index");
		}
		/// <summary>
		/// "Logout" Action (GET)
		/// </summary>
		/// <remarks>
		/// Redirects to "/Home/Index" after clearing session data
		/// </remarks>
		[HttpGet]
		public IActionResult Logout() {
			HttpContext.Session.Remove("CustomerID");
			HttpContext.Session.Remove("LocationID");
			HttpContext.Session.Remove("Cart");
			return Redirect("/Home/Index");
		}
	}
}
