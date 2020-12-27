using System;
using System.Data.Common;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using TylerMart.Storage.Contexts;
using TylerMart.Storage.Repositories;

namespace TylerMart.Testing.Services {
	internal class DatabaseFactory : IDesignTimeDbContextFactory<DatabaseContext>, IDisposable {
		private DbConnection Connection;
		/// <summary>
		/// Default constructor
		/// </summary>
		public DatabaseFactory() {
			Connection = new SqliteConnection("Filename=:memory:");
			Connection.Open();
		}
		/// <summary>
		/// Dispose of connection
		/// </summary>
		public void Dispose() => Connection.Dispose();
		/// <summary>
		/// Creates database context configured to use SQLite in-memory
		/// </summary>
		/// <param name="args">Argument list</param>
		/// <returns>
		/// Database context
		/// </returns>
		public DatabaseContext CreateDbContext(string[] args) {
			var options = new DbContextOptionsBuilder<DatabaseContext>()
				.UseSqlite(Connection)
				.Options;
			return new DatabaseContext(options);
		}
		/// <summary>
		/// Creates database context configured to use SQLite in-memory
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
