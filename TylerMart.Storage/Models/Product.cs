using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Storage.Models {
	[Table("Products")]
	public class Product : Model {
		[Key]
		public int ID { get; set; }
		[Required]
		[MinLength(3)]
		public string Name { get; set; }
		[Required]
		[MinLength(5)]
		public string Description { get; set; }
		[Required]
		public decimal Price { get; set; }
		public override int GetID() => ID;
	}
}
