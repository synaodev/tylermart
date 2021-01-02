using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using TylerMart.Domain.Models;
using TylerMart.Client.Services;

namespace TylerMart.Client.Utility {
	public static class ControllerExtensions {
		public static bool IsCustomerLoggedIn(this Controller controller) {
			int ID = controller.HttpContext.Session
				.GetInt32("CustomerID")
				.GetValueOrDefault();
			return ID > 0;
		}
		public static Customer GetCurrentCustomer(this Controller controller, DatabaseService db) {
			int ID = controller.HttpContext.Session
				.GetInt32("CustomerID")
				.GetValueOrDefault();
			return ID > 0 ?
				db.Customers.Get(ID) :
				null;
		}
		public static bool IsLocationAssigned(this Controller controller) {
			int ID = controller.HttpContext.Session
				.GetInt32("LocationID")
				.GetValueOrDefault();
			return ID > 0;
		}
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
