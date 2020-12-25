using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using TylerMart.Storage.Contexts;
using TylerMart.Storage.Repositories;

namespace TylerMart.Testing.Services {
	internal class DatabaseFactory : IDesignTimeDbContextFactory<DatabaseContext> {
		/// <summary>
		/// Creates database context configured to use transient in-memory database
		/// </summary>
		/// <param name="args">Argument list</param>
		/// <returns>
		/// Database context
		/// </returns>
		public DatabaseContext CreateDbContext(string[] args) {
			var options = new DbContextOptionsBuilder<DatabaseContext>()
				.UseInMemoryDatabase("InMemoryDB")
				.Options;
			return new DatabaseContext(options);
		}
		/// <summary>
		/// Creates database context configured to use transient in-memory database
		/// </summary>
		/// <returns>
		/// Database context
		/// </returns>
		public DatabaseContext CreateDbContext() {
			string[] args = new string[] {};
			return this.CreateDbContext(args);
		}
	}
	/// <summary>
	/// Service that allows for easy database access
	/// </summary>
	public class DatabaseService : RepositoryCollection {
		private DatabaseFactory Factory;
		/// <summary>
		/// Default constructor
		/// </summary>
		public DatabaseService() {
			Factory = new DatabaseFactory();
			base.Initialize(
				db: Factory.CreateDbContext(),
				transient: true
			);
		}
	}
}
