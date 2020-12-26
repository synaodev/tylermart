using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Customer Model
	/// </summary>
	[Table("Customers")]
	public class Customer : Model {
		/// <summary>
		/// Primary key
		/// </summary>
		[Key]
		public int CustomerID { get; set; }
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
		[DataType(DataType.EmailAddress)]
		public string EmailAddress { get; set; }
		/// <summary>
		/// Password
		/// </summary>
		[Required(ErrorMessage = "Password is required!")]
		[DataType(DataType.Password)]
		[MinLength(8, ErrorMessage = "Password must be at least eight characters long!")]
		public string Password { get; set; }
		/// <summary>
		/// Get Customer's primary key
		/// </summary>
		/// <returns>
		/// 32-bit integer
		/// </returns>
		public override int GetID() => CustomerID;
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
					CustomerID = 1,
					FirstName = "Tyler",
					LastName = "Cadena",
					EmailAddress = "tyler.cadena@revature.net",
					Password = "tylercadena"
				},
				new Customer() {
					CustomerID = 2,
					FirstName = "George",
					LastName = "Bumble",
					EmailAddress = "george.bumble@revature.net",
					Password = "georgebumble"
				}
			};
			return customers;
		}
	}
}
