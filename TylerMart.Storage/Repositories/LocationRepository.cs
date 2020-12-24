using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TylerMart.Storage.Models;

namespace TylerMart.Storage.Repositories {
	public class LocationRepository : Repository<Location> {
		public LocationRepository(DbContext db) : base(db) {}
		/// <summary>
		/// Gets location from name
		/// </summary>
		/// <returns>
		/// Returns single location or null
		/// </returns>
		public Location GetByName(string name) {
			return Db.Set<Location>()
				.SingleOrDefault(l => l.Name == name);
		}
		/// <summary>
		/// Get list of locations where product is stocked
		/// </summary>
		/// <returns>
		/// Returns list of locations
		/// </returns>
		public List<Location> FindFromProduct(Product product) {
			return Db.Set<LocationProduct>()
				.Where(lp => lp.ProductID == product.ProductID)
				.Include(lp => lp.Location)
				.Select(lp => lp.Location)
				.ToList();
		}
		/// <summary>
		/// Adds product to location's inventory
		/// </summary>
		/// <returns>
		/// Returns 'true' if successfully inserted into database
		/// </returns>
		public bool AddProduct(Location location, Product product) {
			LocationProduct lp = new LocationProduct() {
				LocationID = location.LocationID,
				ProductID = product.ProductID
			};
			Db.Set<LocationProduct>().Add(lp);
			return Db.SaveChanges() >= 1;
		}
		/// <summary>
		/// Removes product from location's inventory
		/// </summary>
		/// <returns>
		/// Returns 'true' if successfully removed from database
		/// </returns>
		public bool RemoveProduct(Location location, Product product) {
			LocationProduct q = Db.Set<LocationProduct>()
				.LastOrDefault(lp => lp.LocationID == location.LocationID && lp.ProductID == product.ProductID);
			if (q != null) {
				Db.Set<LocationProduct>().Remove(q);
				return Db.SaveChanges() >= 1;
			}
			return false;
		}
		/// <summary>
		/// Adds list of products to location's inventory
		/// </summary>
		/// <returns>
		/// Returns 'true' if successfully inserted into database
		/// </returns>
		public bool AddProducts(Location location, List<Product> products) {
			List<LocationProduct> lps = products.ConvertAll<LocationProduct>(p =>
				new LocationProduct() {
					LocationID = location.LocationID,
					ProductID = p.ProductID
				}
			);
			Db.Set<LocationProduct>().AddRange(lps);
			return Db.SaveChanges() >= 1;
		}
		/// <summary>
		/// Removes list of products from location's inventory
		/// </summary>
		/// <returns>
		/// Returns 'true' if successfully removed from database
		/// </returns>
		public bool RemoveProducts(Location location, List<Product> products) {
			List<LocationProduct> range = new List<LocationProduct>();
			foreach (var p in products) {
				var lps = Db.Set<LocationProduct>()
					.Where(lp => lp.LocationID == location.LocationID && lp.ProductID == p.ProductID);
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
