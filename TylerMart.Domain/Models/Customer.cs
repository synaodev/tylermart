using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Customer Model
	/// </summary>
	[Table("Customers")]
	public class Customer : Model {
		/// <summary>
		/// First Name
		/// </summary>
		[Required(ErrorMessage = "First name is required!")]
		[MinLength(2, ErrorMessage = "First name must be at least two letters long!")]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name should only contain letters!")]
		public string FirstName { get; set; }
		/// <summary>
		/// Last Name
		/// </summary>
		[Required(ErrorMessage = "Last name is required!")]
		[MinLength(2, ErrorMessage = "Last name must be at least two letters long!")]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name should only contain letters!")]
		public string LastName { get; set; }
		/// <summary>
		/// Email Address
		/// </summary>
		[Required(ErrorMessage = "Email address is required!")]
		[EmailAddress(ErrorMessage = "Email address must be valid!")]
		public string EmailAddress { get; set; }
		/// <summary>
		/// Password
		/// </summary>
		[Required(ErrorMessage = "Password is required!")]
		[MinLength(8, ErrorMessage = "Password must be at least eight characters long!")]
		public string Password { get; set; }
		/// <summary>
		/// Real Address
		/// </summary>
		[Required(ErrorMessage = "Real address is required!")]
		[MinLength(5, ErrorMessage = "Real address must be at least five characters long!")]
		public string RealAddress { get; set; }
		/// <summary>
		/// Generates Customer array for seeding database
		/// </summary>
		/// <remarks>
		/// Only to be used in <c>DbContext.OnModelCreating()</c>
		/// </remarks>
		/// <returns>
		/// Array of Customers
		/// </returns>
		public static Customer[] GenerateSeededData() {
			Customer[] customers = new Customer[] {
				new Customer() {
					ID = 1,
					FirstName = "Admin",
					LastName = "Admin",
					EmailAddress = "admin.admin@revature.net",
					Password = "administrator",
					RealAddress = "Nowhere"
				}
			};
			return customers;
		}
	}
}
