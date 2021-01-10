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
		[StringLength(30, MinimumLength = 2, ErrorMessage = "First name must be between two and thirty characters in length!")]
		public string FirstName { get; set; }
		/// <summary>
		/// Last Name
		/// </summary>
		[Required(ErrorMessage = "Last name is required!")]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "Last name must be between two and thirty characters in length!")]
		public string LastName { get; set; }
		/// <summary>
		/// Email Address
		/// </summary>
		[Required(ErrorMessage = "Email is required!")]
		[StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be between five and one-hundred characters in length!")]
		public string Email { get; set; }
		/// <summary>
		/// Password
		/// </summary>
		[Required(ErrorMessage = "Password is required!")]
		[StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be between eight and fifty characters in length!")]
		public string Password { get; set; }
		/// <summary>
		/// Real Address
		/// </summary>
		[Required(ErrorMessage = "Address is required!")]
		[StringLength(100, MinimumLength = 5, ErrorMessage = "Address must be between five and one-hundred characters in length!")]
		public string Address { get; set; }
		/// <summary>
		/// Default <see cref="TylerMart.Domain.Models.Location"/> primary key
		/// </summary>
		public int DefaultLocationID { get; set; }
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
					Email = "admin.admin@revature.net",
					Password = "administrator",
					Address = "Nowhere",
					DefaultLocationID = 0
				}
			};
			return customers;
		}
		/// <summary>
		/// Order prettifier
		/// </summary>
		/// <returns>
		/// Order as a string
		/// </returns>
		public override string ToString() {
			string result = "";
			result += $"\tCustomer ID: {ID}\n";
			result += $"\tFirst Name: {FirstName}\n";
			result += $"\tLast Name: {LastName}\n";
			result += $"\tEmail: {Email}\n";
			result += $"\tAddress: {Address}";
			result += $"\tDefault Location ID: {DefaultLocationID}\n";
			return result;
		}
	}
}
