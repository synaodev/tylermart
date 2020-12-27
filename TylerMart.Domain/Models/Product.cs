using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Product Model
	/// </summary>
	[Table("Products")]
	public class Product : Model, IValidatableObject {
		/// <summary>
		/// Name
		/// </summary>
		[Required(ErrorMessage = "Name is required!")]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be at least two letters long!")]
		public string Name { get; set; }
		/// <summary>
		/// Description
		/// </summary>
		[Required(ErrorMessage = "Description is required!")]
		[StringLength(500, MinimumLength = 2, ErrorMessage = "Description must be at least three letters long!")]
		public string Description { get; set; }
		/// <summary>
		/// Price
		/// </summary>
		public decimal Price { get; set; } = 0.0M;
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
		/// Navigation list of <see cref="TylerMart.Domain.Models.OrderProduct"/> Pairs
		/// </summary>
		/// <remarks>
		/// This field only exists for using DbContext's Fluent API
		/// and it will generally be null when accessed via repository,
		/// so do not attempt to use this field for any reason.
		/// </remarks>
		public virtual List<OrderProduct> OrderProducts { get; private set; }
		/// <summary>
		/// Generates Product array for seeding database
		/// </summary>
		/// <remarks>
		/// Only to be used in <c>DbContext.OnModelCreating()</c>
		/// </remarks>
		/// <returns>
		/// Array of Products
		/// </returns>
		public static Product[] GenerateSeededData() {
			Product[] products = new Product[] {
				new Product() {
					ID = 1,
					Name = "Bag",
					Description = "You can carry stuff around",
					Price = 3.50M
				},
				new Product() {
					ID = 2,
					Name = "Glasses",
					Description = "Helpful for those with poor eyesight",
					Price = 20.00M
				}
			};
			return products;
		}
		/// <summary>
		/// Validates Product.Price
		/// </summary>
		/// <param name="context">Validation context</param>
		/// <returns>
		/// IEnumerable containing validation errors
		/// </returns>
		public IEnumerable<ValidationResult> Validate(ValidationContext context) {
			if (Price <= 0.0M) {
				yield return new ValidationResult(
					"Price cannot be less than or equal to zero!",
					new[] { nameof(Price), nameof(Product) }
				);
			}
		}
	}
}
