using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using TylerMart.Storage.Contexts;
using TylerMart.Storage.Repositories;

namespace TylerMart.Client.Services {
	internal class DatabaseFactory : IDesignTimeDbContextFactory<DatabaseContext> {
		/// <summary>
		/// Creates database context configured to use SQL Server
		/// </summary>
		/// <param name="args">Argument list</param>
		/// <returns>
		/// Database context
		/// </returns>
		public DatabaseContext CreateDbContext(string[] args) {
			var options = new DbContextOptionsBuilder<DatabaseContext>()
				.UseSqlServer(@"server=localhost,1433;database=TylerMart;user id=sa;password=Password12345;")
				.Options;
			return new DatabaseContext(options);
		}
		/// <summary>
		/// Creates database context configured to use SQL Server
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
		public DatabaseService() {
			Factory = new DatabaseFactory();
			base.Initialize(Factory.CreateDbContext());
		}
	}
}
