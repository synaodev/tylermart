using System.Collections.Generic;
using Xunit;

using TylerMart.Domain.Models;
using TylerMart.Testing.Services;

namespace TylerMart.Testing {
	/// <summary>
	/// Tests for every repository
	/// </summary>
	public class RepositoryTests {
		/// <summary>
		/// Checks for seeded data in database
		/// </summary>
		[Fact]
		public void TestSeededDatabase() {
			DatabaseService db = new DatabaseService();

			List<Customer> customers = db.Customers.All();
			Assert.Equal(2, customers.Count);

			List<Product> products = db.Products.All();
			Assert.Equal(2, products.Count);

			List<Location> locations = db.Locations.All();
			Assert.Equal(2, locations.Count);

			List<Order> orders = db.Orders.All();
			Assert.Equal(2, orders.Count);
		}
	}
}
