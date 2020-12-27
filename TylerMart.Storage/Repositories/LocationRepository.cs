using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using TylerMart.Domain.Models;
using TylerMart.Storage.Contexts;

namespace TylerMart.Storage.Repositories {
	/// <summary>
	/// <see cref="TylerMart.Domain.Models.Location"/> Repository
	/// </summary>
	public class LocationRepository : Repository<Location> {
		/// <summary>
		/// Constructor that takes an instance of DatabaseContext
		/// </summary>
		/// <param name="db">Instance of DatabaseContext</param>
		public LocationRepository(DatabaseContext db) : base(db) {}
		/// <summary>
		/// Gets Location from name
		/// </summary>
		/// <param name="name">Name</param>
		/// <returns>
		/// Single Location or null
		/// </returns>
		public Location GetByName(string name) {
			return Db.Locations
				.SingleOrDefault(l => l.Name == name);
		}
		/// <summary>
		/// Finds Locations where a Product is in stock
		/// </summary>
		/// <param name="product">The Product</param>
		/// <returns>
		/// List of Locations
		/// </returns>
		public List<Location> FindFromProduct(Product product) {
			return Db.Set<LocationProduct>()
				.Where(lp => lp.ProductID == product.ID)
				.Include(lp => lp.Location)
				.Select(lp => lp.Location)
				.Distinct()
				.ToList();
		}
		/// <summary>
		/// Adds a Product to a Location's inventory
		/// </summary>
		/// <param name="location">The Location</param>
		/// <param name="product">The Product</param>
		/// <returns>
		/// 'true' if successfully inserted into database
		/// </returns>
		public bool AddProduct(Location location, Product product) {
			LocationProduct lp = new LocationProduct() {
				LocationID = location.ID,
				ProductID = product.ID
			};
			Db.Set<LocationProduct>().Add(lp);
			return Db.SaveChanges() >= 1;
		}
		/// <summary>
		/// Removes a Product from a Location's inventory
		/// </summary>
		/// <param name="location">The Location</param>
		/// <param name="product">The Product</param>
		/// <returns>
		/// 'true' if successfully removed from database
		/// </returns>
		public bool RemoveProduct(Location location, Product product) {
			LocationProduct q = Db.Set<LocationProduct>()
				.SingleOrDefault(lp => lp.LocationID == location.ID && lp.ProductID == product.ID);
			if (q != null) {
				Db.Set<LocationProduct>().Remove(q);
				return Db.SaveChanges() >= 1;
			}
			return false;
		}
		/// <summary>
		/// Adds a range of Products to a Location's inventory
		/// </summary>
		/// <param name="location">The Location</param>
		/// <param name="products">List of Products</param>
		/// <returns>
		/// 'true' if successfully inserted into database
		/// </returns>
		public bool AddProducts(Location location, List<Product> products) {
			List<LocationProduct> lps = products.ConvertAll<LocationProduct>(p =>
				new LocationProduct() {
					LocationID = location.ID,
					ProductID = p.ID
				}
			);
			Db.Set<LocationProduct>().AddRange(lps);
			return Db.SaveChanges() >= 1;
		}
		/// <summary>
		/// Removes a range of Products from a Location's inventory
		/// </summary>
		/// <param name="location">The Location</param>
		/// <param name="products">List of Products</param>
		/// <returns>
		/// 'true' if successfully removed from database
		/// </returns>
		public bool RemoveProducts(Location location, List<Product> products) {
			List<LocationProduct> range = new List<LocationProduct>();
			foreach (var p in products) {
				var lps = Db.Set<LocationProduct>()
					.Where(lp => lp.LocationID == location.ID && lp.ProductID == p.ID);
				range.AddRange(lps);
			}
			if (range.Count > 0) {
				Db.Set<LocationProduct>().RemoveRange(range);
				return Db.SaveChanges() >= 1;
			}
			return false;
		}
	}
}
