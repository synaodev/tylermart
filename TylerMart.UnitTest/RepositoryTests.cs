using System;
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
				FirstName = "Tyler",
				LastName = "Cadena",
				EmailAddress = "tyler.cadena@revature.net",
				Password = "tylercadena"
			});
			Assert.True(creationSuccessful);

			int count = db.Customers.All().Count;

			Customer tyler1 = db.Customers.Get(count);
			Assert.Equal("tyler.cadena@revature.net", tyler1.EmailAddress);

			tyler1.EmailAddress = "tyler.cadena@revature.com";
			bool updateSuccessful = db.Customers.Update(tyler1);
			Assert.True(updateSuccessful);

			Customer tyler2 = db.Customers.Get(count);
			Assert.Equal("tyler.cadena@revature.com", tyler2.EmailAddress);

			bool deleteSuccessful = db.Customers.Delete(tyler2);
			Assert.True(deleteSuccessful);

			Customer tyler3 = db.Customers.Get(count);
			Assert.Null(tyler3);
		}
		/// <summary>
		/// For <see cref="TylerMart.Storage.Repositories.CustomerRepository"/>
		/// </summary>
		[Fact]
		public void TestCustomerRepository() {
			DatabaseService db = new DatabaseService();

			db.Customers.Create(new Customer() {
				FirstName = "Tyler",
				LastName = "Cadena",
				EmailAddress = "tyler.cadena@revature.net",
				Password = "tylercadena"
			});

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

			db.Products.Create(new Product() {
				Name = "Thingamajig",
				Description = "Blah blah blah",
				Price = 30.0M
			});

			Product thing = db.Products.GetFromName("Thingamajig");
			Assert.NotNull(thing);
			Assert.Equal("Thingamajig", thing.Name);
		}
		/// <summary>
		/// For <see cref="TylerMart.Storage.Repositories.LocationRepository"/>
		/// </summary>
		[Fact]
		public void TestLocationRepository() {
			DatabaseService db = new DatabaseService();

			Location dreamland = db.Locations.GetByName("Dreamland");
			Assert.NotNull(dreamland);
			Assert.Equal("Dreamland", dreamland.Name);

			Product nightmare = db.Products.GetFromName("Nightmare");
			Assert.NotNull(nightmare);

			List<Location> locations = db.Locations.FindFromProduct(nightmare);
			Assert.NotEmpty(locations);
			Assert.Contains(dreamland, locations);
		}
		/// <summary>
		/// For <see cref="TylerMart.Storage.Repositories.OrderRepository"/>
		/// </summary>
		[Fact]
		public void TestOrderRepository() {
			DatabaseService db = new DatabaseService();

			List<Order> incompleteOrders = db.Orders.FindByCompleteness(false);
			Assert.Empty(incompleteOrders);

			Customer admin = db.Customers.GetByEmailAddress("admin.admin@revature.net");
			List<Order> adminOrders = db.Orders.FindFromCustomer(admin);
			Assert.NotEmpty(adminOrders);

			Location dreamland = db.Locations.GetByName("Dreamland");
			List<Order> dreamlandOrders = db.Orders.FindFromLocation(dreamland);
			Assert.NotEmpty(dreamlandOrders);
			Assert.Equal(adminOrders[0], dreamlandOrders[0]);

			DateTime now = DateTime.Now;
			db.Orders.Create(new Order() {
				Complete = false,
				CreatedAt = now,
				CustomerID = admin.ID,
				LocationID = dreamland.ID
			});
			Order lastOrder = db.Orders.GetByTimestamp(now);
			Assert.NotNull(lastOrder);
			Assert.Equal(lastOrder.CustomerID, admin.ID);
			Assert.Equal(lastOrder.LocationID, dreamland.ID);
		}
		/// <summary>
		/// For <see cref="TylerMart.Storage.Repositories.LocationRepository"/>
		/// and <see cref="TylerMart.Storage.Repositories.ProductRepository"/>
		/// on many-to-many relationships
		/// </summary>
		[Fact]
		public void TestLocationsAndProducts() {
			DatabaseService db = new DatabaseService();

			db.Locations.Create(new Location() { Name = "New Jersey" });
			db.Locations.Create(new Location() { Name = "Florida" });
			db.Products.Create(new Product() {
				Name = "Bananas",
				Description = "Bananas",
				Price = 20.0M
			});
			db.Products.Create(new Product() {
				Name = "Apples",
				Description = "Apples",
				Price = 15.0M
			});

			Location jersey = db.Locations.GetByName("New Jersey");
			Location florida = db.Locations.GetByName("Florida");
			Product bananas = db.Products.GetFromName("Bananas");
			Product apples = db.Products.GetFromName("Apples");

			bool successfulAddedJerseyBananas = db.Locations.AddProduct(jersey, bananas);
			Assert.True(successfulAddedJerseyBananas);

			List<Location> jerseyList = db.Locations.FindFromProduct(bananas);
			Assert.Contains(jersey, jerseyList);

			bool successfulRemovedJerseyBananas = db.Locations.RemoveProduct(jersey, bananas);
			Assert.True(successfulRemovedJerseyBananas);

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

			Customer admin = db.Customers.GetByEmailAddress("admin.admin@revature.net");
			Location dreamland = db.Locations.GetByName("Dreamland");

			db.Orders.Create(new Order() {
				Complete = false,
				CreatedAt = DateTime.Now,
				CustomerID = admin.ID,
				LocationID = dreamland.ID
			});
			db.Orders.Create(new Order() {
				Complete = false,
				CreatedAt = DateTime.Now,
				CustomerID = admin.ID,
				LocationID = dreamland.ID
			});
			db.Products.Create(new Product() {
				Name = "Bananas",
				Description = "Bananas",
				Price = 20.0M
			});
			db.Products.Create(new Product() {
				Name = "Apples",
				Description = "Apples",
				Price = 15.0M
			});

			List<Order> orders = db.Orders.FindByCompleteness(false);
			Order o1 = orders[0];
			Order o2 = orders[1];
			Product bananas = db.Products.GetFromName("Bananas");
			Product apples = db.Products.GetFromName("Apples");

			bool successfulAddedO1Banana = db.Orders.AddProduct(o1, bananas);
			Assert.True(successfulAddedO1Banana);

			List<Order> o1List = db.Orders.FindFromProduct(bananas);
			Assert.Contains(o1, o1List);

			bool successfulRemovedO1Banana = db.Orders.RemoveProduct(o1, bananas);
			Assert.True(successfulRemovedO1Banana);

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
		/// <summary>
		/// Specifically for <see cref="TylerMart.Storage.Repositories.ProductRepository.CountAtLocation(Location)"/>
		/// because it's the most complicated query in the entire codebase
		/// </summary>
		[Fact]
		public void TestProductRepositoryCountAtLocation() {
			DatabaseService db = new DatabaseService();

			db.Products.Create(new Product() {
				Name = "P0",
				Description = "-----",
				Price = 1.0M
			});
			db.Products.Create(new Product() {
				Name = "P1",
				Description = "-----",
				Price = 1.0M
			});
			db.Products.Create(new Product() {
				Name = "P2",
				Description = "-----",
				Price = 1.0M
			});
			List<Product> products = new List<Product>() {
				db.Products.GetFromName("P0"),
				db.Products.GetFromName("P1"),
				db.Products.GetFromName("P2")
			};
			products.ForEach(p => Assert.NotNull(p));

			Location dreamland = db.Locations.GetByName("Dreamland");
			db.Locations.AddProducts(dreamland, products);
			db.Locations.AddProduct(dreamland, products[1]);
			db.Locations.AddProducts(dreamland, new List<Product>() {
				products[2],
				products[2]
			});

			Dictionary<Product, int> results = db.Products.CountAtLocation(dreamland);
			Assert.Equal(1, results[products[0]]);
			Assert.Equal(2, results[products[1]]);
			Assert.Equal(3, results[products[2]]);
		}
	}
}
