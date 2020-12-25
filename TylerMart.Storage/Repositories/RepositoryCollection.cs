using Microsoft.EntityFrameworkCore;

namespace TylerMart.Storage.Repositories {
	/// <summary>
	/// Abstract class for services that access the database
	/// </summary>
	public abstract class RepositoryCollection {
		private DbContext Db;
		/// <summary>
		/// The <see cref="TylerMart.Storage.Repositories.CustomerRepository"/>
		/// </summary>
		public CustomerRepository Customers;
		/// <summary>
		/// The <see cref="TylerMart.Storage.Repositories.ProductRepository"/>
		/// </summary>
		public ProductRepository Products;
		/// <summary>
		/// The <see cref="TylerMart.Storage.Repositories.LocationRepository"/>
		/// </summary>
		public LocationRepository Locations;
		/// <summary>
		/// The <see cref="TylerMart.Storage.Repositories.OrderRepository"/>
		/// </summary>
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
