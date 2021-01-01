using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using TylerMart.Client.Models;
using TylerMart.Client.Services;
using TylerMart.Client.Utility;

namespace TylerMart.Client.Controllers {
	public class HomeController : Controller {
		private readonly ILogger<HomeController> Logger;
		private readonly DatabaseService Db;
		public HomeController(ILogger<HomeController> logger, DatabaseService db) {
			Logger = logger;
			Db = db;
		}
		[HttpGet]
		public IActionResult Index() {
			if (this.IsCustomerLoggedIn(Db)) {
				return Redirect("/Customer/Index");
			}
			return View();
		}
		[HttpGet]
		public IActionResult Privacy() {
			return View();
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel {
				RequestId = Activity.Current?.Id ??
					HttpContext.TraceIdentifier
			});
		}
	}
}
