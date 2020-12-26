using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TylerMart.Domain.Models;

namespace TylerMart.Storage.Repositories {
	/// <summary>
	/// <see cref="TylerMart.Domain.Models.Order"/> Repository
	/// </summary>
	public class OrderRepository : Repository<Order> {
		/// <summary>
		/// Constructor that takes an instance of DbContext
		/// </summary>
		/// <param name="db">Instance of DbContext</param>
		public OrderRepository(DbContext db) : base(db) {}
		/// <summary>
		/// Get Order from primary key
		/// </summary>
		/// <remarks>
		/// Eagerly loads the Customer and Location
		/// </remarks>
		/// <param name="ID">Primary Key</param>
		/// <returns>
		/// Single Order or null
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
		/// Find Orders that are incomplete
		/// </summary>
		/// <returns>
		/// List of Orders
		/// </returns>
		public List<Order> FindByIncomplete() {
			return Db.Set<Order>()
				.Where(o => !o.Complete)
				.ToList();
		}
		/// <summary>
		/// Find Orders made by a Customer
		/// </summary>
		/// <param name="customer">The Customer</param>
		/// <returns>
		/// List of Orders
		/// </returns>
		public List<Order> FindFromCustomer(Customer customer) {
			return Db.Set<Order>()
				.Where(o => o.CustomerID == customer.CustomerID)
				.ToList();
		}
		/// <summary>
		/// Finds Orders made to a Location
		/// </summary>
		/// <param name="location">The Location</param>
		/// <returns>
		/// List of Orders
		/// </returns>
		public List<Order> FindFromLocation(Location location) {
			return Db.Set<Order>()
				.Where(o => o.LocationID == location.LocationID)
				.ToList();
		}
		/// <summary>
		/// Finds Orders containing a particular Product
		/// </summary>
		/// <param name="product">The Product</param>
		/// <returns>
		/// List of Orders
		/// </returns>
		public List<Order> FindFromProduct(Product product) {
			return Db.Set<OrderProduct>()
				.Where(op => op.ProductID == product.ProductID)
				.Include(op => op.Order)
				.Select(op => op.Order)
				.ToList();
		}
		/// <summary>
		/// Adds a Product to an Order
		/// </summary>
		/// <param name="order">The Order</param>
		/// <param name="product">The Product</param>
		/// <returns>
		/// 'true' if successfully inserted into database
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
		/// Remove a Product from an Order
		/// </summary>
		/// <param name="order">The Order</param>
		/// <param name="product">The Product</param>
		/// <returns>
		/// 'true' if successfully removed from database
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
		/// Adds a range of Products to an Order
		/// </summary>
		/// <param name="order">The Order</param>
		/// <param name="products">List of Products</param>
		/// <returns>
		/// 'true' if successfully inserted into database
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
		/// Removes a range of Products from an Order
		/// </summary>
		/// <param name="order">The Order</param>
		/// <param name="products">List of Products</param>
		/// <returns>
		/// 'true' if successfully removed from database
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
