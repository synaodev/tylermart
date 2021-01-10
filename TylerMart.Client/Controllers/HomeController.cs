using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using TylerMart.Client.Models;
using TylerMart.Client.Services;
using TylerMart.Client.Utility;

namespace TylerMart.Client.Controllers {
	/// <summary>
	/// Home Controller
	/// </summary>
	public class HomeController : Controller {
		private readonly ILogger<HomeController> Logger;
		private readonly DatabaseService Db;
		/// <summary>
		/// Constructor that takes logger and instance of database context
		/// </summary>
		/// <param name="logger">Injected logger</param>
		/// <param name="db">Injected database context</param>
		public HomeController(ILogger<HomeController> logger, DatabaseService db) {
			Logger = logger;
			Db = db;
		}
		/// <summary>
		/// "Index" Action (GET)
		/// </summary>
		/// <remarks>
		/// Redirects to "/Customer/Index" if logged in
		/// </remarks>
		[HttpGet]
		public IActionResult Index() {
			if (this.IsCustomerLoggedIn()) {
				return Redirect("/Customer/Index");
			}
			return View();
		}
		/// <summary>
		/// "Privacy" Action (GET)
		/// </summary>
		[HttpGet]
		public IActionResult Privacy() {
			return View();
		}
		/// <summary>
		/// "Error" Action (?)
		/// </summary>
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel {
				RequestId = Activity.Current?.Id ??
					HttpContext.TraceIdentifier
			});
		}
	}
}
