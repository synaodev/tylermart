using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Storage.Models {
	[Table("Products")]
	public class Product : Model {
		[Required]
		[MinLength(3)]
		public string Name { get; set; }
		[Required]
		[MinLength(5)]
		public string Description { get; set; }
		[Required]
		public decimal Price { get; set; }
	}
}
