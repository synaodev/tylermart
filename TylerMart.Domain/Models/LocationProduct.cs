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
				// Dummy
				new LocationProduct() {
					ID = 1,
					LocationID = locations[0].ID,
					ProductID = products[0].ID
				},
				/*
				 * California:
				 * - Eggs: 2
				 * - Bookshelf: 3
				 */
				new LocationProduct() {
					ID = 2,
					LocationID = locations[1].ID,
					ProductID = products[1].ID
				},
				new LocationProduct() {
					ID = 3,
					LocationID = locations[1].ID,
					ProductID = products[1].ID
				},
				new LocationProduct() {
					ID = 4,
					LocationID = locations[1].ID,
					ProductID = products[2].ID
				},
				new LocationProduct() {
					ID = 5,
					LocationID = locations[1].ID,
					ProductID = products[2].ID
				},
				new LocationProduct() {
					ID = 6,
					LocationID = locations[1].ID,
					ProductID = products[2].ID
				},
				/*
				 * Washington:
				 * - Jacket: 3
				 */
				new LocationProduct() {
					ID = 7,
					LocationID = locations[2].ID,
					ProductID = products[3].ID
				},
				new LocationProduct() {
					ID = 8,
					LocationID = locations[2].ID,
					ProductID = products[3].ID
				},
				new LocationProduct() {
					ID = 9,
					LocationID = locations[2].ID,
					ProductID = products[3].ID
				},
				/*
				 * Oregon:
				 * - Oranges: 1
				 * - Lipstick: 1
				 * - Cave Story: 1
				 */
				new LocationProduct() {
					ID = 10,
					LocationID = locations[3].ID,
					ProductID = products[4].ID
				},
				new LocationProduct() {
					ID = 11,
					LocationID = locations[3].ID,
					ProductID = products[5].ID
				},
				new LocationProduct() {
					ID = 12,
					LocationID = locations[3].ID,
					ProductID = products[6].ID
				},
				/*
				 * Texas:
				 * - Eggs: 4
				 */
				new LocationProduct() {
					ID = 13,
					LocationID = locations[4].ID,
					ProductID = products[1].ID
				},
				new LocationProduct() {
					ID = 14,
					LocationID = locations[4].ID,
					ProductID = products[1].ID
				},
				new LocationProduct() {
					ID = 15,
					LocationID = locations[4].ID,
					ProductID = products[1].ID
				},
				new LocationProduct() {
					ID = 16,
					LocationID = locations[4].ID,
					ProductID = products[1].ID
				},
				/*
				 * New York:
				 * - Bookshelf: 2
				 * - Cave Story: 2
				 * - Oranges: 1
				 */
				new LocationProduct() {
					ID = 17,
					LocationID = locations[5].ID,
					ProductID = products[2].ID
				},
				new LocationProduct() {
					ID = 18,
					LocationID = locations[5].ID,
					ProductID = products[2].ID
				},
				new LocationProduct() {
					ID = 19,
					LocationID = locations[5].ID,
					ProductID = products[6].ID
				},
				new LocationProduct() {
					ID = 20,
					LocationID = locations[5].ID,
					ProductID = products[6].ID
				},
				new LocationProduct() {
					ID = 21,
					LocationID = locations[5].ID,
					ProductID = products[4].ID
				},
				/*
				 * Virginia:
				 * - One of each
				 */
				new LocationProduct() {
					ID = 22,
					LocationID = locations[6].ID,
					ProductID = products[1].ID
				},
				new LocationProduct() {
					ID = 23,
					LocationID = locations[6].ID,
					ProductID = products[2].ID
				},
				new LocationProduct() {
					ID = 24,
					LocationID = locations[6].ID,
					ProductID = products[3].ID
				},
				new LocationProduct() {
					ID = 25,
					LocationID = locations[6].ID,
					ProductID = products[4].ID
				},
				new LocationProduct() {
					ID = 26,
					LocationID = locations[6].ID,
					ProductID = products[5].ID
				},
				new LocationProduct() {
					ID = 27,
					LocationID = locations[6].ID,
					ProductID = products[6].ID
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
