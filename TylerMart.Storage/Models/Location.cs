using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Storage.Models {
	[Table("Locations")]
	public class Location : Model {
		[Key]
		public int LocationID { get; set; }
		[MinLength(3)]
		public string Name { get; set; }
		public virtual List<LocationProduct> LocationProducts { get; set; }
		public override int GetID() => LocationID;
		public static Location[] GenerateSeededData() {
			Location[] locations = new Location[] {
				new Location() {
					LocationID = 1,
					Name = "New Jersey"
				},
				new Location() {
					LocationID = 2,
					Name = "Florida"
				}
			};
			return locations;
		}
	}
}
