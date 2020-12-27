using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TylerMart.Domain.Models;
using TylerMart.Storage.Contexts;

namespace TylerMart.Storage.Repositories {
	/// <summary>
	/// <see cref="TylerMart.Domain.Models.Order"/> Repository
	/// </summary>
	public class OrderRepository : Repository<Order> {
		/// <summary>
		/// Constructor that takes an instance of DatabaseContext
		/// </summary>
		/// <param name="db">Instance of DatabaseContext</param>
		public OrderRepository(DatabaseContext db) : base(db) {}
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
			return Db.Orders
				.Where(o => o.ID == ID)
				.Include(o => o.Customer)
				.Include(o => o.Location)
				.Single();
		}
		/// <summary>
		/// Find Orders by completion status
		/// </summary>
		/// <returns>
		/// List of Orders
		/// </returns>
		public List<Order> FindByCompleteness(bool complete = true) {
			return Db.Orders
				.Where(o => o.Complete == complete)
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
			return Db.Orders
				.Where(o => o.CustomerID == customer.ID)
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
			return Db.Orders
				.Where(o => o.LocationID == location.ID)
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
				.Where(op => op.ProductID == product.ID)
				.Include(op => op.Order)
				.Select(op => op.Order)
				.Distinct()
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
				OrderID = order.ID,
				ProductID = product.ID
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
				.SingleOrDefault(op => op.OrderID == order.ID && op.ProductID == product.ID);
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
					OrderID = order.ID,
					ProductID = p.ID
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
					.Where(op => op.OrderID == order.ID && op.ProductID == p.ID);
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
