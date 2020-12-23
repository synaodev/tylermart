using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Storage.Models {
	[Table("Inventories")]
	public class Inventory : Model {
		[Required]
		public List<LocationInventory> LocationInventories { get; set; }
		[Required]
		public List<Product> Products { get; set; } = new List<Product>();
		public int Count { get => Products.Count; }
	}
}
