using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TylerMart.Storage.Models;

namespace TylerMart.Storage.Contexts {
	public class DatabaseContext : DbContext {
		private DbSet<Customer> Customers;
		private DbSet<Product> Products;
		private DbSet<Location> Locations;
		private DbSet<Order> Orders;
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}
		public DatabaseContext() {}
		protected override void OnModelCreating(ModelBuilder builder) {
			// Field Properties
			builder.Entity<Customer>().HasIndex(c => c.EmailAddress)
				.IsUnique();
			builder.Entity<Location>().HasIndex(l => l.Name)
				.IsUnique();
			// Clarify Relationships
			builder.Entity<LocationProduct>().HasOne(lp => lp.Location)
				.WithMany(l => l.LocationProducts)
				.HasForeignKey(lp => lp.LocationID);
			builder.Entity<LocationProduct>().HasOne(lp => lp.Product)
				.WithMany(p => p.LocationProducts)
				.HasForeignKey(lp => lp.ProductID);
			builder.Entity<OrderProduct>().HasOne(op => op.Order)
				.WithMany(o => o.OrderProducts)
				.HasForeignKey(op => op.OrderID);
			builder.Entity<OrderProduct>().HasOne(op => op.Product)
				.WithMany(p => p.OrderProducts)
				.HasForeignKey(op => op.ProductID);
			// Seed Data
			builder.Entity<Customer>().HasData(Customer.GenerateSeededData());
			builder.Entity<Product>().HasData(Product.GenerateSeededData());
			builder.Entity<Location>().HasData(Location.GenerateSeededData());
			builder.Entity<Order>().HasData(Order.GenerateSeededData());
			builder.Entity<LocationProduct>().HasData(LocationProduct.GenerateSeededData());
			builder.Entity<OrderProduct>().HasData(OrderProduct.GenerateSeededData());
		}
		/// <summary>
		/// Sets timestamp to Order when first committed to the database
		/// </summary>
		/// <returns>
		/// Returns number of saved changes to the database
		/// </returns>
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
