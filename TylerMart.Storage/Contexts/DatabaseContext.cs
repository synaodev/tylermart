using Microsoft.EntityFrameworkCore;

using TylerMart.Storage.Models;

namespace TylerMart.Storage.Contexts {
	public class DatabaseContext : DbContext {
		private DbSet<Customer> Customers;
		private DbSet<Order> Orders;
		private DbSet<Location> Locations;
		private DbSet<Inventory> Inventories;
		private DbSet<Product> Products;
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}
		public DatabaseContext() {}
	}
}
