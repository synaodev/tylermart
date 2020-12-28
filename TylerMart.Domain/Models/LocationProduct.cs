using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Location-Product Pair Model
	/// </summary>
	[Table("LocationProducts")]
	public class LocationProduct : Model, IValidatableObject {
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Location"/> primary key
		/// </summary>
		public int LocationID { get; set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Location"/> navigation field
		/// </summary>
		/// <remarks>
		/// This field only exists for using DbContext's Fluent API
		/// and it will generally be null when accessed via repository,
		/// so do not attempt to use this field for any reason.
		/// </remarks>
		public virtual Location Location { get; private set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Product"/> primary key
		/// </summary>
		public int ProductID { get; set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Product"/> navigation field
		/// </summary>
		/// <remarks>
		/// This field only exists for using DbContext's Fluent API
		/// and it will generally be null when accessed via repository,
		/// so do not attempt to use this field for any reason.
		/// </remarks>
		public virtual Product Product { get; private set; }
		/// <summary>
		/// Generates Location-Product Pair array for seeding database
		/// </summary>
		/// <remarks>
		/// Only to be used in <c>DbContext.OnModelCreating()</c>
		/// </remarks>
		/// <returns>
		/// Array of Location-Product Pairs
		/// </returns>
		public static LocationProduct[] GenerateSeededData() {
			Location[] locations = Location.GenerateSeededData();
			Product[] products = Product.GenerateSeededData();
			LocationProduct[] locationProducts = new LocationProduct[] {
				new LocationProduct() {
					ID = 1,
					LocationID = locations[0].ID,
					ProductID = products[0].ID
				}
			};
			return locationProducts;
		}
		/// <summary>
		/// Validates LocationID and ProductID
		/// </summary>
		/// <param name="context">Validation context</param>
		/// <returns>
		/// IEnumerable containing validation errors
		/// </returns>
		public IEnumerable<ValidationResult> Validate(ValidationContext context) {
			if (LocationID <= 0) {
				yield return new ValidationResult(
					"LocationID cannot be less than or equal to zero!",
					new[] { nameof(LocationID), nameof(LocationProduct) }
				);
			}
			if (ProductID <= 0) {
				yield return new ValidationResult(
					"ProductID cannot be less than or equal to zero!",
					new[] { nameof(ProductID), nameof(LocationProduct) }
				);
			}
		}
	}
}
