using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TylerMart.Storage.Models;

namespace TylerMart.Storage.Repositories {
	public class OrderRepository : Repository<Order> {
		public OrderRepository(DbContext db) : base(db) {}
		/// <summary>
		/// Get one order using key, eagerly load customer + location
		/// </summary>
		/// <returns>
		/// Returns single row or null
		/// </returns>
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
		/// <summary>
		/// Get list of incomplete orders
		/// </summary>
		/// <returns>
		/// Returns list of orders
		/// </returns>
		public List<Order> FindByIncomplete() {
			return Db.Set<Order>()
				.Where(o => !o.Completed)
				.ToList();
		}
		/// <summary>
		/// Get list of orders made by customer
		/// </summary>
		/// <returns>
		/// Returns list of orders
		/// </returns>
		public List<Order> FindFromCustomer(Customer customer) {
			return Db.Set<Order>()
				.Where(o => o.CustomerID == customer.CustomerID)
				.ToList();
		}
		/// <summary>
		/// Get list of orders made to location
		/// </summary>
		/// <returns>
		/// Returns list of orders
		/// </returns>
		public List<Order> FindFromLocation(Location location) {
			return Db.Set<Order>()
				.Where(o => o.LocationID == location.LocationID)
				.ToList();
		}
		/// <summary>
		/// Get list of orders that have a particular product
		/// </summary>
		/// <returns>
		/// Returns list of orders
		/// </returns>
		public List<Order> FindFromProduct(Product product) {
			return Db.Set<OrderProduct>()
				.Where(op => op.ProductID == product.ProductID)
				.Include(op => op.Order)
				.Select(op => op.Order)
				.ToList();
		}
		/// <summary>
		/// Adds product to order
		/// </summary>
		/// <returns>
		/// Returns 'true' if successfully inserted into database
		/// </returns>
		public bool AddProduct(Order order, Product product) {
			OrderProduct op = new OrderProduct() {
				OrderID = order.OrderID,
				ProductID = product.ProductID
			};
			Db.Set<OrderProduct>().Add(op);
			return Db.SaveChanges() >= 1;
		}
		/// <summary>
		/// Removes product from order
		/// </summary>
		/// <returns>
		/// Returns 'true' if successfully removed from database
		/// </returns>
		public bool RemoveProduct(Order order, Product product) {
			OrderProduct q = Db.Set<OrderProduct>()
				.LastOrDefault(op => op.OrderID == order.OrderID && op.ProductID == product.ProductID);
			if (q != null) {
				Db.Set<OrderProduct>().Remove(q);
				return Db.SaveChanges() >= 1;
			}
			return false;
		}
		/// <summary>
		/// Adds list of products to order
		/// </summary>
		/// <returns>
		/// Returns 'true' if successfully inserted into database
		/// </returns>
		public bool AddProducts(Order order, List<Product> products) {
			List<OrderProduct> range = products.ConvertAll<OrderProduct>(p =>
				new OrderProduct() {
					OrderID = order.OrderID,
					ProductID = p.ProductID
				}
			);
			Db.Set<OrderProduct>().AddRange(range);
			return Db.SaveChanges() >= 1;
		}
		/// <summary>
		/// Removes list of products from order
		/// </summary>
		/// <returns>
		/// Returns 'true' if successfully removed from database
		/// </returns>
		public bool RemoveProducts(Order order, List<Product> products) {
			List<OrderProduct> range = new List<OrderProduct>();
			foreach (var p in products) {
				var ops = Db.Set<OrderProduct>()
					.Where(op => op.OrderID == order.OrderID && op.ProductID == p.ProductID);
				range.AddRange(ops);
			}
			if (range.Count > 0) {
				Db.Set<OrderProduct>().RemoveRange(range);
				return Db.SaveChanges() >= 1;
			}
			return false;
		}
	}
}
