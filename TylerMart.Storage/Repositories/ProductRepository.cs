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
		/// Gets list of Products from a list of primary keys
		/// </summary>
		/// <param name="ids">List of primary keys</param>
		/// <returns>
		/// List of Products
		/// </returns>
		public List<Product> GetFromKeys(List<int> ids){
			return Db.Products
				.Where(p => ids.Contains(p.ID))
				.ToList();
		}
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
		/// <summary>
		/// Adds list of Products to an Order and removes that
		/// list of Products from a Location
		/// </summary>
		/// <remarks>
		/// This attempts to make the entire process a transaction
		/// that can be rolled back halfway through should there be
		/// any kind of serious error
		/// </remarks>
		/// <param name="products">List of Products</param>
		/// <param name="order">The Order</param>
		/// <param name="location">The Location</param>
		public bool ForwardOperation(List<Product> products, Order order, Location location) {
			List<OrderProduct> opRange = products.ConvertAll(product =>
				new OrderProduct() {
					OrderID = order.ID,
					ProductID = product.ID
				}
			);
			if (opRange.Count == 0) {
				return false;
			}
			List<LocationProduct> lpRange = new List<LocationProduct>();
			products.ForEach(product => {
				LocationProduct result = Db.Set<LocationProduct>()
					.FirstOrDefault(lp =>
						lp.LocationID == location.ID &&
						lp.ProductID == product.ID &&
						!lpRange.Contains(lp)
					);
				if (result != null) {
					lpRange.Add(result);
				}
			});
			if (lpRange.Count == 0) {
				return false;
			}
			Db.Set<OrderProduct>().AddRange(opRange);
			Db.Set<LocationProduct>().RemoveRange(lpRange);
			return base.Commit();
		}
		/// <summary>
		/// Removes list of Products from an Order and adds that
		/// list of Products to a Location
		/// </summary>
		/// <remarks>
		/// This attempts to make the entire process a transaction
		/// that can be rolled back halfway through should there be
		/// any kind of serious error
		/// </remarks>
		/// <param name="products">List of Products</param>
		/// <param name="order">The Order</param>
		/// <param name="location">The Location</param>
		public bool ReverseOperation(List<Product> products, Order order, Location location) {
			List<OrderProduct> opRange = new List<OrderProduct>();
			products.ForEach(product => {
				OrderProduct result = Db.Set<OrderProduct>()
					.FirstOrDefault(op =>
						op.OrderID == order.ID &&
						op.ProductID == product.ID &&
						!opRange.Contains(op)
					);
				if (result != null) {
					opRange.Add(result);
				}
			});
			if (opRange.Count == 0) {
				return false;
			}
			List<LocationProduct> lpRange = products.ConvertAll(product =>
				new LocationProduct() {
					LocationID = location.ID,
					ProductID = product.ID
				}
			);
			if (lpRange.Count == 0) {
				return false;
			}
			Db.Set<OrderProduct>().RemoveRange(opRange);
			Db.Set<LocationProduct>().AddRange(lpRange);
			return base.Commit();
		}
	}
}
