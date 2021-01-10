using TylerMart.Storage.Contexts;
using TylerMart.Storage.Repositories;

namespace TylerMart.Client.Services {
	/// <summary>
	/// Service that allows for easy database access
	/// </summary>
	public class DatabaseService : RepositoryCollection {
		/// <summary>
		/// Constructor that takes instance of database context
		/// </summary>
		/// <param name="db">Injected instance of database context</param>
		public DatabaseService(DatabaseContext db) {
			base.Initialize(db);
		}
	}
}
