using Microsoft.EntityFrameworkCore;

namespace TylerMart.Storage.Repositories {
	/// <summary>
	/// Abstract class for services that access the database
	/// </summary>
	public abstract class RepositoryCollection {
		private DbContext Db;
		public CustomerRepository Customers;
		public ProductRepository Products;
		public LocationRepository Locations;
		public OrderRepository Orders;
		/// <summary>
		/// Initialize database connection and all repositories
		/// </summary>
		/// <remarks>
		/// This should be called in the derived class constructor
		/// </remarks>
		/// <param name="db">Instance of DbContext</param>
		/// <param name="transient">Database provider is transient (set to 'true' when testing)</param>
		public void Initialize(DbContext db, bool transient = false) {
			Db = db;
			if (transient) {
				Db.Database.EnsureDeleted();
				Db.Database.EnsureCreated();
			}
			Customers = new CustomerRepository(Db);
			Products = new ProductRepository(Db);
			Locations = new LocationRepository(Db);
			Orders = new OrderRepository(Db);
		}
	}
}
