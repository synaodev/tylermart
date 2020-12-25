using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TylerMart.Domain.Models;

namespace TylerMart.Storage.Repositories {
	public class ProductRepository : Repository<Product> {
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Product"/> Repository
		/// </summary>
		public ProductRepository(DbContext db) : base(db) {}
		/// <summary>
		/// Finds Products contained in an Order
		/// </summary>
		/// <param name="order">The Order</param>
		/// <returns>
		/// List of Products
		/// </returns>
		public List<Product> FindFromOrder(Order order) {
			return Db.Set<OrderProduct>()
				.Where(op => op.OrderID == order.OrderID)
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
				.Where(lp => lp.LocationID == location.LocationID)
				.Include(lp => lp.Product)
				.Select(lp => lp.Product)
				.ToList();
		}
	}
}
