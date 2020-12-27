using System.Collections.Generic;
using Xunit;

using TylerMart.Domain.Models;
using TylerMart.Testing.Services;

namespace TylerMart.Testing {
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
	}
}
