using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TylerMart.Domain.Models;
using TylerMart.Storage.Contexts;

namespace TylerMart.Storage.Repositories {
	/// <summary>
	/// <see cref="TylerMart.Domain.Models.Product"/> Repository
	/// </summary>
	public class ProductRepository : Repository<Product> {
		/// <summary>
		/// Constructor that takes an instance of DatabaseContext
		/// </summary>
		/// <param name="db">Instance of DatabaseContext</param>
		public ProductRepository(DatabaseContext db) : base(db) {}
		/// <summary>
		/// Gets Products from name
		/// </summary>
		/// <param name="name">Product name</param>
		/// <returns>
		/// List of Products
		/// </returns>
		public Product GetFromName(string name) {
			return Db.Products
				.SingleOrDefault(p => p.Name == name);
		}
		/// <summary>
		/// Finds Products contained in an Order
		/// </summary>
		/// <param name="order">The Order</param>
		/// <returns>
		/// List of Products
		/// </returns>
		public List<Product> FindFromOrder(Order order) {
			return Db.Set<OrderProduct>()
				.Where(op => op.OrderID == order.ID)
				.Include(op => op.Product)
				.Select(op => op.Product)
				.ToList();
		}
		/// <summary>
		/// Finds Products in stock at a Location
		/// </summary>
		/// <param name="location">The Location</param>
		/// <returns>
		/// List of Products
		/// </returns>
		public List<Product> FindFromLocation(Location location) {
			return Db.Set<LocationProduct>()
				.Where(lp => lp.LocationID == location.ID)
				.Include(lp => lp.Product)
				.Select(lp => lp.Product)
				.ToList();
		}
		/// <summary>
		/// Finds the total for every different Product at a Location
		/// </summary>
		/// <param name="location">The Location</param>
		/// <returns>
		/// Dictionary of Products and their stock
		/// </returns>
		public Dictionary<Product, int> CountAtLocation(Location location) {
			return Db.Set<LocationProduct>()
				.Where(lp => lp.LocationID == location.ID)
				.Include(lp => lp.Product)
				.Select(lp => lp.Product)
				.ToList()
				.GroupBy(p => p)
				.Select(g => new { Product = g.Key, Total = g.Count() })
				.ToDictionary(kv => kv.Product, kv => kv.Total);
		}
	}
}
