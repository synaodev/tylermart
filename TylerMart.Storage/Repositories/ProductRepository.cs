using Microsoft.EntityFrameworkCore;

using TylerMart.Storage.Models;

namespace TylerMart.Storage.Repositories {
	public class ProductRepository : Repository<Product> {
		public ProductRepository(DbContext db) : base(db) {}
	}
}
