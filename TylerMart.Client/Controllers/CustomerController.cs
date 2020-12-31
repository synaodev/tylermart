using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using TylerMart.Domain.Models;
using TylerMart.Client.Models;
using TylerMart.Client.Services;

namespace TylerMart.Client.Controllers {
	public class CustomerController : Controller {
		private static readonly string KEY_ERROR = "Error";
		private static readonly string MSG_REGISTER_EMAIL_FAILURE = "This email is already used by another customer! Please enter a different one.";
		private static readonly string MSG_REGISTER_UNKNOWN_FAILURE = "Customer registration failed for an unknown reason!";
		private static readonly string MSG_LOGIN_OBFUSCATED_FAILURE = "Either your email or password were incorrect!";
		private DatabaseService Db;
		private Customer GetCustomerIfLoggedIn() {
			int ID = HttpContext.Session
				.GetInt32(nameof(Customer.ID))
				.GetValueOrDefault();
			return ID > 0 ?
				Db.Customers.Get(ID) :
				null;
		}
		public CustomerController(DatabaseService db) {
			Db = db;
		}
		[HttpGet]
		public IActionResult Index() {
			Customer c = GetCustomerIfLoggedIn();
			if (c == null) {
				return Redirect("/Customer/Logout");
			}
			ViewBag["CustomerName"] = $"{c.FirstName} {c.LastName}";
			return View();
		}
		[HttpGet]
		public IActionResult Register() {
			return View();
		}
		[HttpPost]
		public IActionResult Register(RegisterViewModel model) {
			if (!ModelState.IsValid) {
				return View(model);
			}
			if (Db.Customers.ExistsWithEmail(model.Email)) {
				ViewBag[KEY_ERROR] = MSG_REGISTER_EMAIL_FAILURE;
				return View(model);
			}
			Customer customer = model.Create();
			if (!Db.Customers.Create(customer)) {
				ViewBag[KEY_ERROR] = MSG_REGISTER_UNKNOWN_FAILURE;
				return View(model);
			}
			return Redirect("/Customer/Login");
		}
		[HttpGet]
		public IActionResult Login() {
			return View();
		}
		[HttpPost]
		public IActionResult Login(LoginViewModel model) {
			if (!ModelState.IsValid) {
				return View(model);
			}
			Customer customer = Db.Customers.GetByEmail(model.Email);
			if (customer == null) {
				ViewBag[KEY_ERROR] = MSG_LOGIN_OBFUSCATED_FAILURE;
				return View(model);
			}
			if (customer.Password != model.Password) {
				ViewBag[KEY_ERROR] = MSG_LOGIN_OBFUSCATED_FAILURE;
				return View(model);
			}
			HttpContext.Session.SetInt32(nameof(Customer.ID), customer.ID);
			return Redirect("/Customer/Index");
		}
		[HttpGet]
		public IActionResult Logout() {
			HttpContext.Session.Remove(nameof(Customer.ID));
			return Redirect("/Home/Index");
		}
	}
}
