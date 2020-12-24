using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TylerMart.Storage.Models;

namespace TylerMart.Storage.Repositories {
	public class LocationRepository : Repository<Location> {
		public LocationRepository(DbContext db) : base(db) {}
		public Location GetByName(string name) {
			return Db.Set<Location>()
				.SingleOrDefault(l => l.Name == name);
		}
		public List<Location> FindFromProduct(Product product) {
			return Db.Set<LocationProduct>()
				.Where(lp => lp.ProductID == product.ProductID)
				.Include(lp => lp.Location)
				.Select(lp => lp.Location)
				.ToList();
		}
		public bool AddProduct(Location location, Product product) {
			LocationProduct lp = new LocationProduct() {
				LocationID = location.LocationID,
				ProductID = product.ProductID
			};
			Db.Set<LocationProduct>().Add(lp);
			return Db.SaveChanges() >= 1;
		}
		public bool RemoveProduct(Location location, Product product) {
			LocationProduct q = Db.Set<LocationProduct>()
				.LastOrDefault(lp => lp.LocationID == location.LocationID && lp.ProductID == product.ProductID);
			if (q != null) {
				Db.Set<LocationProduct>().Remove(q);
				return Db.SaveChanges() >= 1;
			}
			return false;
		}
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
