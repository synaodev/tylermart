using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TylerMart.Storage.Models;

namespace TylerMart.Storage.Repositories {
	public class CustomerRepository : Repository<Customer> {
		public CustomerRepository(DbContext db) : base(db) {}
		public Customer GetByEmailAddress(string emailAddress) {
			return Db.Set<Customer>()
				.SingleOrDefault(c => c.EmailAddress == emailAddress);
		}
		public List<Customer> FindByFirstName(string name) {
			return Db.Set<Customer>()
				.Where(c => c.FirstName == name)
				.ToList();
		}
		public List<Customer> FindByLastName(string name) {
			return Db.Set<Customer>()
				.Where(c => c.LastName == name)
				.ToList();
		}
		public List<Customer> FindByWholeName(string firstName, string lastName) {
			return Db.Set<Customer>()
				.Where(c => c.FirstName == firstName && c.LastName == lastName)
				.ToList();
		}
	}
}
