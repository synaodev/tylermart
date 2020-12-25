using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Location Model
	/// </summary>
	[Table("Locations")]
	public class Location : Model {
		/// <summary>
		/// Primary key
		/// </summary>
		[Key]
		public int LocationID { get; set; }
		/// <summary>
		/// Name
		/// </summary>
		[MinLength(2, ErrorMessage = "Name must be at least two letters long!")]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name must only contain letters!")]
		public string Name { get; set; }
		/// <summary>
		/// Navigation list of <see cref="TylerMart.Domain.Models.LocationProduct"/> Pairs
		/// </summary>
		public virtual List<LocationProduct> LocationProducts { get; set; }
		/// <summary>
		/// Get Location's primary key
		/// </summary>
		/// <returns>
		/// 32-bit integer
		/// </returns>
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
