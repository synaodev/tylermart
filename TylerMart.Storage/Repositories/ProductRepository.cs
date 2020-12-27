using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TylerMart.Domain.Models;

namespace TylerMart.Storage.Repositories {
	/// <summary>
	/// <see cref="TylerMart.Domain.Models.Product"/> Repository
	/// </summary>
	public class ProductRepository : Repository<Product> {
		/// <summary>
		/// Constructor that takes an instance of DbContext
		/// </summary>
		/// <param name="db">Instance of DbContext</param>
		public ProductRepository(DbContext db) : base(db) {}
		/// <summary>
		/// Finds Products
		/// </summary>
		/// <param name="name">Product name</param>
		/// <returns>
		/// List of Products
		/// </returns>
		public List<Product> FindFromName(string name) {
			return Db.Set<Product>()
				.Where(p => p.Name.Contains(name))
				.ToList();
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
	}
}
