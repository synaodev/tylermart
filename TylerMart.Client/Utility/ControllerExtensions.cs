using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using TylerMart.Domain.Models;
using TylerMart.Client.Services;

namespace TylerMart.Client.Utility {
	/// <summary>
	/// Extensions for <see cref="Microsoft.AspNetCore.Mvc.Controller"/>
	/// </summary>
	public static class ControllerExtensions {
		/// <summary>
		/// Check if <see cref="TylerMart.Domain.Models.Customer"/> is logged in
		/// </summary>
		/// <param name="controller">This controller</param>
		/// <returns>
		/// Returns 'true' if a Customer is logged in
		/// </returns>
		public static bool IsCustomerLoggedIn(this Controller controller) {
			int ID = controller.HttpContext.Session
				.GetInt32("CustomerID")
				.GetValueOrDefault();
			return ID > 0;
		}
		/// <summary>
		/// Get logged in <see cref="TylerMart.Domain.Models.Customer"/>
		/// </summary>
		/// <param name="controller">This controller</param>
		/// <param name="db">Database service</param>
		/// <returns>
		/// Returns logged in Customer or null
		/// </returns>
		public static Customer GetCurrentCustomer(this Controller controller, DatabaseService db) {
			int ID = controller.HttpContext.Session
				.GetInt32("CustomerID")
				.GetValueOrDefault();
			return ID > 0 ?
				db.Customers.Get(ID) :
				null;
		}
		/// <summary>
		/// Check if <see cref="TylerMart.Domain.Models.Location"/> is assigned to session
		/// </summary>
		/// <param name="controller">This controller</param>
		/// <returns>
		/// Returns 'true' if a Location is assigned to session
		/// </returns>
		public static bool IsLocationAssigned(this Controller controller) {
			int ID = controller.HttpContext.Session
				.GetInt32("LocationID")
				.GetValueOrDefault();
			return ID > 0;
		}
		/// <summary>
		/// Get assigned <see cref="TylerMart.Domain.Models.Location"/> in session
		/// </summary>
		/// <param name="controller">This controller</param>
		/// <param name="db">Database service</param>
		/// <returns>
		/// Returns assigned Location or null
		/// </returns>
		public static Location GetCurrentLocation(this Controller controller, DatabaseService db) {
			int ID = controller.HttpContext.Session
				.GetInt32("LocationID")
				.GetValueOrDefault();
			return ID > 0 ?
				db.Locations.Get(ID) :
				null;
		}
	}
}
