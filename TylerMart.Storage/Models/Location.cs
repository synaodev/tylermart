using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Storage.Models {
	[Table("Locations")]
	public class Location : Model {
		[Key]
		public int LocationID { get; set; }
		[Required]
		[MinLength(3)]
		public string Name { get; set; }
		[Required]
		public List<Product> Inventory { get; set; }
		public override int GetID() => LocationID;
	}
}
