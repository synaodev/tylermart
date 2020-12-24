using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Storage.Models {
	[Table("Customers")]
	public class Customer : Model {
		[Key]
		public int CustomerID { get; set; }
		[Required]
		[MinLength(2)]
		public string FirstName { get; set; }
		[Required]
		[MinLength(2)]
		public string LastName { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		public string EmailAddress { get; set; }
		[Required]
		[MinLength(8)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public override int GetID() => CustomerID;
	}
}
