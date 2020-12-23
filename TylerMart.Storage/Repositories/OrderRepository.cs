using Microsoft.EntityFrameworkCore;

using TylerMart.Storage.Models;

namespace TylerMart.Storage.Repositories {
	public class OrderRepository : Repository<Order> {
		public OrderRepository(DbContext db) : base(db) {}
	}
}
