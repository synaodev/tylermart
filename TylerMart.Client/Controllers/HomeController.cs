using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using TylerMart.Domain.Models;
using TylerMart.Client.Models;

namespace TylerMart.Client.Controllers {
	public class HomeController : Controller {
		private readonly ILogger<HomeController> Logger;
		public HomeController(ILogger<HomeController> logger) {
			Logger = logger;
		}
		[HttpGet]
		public IActionResult Index() {
			int ID = HttpContext.Session
				.GetInt32(nameof(Customer.ID))
				.GetValueOrDefault();
			if (ID == 0) {
				return View();
			}
			return Redirect("/Customer/Index");
		}
		[HttpGet]
		public IActionResult Privacy() {
			return View();
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
