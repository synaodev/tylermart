using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TylerMart.Storage.Models;

namespace TylerMart.Storage.Repositories {
	public class ProductRepository : Repository<Product> {
		public ProductRepository(DbContext db) : base(db) {}
		public List<Product> FindFromOrder(Order order) {
			return Db.Set<OrderProduct>()
				.Where(op => op.OrderID == order.OrderID)
				.Include(op => op.Product)
				.Select(op => op.Product)
				.ToList();
		}
		public List<Product> FindFromLocation(Location location) {
			return Db.Set<LocationProduct>()
				.Where(lp => lp.LocationID == location.LocationID)
				.Include(lp => lp.Product)
				.Select(lp => lp.Product)
				.ToList();
		}
	}
}
