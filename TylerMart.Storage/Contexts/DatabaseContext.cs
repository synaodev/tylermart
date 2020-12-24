using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TylerMart.Storage.Models;

namespace TylerMart.Storage.Contexts {
	public class DatabaseContext : DbContext {
		private DbSet<Customer> Customers;
		private DbSet<Order> Orders;
		private DbSet<Location> Locations;
		private DbSet<Product> Products;
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}
		public DatabaseContext() {}
		protected override void OnModelCreating(ModelBuilder builder) {

		}
		public override int SaveChanges() {
			var entries = ChangeTracker.Entries()
				.Where(e => e.Entity is Order && e.State == EntityState.Added);
			foreach (var e in entries) {
				((Order)e.Entity).PlacedAt = DateTime.Now;
			}
			return base.SaveChanges();
		}
	}
}
