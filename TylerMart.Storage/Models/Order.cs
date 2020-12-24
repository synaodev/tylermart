using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Storage.Models {
	[Table("Orders")]
	public class Order : Model {
		[Key]
		public int OrderID { get; set; }
		[Required]
		public Customer Customer { get; set; }
		[Required]
		public Location Location { get; set; }
		[Required]
		public DateTime PlacedAt { get; set; }
		[Required]
		[MaxLength(30)]
		public List<Product> Products { get; set; }
		public override int GetID() => OrderID;
	}
}
