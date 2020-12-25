using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Location Model contains:
	/// <list>
	/// <item>- Location ID</item>
	/// <item>- Name</item>
	/// <item>- List of <see cref="TylerMart.Domain.Models.LocationProduct"/> Pairs</item>
	/// </list>
	/// </summary>
	[Table("Locations")]
	public class Location : Model {
		[Key]
		public int LocationID { get; set; }
		[MinLength(3)]
		public string Name { get; set; }
		public virtual List<LocationProduct> LocationProducts { get; set; }
		public override int GetID() => LocationID;
		/// <summary>
		/// Generates Location array for seeding database
		/// </summary>
		/// <remarks>
		/// Only to be used in <c>DbContext.OnModelCreating()</c>
		/// </remarks>
		/// <returns>
		/// Array of Locations
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
