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
		/// <summary>
		/// Generates location array for seeding database
		/// Only to be used in DbContext.OnModelCreating()
		/// </summary>
		/// <returns>
		/// Returns array of locations
		/// </returns>
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
