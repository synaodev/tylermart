using Microsoft.EntityFrameworkCore;

namespace TylerMart.Storage.Repositories {
	public abstract class RepositoryCollection {
		private DbContext Db;
		public CustomerRepository Customers;
		public ProductRepository Products;
		public LocationRepository Locations;
		public OrderRepository Orders;
		public void Initialize(DbContext db) {
			Db = db;
			Customers = new CustomerRepository(Db);
			Products = new ProductRepository(Db);
			Locations = new LocationRepository(Db);
			Orders = new OrderRepository(Db);
		}
	}
}
