using System.Collections.Generic;
using Xunit;

using TylerMart.Domain.Models;
using TylerMart.UnitTest.Services;

namespace TylerMart.UnitTest {
	/// <summary>
	/// Tests for <see cref="TylerMart.Storage.Contexts.DatabaseContext"/>
	/// </summary>
	public class ContextTests {
		/// <summary>
		/// Checks for seeded data in database
		/// </summary>
		[Fact]
		public void TestSeededData() {
			DatabaseService db = new DatabaseService();

			List<Customer> customerList = db.Customers.All();
			bool customerExists = db.Customers.Exists(1);
			Customer customer = db.Customers.Get(1);

			Assert.NotEmpty(customerList);
			Assert.True(customerExists);
			Assert.NotNull(customer);

			List<Product> productList = db.Products.All();
			bool productExists = db.Products.Exists(1);
			Product product = db.Products.Get(1);

			Assert.NotEmpty(productList);
			Assert.True(productExists);
			Assert.NotNull(product);

			List<Location> locationList = db.Locations.All();
			bool locationExists = db.Locations.Exists(1);
			Location location = db.Locations.Get(1);

			Assert.NotEmpty(locationList);
			Assert.True(locationExists);
			Assert.NotNull(location);

			List<Order> orderList = db.Orders.All();
			bool orderExists = db.Orders.Exists(1);
			Order order = db.Orders.Get(1);

			Assert.NotEmpty(orderList);
			Assert.True(orderExists);
			Assert.NotNull(order);
		}
		/// <summary>
		/// Checks for seeded many-to-many relationships in database
		/// </summary>
		[Fact]
		public void TestSeededManyToManyData() {
			DatabaseService db = new DatabaseService();

			Location location = db.Locations.Get(1);
			List<Product> productsAtLocation = db.Products.FindFromLocation(location);
			Assert.NotEmpty(productsAtLocation);

			Order order = db.Orders.Get(1);
			List<Product> productsInOrder = db.Products.FindFromOrder(order);
			Assert.NotEmpty(productsInOrder);
		}
		/// <summary>
		/// Checks to ensure unique fields are actually unique
		/// </summary>
		[Fact]
		public void TestUniqueFields() {
			DatabaseService db = new DatabaseService();

			bool failure1 = db.Customers.Create(new Customer() {
				FirstName = "Tyler",
				LastName = "Cadena",
				EmailAddress = "admin.admin@revature.net",
				Password = "tylercadena"
			});
			Assert.False(failure1);

			bool success1 = db.Customers.Create(new Customer() {
				FirstName = "Tyler",
				LastName = "Cadena",
				EmailAddress = "tyler.cadena@revature.net",
				Password = "tylercadena"
			});
			Assert.True(success1);

			Customer tyler = db.Customers.Get(2);
			tyler.EmailAddress = "admin.admin@revature.net";
			bool failure2 = db.Customers.Update(tyler);
			Assert.False(failure2);

			bool failure3 = db.Locations.Create(new Location() {
				Name = "Dreamland"
			});
			Assert.False(failure3);

			bool success2 = db.Locations.Create(new Location() {
				Name = "Nightmareland"
			});
			Assert.True(success2);

			Location nightmareland = db.Locations.Get(2);
			nightmareland.Name = "Dreamland";
			bool failure4 = db.Locations.Update(nightmareland);
			Assert.False(failure4);

			bool failure5 = db.Products.Create(new Product() {
				Name = "Nightmare",
				Description = "Boo, it's scary",
				Price = 786.0M
			});
			Assert.False(failure5);

			bool success3 = db.Products.Create(new Product() {
				Name = "Dream",
				Description = "Yay, it's not scary",
				Price = 231.02M
			});
			Assert.True(success3);

			Product dream = db.Products.Get(2);
			dream.Name = "Nightmare";
			bool failure6 = db.Products.Update(dream);
			Assert.False(failure6);
		}
	}
}
