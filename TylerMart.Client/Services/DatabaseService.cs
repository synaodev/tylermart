using TylerMart.Storage.Contexts;
using TylerMart.Storage.Repositories;

namespace TylerMart.Client.Services {
	public class DatabaseService : RepositoryCollection {
		public DatabaseService(DatabaseContext db) {
			base.Initialize(db);
		}
	}
}
