using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using TylerMart.Storage.Contexts;
using TylerMart.Storage.Repositories;

namespace TylerMart.Testing.Services {
	internal class DatabaseFactory : IDesignTimeDbContextFactory<DatabaseContext> {
		public DatabaseContext CreateDbContext(string[] args) {
			var options = new DbContextOptionsBuilder<DatabaseContext>()
				.UseInMemoryDatabase("InMemoryDB")
				.Options;
			return new DatabaseContext(options);
		}
		public DatabaseContext CreateDbContext() {
			string[] args = new string[] {};
			return this.CreateDbContext(args);
		}
	}
	public class DatabaseService : RepositoryCollection {
		private DatabaseFactory Factory;
		public DatabaseService() {
			Factory = new DatabaseFactory();
			base.Initialize(Factory.CreateDbContext());
		}
	}
}
