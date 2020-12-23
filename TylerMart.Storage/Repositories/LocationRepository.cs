using Microsoft.EntityFrameworkCore;

using TylerMart.Storage.Models;

namespace TylerMart.Storage.Repositories {
	public class LocationRepository : Repository<Location> {
		public LocationRepository(DbContext db) : base(db) {}
	}
}
