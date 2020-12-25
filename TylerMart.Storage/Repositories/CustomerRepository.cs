using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TylerMart.Domain.Models;

namespace TylerMart.Storage.Repositories {
	public class CustomerRepository : Repository<Customer> {
		public CustomerRepository(DbContext db) : base(db) {}
		/// <summary>
		/// Gets customer from email address
		/// </summary>
		/// <returns>
		/// Returns single row or null
		/// </returns>
		public Customer GetByEmailAddress(string emailAddress) {
			return Db.Set<Customer>()
				.SingleOrDefault(c => c.EmailAddress == emailAddress);
		}
		/// <summary>
		/// Finds customers with first name
		/// </summary>
		/// <returns>
		/// Returns single row or null
		/// </returns>
		public List<Customer> FindByFirstName(string name) {
			return Db.Set<Customer>()
				.Where(c => c.FirstName == name)
				.ToList();
		}
		/// <summary>
		/// Finds customers with last name
		/// </summary>
		/// <returns>
		/// Returns single row or null
		/// </returns>
		public List<Customer> FindByLastName(string name) {
			return Db.Set<Customer>()
				.Where(c => c.LastName == name)
				.ToList();
		}
		/// <summary>
		/// Finds customers with both first and last names
		/// </summary>
		/// <returns>
		/// Returns single row or null
		/// </returns>
		public List<Customer> FindByWholeName(string firstName, string lastName) {
			return Db.Set<Customer>()
				.Where(c => c.FirstName == firstName && c.LastName == lastName)
				.ToList();
		}
	}
}
