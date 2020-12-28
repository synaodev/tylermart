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
		/// Name
		/// </summary>
		[Required(ErrorMessage = "Name is required!")]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be at least two letters long!")]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name must only contain letters!")]
		public string Name { get; set; }
		/// <summary>
		/// Navigation list of <see cref="TylerMart.Domain.Models.LocationProduct"/> Pairs
		/// </summary>
		/// <remarks>
		/// This field only exists for using DbContext's Fluent API
		/// and it will generally be null when accessed via repository,
		/// so do not attempt to use this field for any reason.
		/// </remarks>
		public virtual List<LocationProduct> LocationProducts { get; private set; }
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
					ID = 1,
					Name = "Dreamland"
				},
				new Location() {
					ID = 2,
					Name = "California"
				},
				new Location() {
					ID = 3,
					Name = "Washington"
				},
				new Location() {
					ID = 4,
					Name = "Oregon"
				},
				new Location() {
					ID = 5,
					Name = "Texas"
				},
				new Location() {
					ID = 6,
					Name = "New York"
				},
				new Location() {
					ID = 7,
					Name = "Virginia"
				}
			};
			return locations;
		}
	}
}
