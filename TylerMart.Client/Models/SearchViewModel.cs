using System.Collections.Generic;

using TylerMart.Domain.Models;

namespace TylerMart.Client.Models {
	public class SearchViewModel {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public List<Customer> Results { get; set; }
	}
}
