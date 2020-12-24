using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using TylerMart.Storage.Contexts;
using TylerMart.Storage.Repositories;

namespace TylerMart.Local.Services {
	internal class DatabaseFactory : IDesignTimeDbContextFactory<DatabaseContext> {
		public DatabaseContext CreateDbContext(string[] args) {
			var options = new DbContextOptionsBuilder<DatabaseContext>()
				.UseSqlServer(@"server=localhost,1433;database=TylerMart;user id=sa;password=Password12345;")
				.Options;
			return new DatabaseContext(options);
		}
		public DatabaseContext CreateDbContext() {
			string[] args = new string[] {};
			return this.CreateDbContext(args);
		}
	}
	public class DatabaseService {
		private DatabaseFactory Factory;
		private DatabaseContext Context;
		public CustomerRepository Customers;
		public OrderRepository Orders;
		public LocationRepository Locations;
		public ProductRepository Products;
		public DatabaseService() {
			Factory = new DatabaseFactory();
			Context = Factory.CreateDbContext();
			Customers = new CustomerRepository(Context);
			Orders = new OrderRepository(Context);
			Locations = new LocationRepository(Context);
			Products = new ProductRepository(Context);
		}
	}
}
