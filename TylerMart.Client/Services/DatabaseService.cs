using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using TylerMart.Storage.Contexts;
using TylerMart.Storage.Repositories;

namespace TylerMart.Local.Services {
	internal class DatabaseFactory : IDesignTimeDbContextFactory<DatabaseContext> {
		/// <summary>
		/// Creates database context configured to use SQL Server
		/// </summary>
		/// <returns>
		/// Returns database context
		/// </returns>
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
	public class DatabaseService : RepositoryCollection {
		private DatabaseFactory Factory;
		public DatabaseService() {
			Factory = new DatabaseFactory();
			base.Initialize(Factory.CreateDbContext());
		}
	}
}
