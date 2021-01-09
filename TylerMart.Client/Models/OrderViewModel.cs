using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using TylerMart.Domain.Models;

namespace TylerMart.Client.Models {
	public class OrderViewModel {
		/*
		 * Fields required to submit order
		 */
		public Customer Customer { get; set; }
		public Location Location { get; set; }
		public List<Product> ShoppingCart { get; set; }
		/*
		 * Fields used for view & controller
		 */
		public Dictionary<Product, int> Inventory { get; set; }
		public int Selection { get; set; }
		public OrderViewModel() {}
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
