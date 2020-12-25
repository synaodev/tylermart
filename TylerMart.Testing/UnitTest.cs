using System.Collections.Generic;
using Xunit;

using TylerMart.Domain.Models;
using TylerMart.Testing.Services;

namespace TylerMart.Testing {
	/// <summary>
	/// Default Test Class
	/// </summary>
	public class UnitTest {
		/// <summary>
		/// A Test
		/// </summary>
		[Fact]
		public void TestSeededCustomers() {
			DatabaseService Db = new DatabaseService();
			List<Customer> customers = Db.Customers.All();
			Assert.Equal(2, customers.Count);
		}
	}
}
