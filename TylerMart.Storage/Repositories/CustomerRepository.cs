using Microsoft.EntityFrameworkCore;

using TylerMart.Storage.Models;

namespace TylerMart.Storage.Repositories {
	public class CustomerRepository : Repository<Customer> {
		public CustomerRepository(DbContext db) : base(db) {}
	}
}
