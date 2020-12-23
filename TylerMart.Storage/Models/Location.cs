using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Storage.Models {
	[Table("Locations")]
	public class Location : Model {
		[Key]
		public int ID { get; set; }
		[Required]
		[MinLength(3)]
		public string Name { get; set; }
		[Required]
		public List<LocationInventory> LocationInventories { get; set; }
		public override int GetID() => ID;
	}
}
