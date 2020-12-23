using Microsoft.EntityFrameworkCore;

using TylerMart.Storage.Models;

namespace TylerMart.Storage.Repositories {
	public class InventoryRepository : Repository<Inventory> {
		public InventoryRepository(DbContext db) : base(db) {}
	}
}
