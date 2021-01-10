using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using TylerMart.Domain.Models;

namespace TylerMart.Client.Models {
	/// <summary>
	/// Order View Model
	/// </summary>
	public class OrderViewModel {
		/// <summary>
		/// Customer
		/// </summary>
		/// <remarks>
		/// Required to submit Order
		/// </remarks>
		public Customer Customer { get; set; }
		/// <summary>
		/// Location
		/// </summary>
		/// <remarks>
		/// Required to submit Order
		/// </remarks>
		public Location Location { get; set; }
		/// <summary>
		/// List of Products
		/// </summary>
		/// <remarks>
		/// Required to submit Order
		/// </remarks>
		public List<Product> ShoppingCart { get; set; }
		/// <summary>
		/// Dictonary containing Product keys to quantity values
		/// </summary>
		/// <remarks>
		/// Used for Razor Views
		/// </remarks>
		public Dictionary<Product, int> Inventory { get; set; }
		/// <summary>
		/// Selected Product ID
		/// </summary>
		/// <remarks>
		/// Used for Razor Views
		/// </remarks>
		public int Selection { get; set; }
		/// <summary>
		/// Default constructor
		/// </summary>
		public OrderViewModel() {}
		/// <summary>
		/// Check to see if the model is valid
		/// </summary>
		/// <param name="logger">Logger for a particular controller</param>
		/// <remarks>
		/// Prints all errors to logger
		/// </remarks>
		/// <returns>
		/// Returns 'true' if valid
		/// </returns>
		public bool ManuallyValidate<T>(ILogger<T> logger) {
			bool result = true;
			if (Customer == null) {
				result = false;
				logger.LogWarning("Customer is required!");
			}
			if (Location == null) {
				result = false;
				logger.LogWarning("Location is required!");
			}
			if (ShoppingCart == null) {
				result = false;
				logger.LogWarning("Shopping Cart is required!");
			} else if (ShoppingCart.Count == 0) {
				result = false;
				logger.LogWarning("Must submit at least one product for Order!");
			}
			return result;
		}
		/// <summary>
		/// Order View Model prettifier
		/// </summary>
		/// <returns>
		/// Model as a string
		/// </returns>
		public override string ToString() {
			string result = "Order = {";
			if (Customer != null) {
				result += $"\n\tCustomer ID = {Customer.ID}";
				result += $"\n\tCustomer Name = {Customer.FirstName} {Customer.LastName}";
			}
			if (Location != null) {
				result += $"\n\tLocation ID = {Location.ID}";
				result += $"\n\tLocation Name = {Location.Name}";
			}
			if (ShoppingCart != null && ShoppingCart.Count > 0) {
				result += "\n\tShopping Cart = {";
				int count = 0;
				foreach (var product in ShoppingCart) {
					result += $"\n\t\t({count}) Product ID = {product.ID}";
					result += $"\n\t\t({count}) Product Name = {product.Name}";
					count++;
				}
				result += "\n\t}";
			}
			result += "\n}";
			result += "\nView = {";
			if (Inventory != null) {
				result += "\n\tInventory = {";
				foreach (var kv in Inventory) {
					result += $"\n\t\tProduct ID = {kv.Key.ID}";
					result += $"\n\t\tProduct Name = {kv.Key.Name}";
					result += $"\n\t\tProduct Count = {kv.Value}";
				}
				result += "\n\t}";
			}
			result += $"\n\tSelected ID = {Selection}";
			result += "\n}";
			return result;
		}
	}
}
