using TylerMart.Storage.Contexts;

namespace TylerMart.Storage.Repositories {
	/// <summary>
	/// Interface for services that access the database
	/// </summary>
	public interface IRepositoryCollection {
		/// <summary>
		/// The <see cref="TylerMart.Storage.Repositories.CustomerRepository"/>
		/// </summary>
		CustomerRepository Customers { get; }
		/// <summary>
		/// The <see cref="TylerMart.Storage.Repositories.ProductRepository"/>
		/// </summary>
		ProductRepository Products { get; }
		/// <summary>
		/// The <see cref="TylerMart.Storage.Repositories.LocationRepository"/>
		/// </summary>
		LocationRepository Locations { get; }
		/// <summary>
		/// The <see cref="TylerMart.Storage.Repositories.OrderRepository"/>
		/// </summary>
		OrderRepository Orders { get; }
		/// <summary>
		/// Initialize database connection and all repositories
		/// </summary>
		/// <remarks>
		/// This should be called in the derived class constructor
		/// </remarks>
		/// <param name="db">Instance of DbContext</param>
		/// <param name="transient">Database provider is transient (set to 'true' when testing)</param>
		void Initialize(DatabaseContext db, bool transient = false);
	}
	/// <summary>
	/// Abstract class for services that access the database
	/// </summary>
	public abstract class RepositoryCollection : IRepositoryCollection {
		private DatabaseContext Db;
		/// <summary>
		/// The <see cref="TylerMart.Storage.Repositories.CustomerRepository"/>
		/// </summary>
		public CustomerRepository Customers { get; private set; }
		/// <summary>
		/// The <see cref="TylerMart.Storage.Repositories.ProductRepository"/>
		/// </summary>
		public ProductRepository Products { get; private set; }
		/// <summary>
		/// The <see cref="TylerMart.Storage.Repositories.LocationRepository"/>
		/// </summary>
		public LocationRepository Locations { get; private set; }
		/// <summary>
		/// The <see cref="TylerMart.Storage.Repositories.OrderRepository"/>
		/// </summary>
		public OrderRepository Orders { get; private set; }
		/// <summary>
		/// Initialize database connection and all repositories
		/// </summary>
		/// <remarks>
		/// This should be called in the derived class constructor
		/// </remarks>
		/// <param name="db">Instance of DbContext</param>
		/// <param name="unitTesting">Database is being used in a unit testing environment</param>
		public void Initialize(DatabaseContext db, bool unitTesting = false) {
			Db = db;
			if (unitTesting) {
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
