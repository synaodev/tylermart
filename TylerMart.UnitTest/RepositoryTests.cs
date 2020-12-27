using System.Collections.Generic;
using Xunit;

using TylerMart.Domain.Models;
using TylerMart.UnitTest.Services;

namespace TylerMart.UnitTest {
	/// <summary>
	/// Tests for <see cref="TylerMart.Storage.Repositories.Repository{T}"/>
	/// </summary>
	/// <remarks>
	/// Derived classes are also tested
	/// </remarks>
	public class RepositoryTests {
		/// <summary>
		/// Generic CRUD
		/// </summary>
		[Fact]
		public void TestGenericCRUD() {
			DatabaseService db = new DatabaseService();

			bool creationSuccessful = db.Customers.Create(new Customer() {
				FirstName = "Johnny",
				LastName = "Bravo",
				EmailAddress = "johnny.bravo@revature.net",
				Password = "johnnybravo"
			});
			Assert.True(creationSuccessful);

			Customer johnny1 = db.Customers.Get(3);
			Assert.Equal("johnny.bravo@revature.net", johnny1.EmailAddress);

			johnny1.EmailAddress = "johnny.bravo@revature.com";
			bool updateSuccessful = db.Customers.Update(johnny1);
			Assert.True(updateSuccessful);

			Customer johnny2 = db.Customers.Get(3);
			Assert.Equal("johnny.bravo@revature.com", johnny2.EmailAddress);

			bool deleteSuccessful = db.Customers.Delete(johnny2);
			Assert.True(deleteSuccessful);

			Customer johnny3 = db.Customers.Get(3);
			Assert.Null(johnny3);
		}
		/// <summary>
		/// For <see cref="TylerMart.Storage.Repositories.CustomerRepository"/>
		/// </summary>
		[Fact]
		public void TestCustomerRepository() {
			DatabaseService db = new DatabaseService();

			Customer tyler = db.Customers.GetByEmailAddress("tyler.cadena@revature.net");
			Assert.NotNull(tyler);
			Assert.Equal("tylercadena", tyler.Password);

			db.Customers.Create(new Customer() {
				FirstName = "Tyler",
				LastName = "Cadena",
				EmailAddress = "tyler.cadena@revature.com",
				Password = "tylercadena"
			});

			List<Customer> tylerList = db.Customers.FindByFirstName("Tyler");
			Assert.Equal(2, tylerList.Count);

			List<Customer> cadenaList = db.Customers.FindByLastName("Cadena");
			Assert.Equal(2, cadenaList.Count);

			List<Customer> tylerCadenaList = db.Customers.FindByWholeName("Tyler", "Cadena");
			Assert.Equal(2, tylerCadenaList.Count);
		}
		/// <summary>
		/// For <see cref="TylerMart.Storage.Repositories.ProductRepository"/>
		/// </summary>
		[Fact]
		public void TestProductRepository() {
			DatabaseService db = new DatabaseService();

			List<Product> bags = db.Products.FindFromName("Bag");
			Assert.NotEmpty(bags);
		}
		/// <summary>
		/// For <see cref="TylerMart.Storage.Repositories.LocationRepository"/>
		/// </summary>
		[Fact]
		public void TestLocationRepository() {
			DatabaseService db = new DatabaseService();

			Location jersey = db.Locations.GetByName("New Jersey");
			Assert.NotNull(jersey);
			Assert.Equal("New Jersey", jersey.Name);

			Product product = db.Products.Get(1);
			List<Location> locations = db.Locations.FindFromProduct(product);

			Assert.Contains(jersey, locations);
		}
		/// <summary>
		/// For <see cref="TylerMart.Storage.Repositories.OrderRepository"/>
		/// </summary>
		[Fact]
		public void TestOrderRepository() {
			DatabaseService db = new DatabaseService();

			List<Order> incompleteOrders = db.Orders.FindByCompleteness(false);
			Assert.Empty(incompleteOrders);

			Customer tyler = db.Customers.GetByEmailAddress("tyler.cadena@revature.net");
			List<Order> tylerOrders = db.Orders.FindFromCustomer(tyler);
			Assert.NotEmpty(tylerOrders);

			Location jersey = db.Locations.GetByName("New Jersey");
			List<Order> jerseyOrders = db.Orders.FindFromLocation(jersey);
			Assert.NotEmpty(jerseyOrders);
		}
		/// <summary>
		/// For <see cref="TylerMart.Storage.Repositories.LocationRepository"/>
		/// and <see cref="TylerMart.Storage.Repositories.ProductRepository"/>
		/// on many-to-many relationships
		/// </summary>
		[Fact]
		public void TestLocationsAndProducts() {
			DatabaseService db = new DatabaseService();

			List<Product> bagProducts = db.Products.FindFromName("Bag");
			List<Location> bagLocations = db.Locations.FindFromProduct(bagProducts[0]);
			Assert.NotEmpty(bagLocations);

			Location jersey = db.Locations.GetByName("New Jersey");
			Location florida = db.Locations.GetByName("Florida");

			db.Products.Create(new Product() {
				Name = "Bananas",
				Description = "Bananas",
				Price = 20.0M
			});
			List<Product> bananaProducts = db.Products.FindFromName("Bananas");
			Product bananas = bananaProducts[0];

			bool successfulAddedJerseyBananas = db.Locations.AddProduct(jersey, bananas);
			Assert.True(successfulAddedJerseyBananas);

			List<Location> jerseyList = db.Locations.FindFromProduct(bananas);
			Assert.Contains(jersey, jerseyList);

			bool successfulRemovedJerseyBananas = db.Locations.RemoveProduct(jersey, bananas);
			Assert.True(successfulRemovedJerseyBananas);

			db.Products.Create(new Product() {
				Name = "Apples",
				Description = "Apples",
				Price = 15.0M
			});
			List<Product> appleProducts = db.Products.FindFromName("Apples");
			Product apples = appleProducts[0];

			bool successfulAddedFloridaFruits = db.Locations.AddProducts(
				florida, new List<Product>() { bananas, apples }
			);
			Assert.True(successfulAddedFloridaFruits);

			List<Location> floridaListApples = db.Locations.FindFromProduct(apples);
			Assert.Contains(florida, floridaListApples);
			List<Location> floridaListBananas = db.Locations.FindFromProduct(bananas);
			Assert.Contains(florida, floridaListBananas);

			bool successfulRemovedFloridaFruits = db.Locations.RemoveProducts(
				florida, new List<Product>() { bananas, apples }
			);
			Assert.True(successfulRemovedFloridaFruits);
		}
		/// <summary>
		/// For <see cref="TylerMart.Storage.Repositories.OrderRepository"/>
		/// and <see cref="TylerMart.Storage.Repositories.ProductRepository"/>
		/// on many-to-many relationships
		/// </summary>
		[Fact]
		public void TestOrdersAndProducts() {
			DatabaseService db = new DatabaseService();

			List<Product> products = db.Products.FindFromName("Bag");
			List<Order> bagOrders = db.Orders.FindFromProduct(products[0]);
			Assert.NotEmpty(bagOrders);

			List<Order> orders = db.Orders.All();
			Order o1 = orders[0];
			Order o2 = orders[1];

			db.Products.Create(new Product() {
				Name = "Bananas",
				Description = "Bananas",
				Price = 20.0M
			});
			List<Product> bananaProducts = db.Products.FindFromName("Bananas");
			Product bananas = bananaProducts[0];

			bool successfulAddedO1Banana = db.Orders.AddProduct(o1, bananas);
			Assert.True(successfulAddedO1Banana);

			List<Order> o1List = db.Orders.FindFromProduct(bananas);
			Assert.Contains(o1, o1List);

			bool successfulRemovedO1Banana = db.Orders.RemoveProduct(o1, bananas);
			Assert.True(successfulRemovedO1Banana);

			db.Products.Create(new Product() {
				Name = "Apples",
				Description = "Apples",
				Price = 15.0M
			});
			List<Product> appleProducts = db.Products.FindFromName("Apples");
			Product apples = appleProducts[0];

			bool successfulAddedO2Fruits = db.Orders.AddProducts(
				o2, new List<Product>() { apples, bananas }
			);
			Assert.True(successfulAddedO2Fruits);

			List<Order> o2ListApples = db.Orders.FindFromProduct(apples);
			Assert.Contains(o2, o2ListApples);
			List<Order> o2ListBananas = db.Orders.FindFromProduct(bananas);
			Assert.Contains(o2, o2ListApples);

			bool successfulRemovedO2Fruits = db.Orders.RemoveProducts(
				o2, new List<Product>() { apples, bananas }
			);
			Assert.True(successfulRemovedO2Fruits);
		}
	}
}
