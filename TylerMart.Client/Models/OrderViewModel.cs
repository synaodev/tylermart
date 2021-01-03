using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using TylerMart.Client.Services;
using TylerMart.Domain.Models;

namespace TylerMart.Client.Models {
	public class OrderViewModel {
		/*
		 * Fields required to submit order
		 */
		[Required(ErrorMessage = "Customer is required!")]
		public Customer Customer { get; set; }
		[Required(ErrorMessage = "Location is required!")]
		public Location Location { get; set; }
		[Required(ErrorMessage = "Shopping Cart is required!")]
		[MinLength(1, ErrorMessage = "Must submit at least one product for Order!")]
		public List<Product> ShoppingCart { get; set; }
		/*
		 * Fields used for view & controller
		 */
		public Dictionary<Product, int> Inventory { get; set; }
		public int Selection { get; set; }
		public OrderViewModel(DatabaseService db, Customer customer, Location location) {
			Customer = customer;
			Location = location;
			Inventory = db.Products.CountAtLocation(location);
			ShoppingCart = new List<Product>();
		}
		public OrderViewModel() {
			Customer = null;
			Location = null;
			Inventory = null;
			ShoppingCart = null;
			Selection = 0;
		}
		public bool LackingData() {
			return Customer == null ||
				Location == null ||
				Inventory == null ||
				ShoppingCart == null;
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
