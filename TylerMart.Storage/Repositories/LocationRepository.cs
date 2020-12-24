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
	}
}
