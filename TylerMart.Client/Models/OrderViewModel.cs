using System.Collections.Generic;

using TylerMart.Client.Services;
using TylerMart.Domain.Models;

namespace TylerMart.Client.Models {
	public class OrderViewModel {
		public Customer Customer { get; set; }
		public Location Location { get; set; }
		public Dictionary<Product, int> Inventory { get; set; }
		public List<Product> ShoppingCart { get; set; }
		public int Selection { get; set; }
		public OrderViewModel(DatabaseService db, Customer customer, Location location) {
			Customer = customer;
			Location = location;
			Inventory = db.Products.CountAtLocation(location);
			ShoppingCart = new List<Product>();
		}
	}
}
