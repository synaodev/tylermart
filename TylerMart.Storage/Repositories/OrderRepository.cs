using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TylerMart.Storage.Models;

namespace TylerMart.Storage.Repositories {
	public class OrderRepository : Repository<Order> {
		public OrderRepository(DbContext db) : base(db) {}
		public Order GetWithDetails(int ID) {
			if (!this.Exists(ID)) {
				return null;
			}
			return Db.Set<Order>()
				.Where(o => o.OrderID == ID)
				.Include(o => o.Customer)
				.Include(o => o.Location)
				.Single();
		}
		public List<Order> FindByIncomplete() {
			return Db.Set<Order>()
				.Where(o => !o.Completed)
				.ToList();
		}
		public List<Order> FindFromCustomer(Customer customer) {
			return Db.Set<Order>()
				.Where(o => o.CustomerID == customer.CustomerID)
				.ToList();
		}
		public List<Order> FindFromLocation(Location location) {
			return Db.Set<Order>()
				.Where(o => o.LocationID == location.LocationID)
				.ToList();
		}
		public List<Order> FindFromProduct(Product product) {
			return Db.Set<OrderProduct>()
				.Where(op => op.ProductID == product.ProductID)
				.Include(op => op.Order)
				.Select(op => op.Order)
				.ToList();
		}
	}
}
