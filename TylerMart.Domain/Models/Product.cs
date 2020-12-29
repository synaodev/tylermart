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
		[MinLength(2, ErrorMessage = "Name must be at least two letters long!")]
		public string Name { get; set; }
		/// <summary>
		/// Description
		/// </summary>
		[Required(ErrorMessage = "Description is required!")]
		[MinLength(3, ErrorMessage = "Description must be at least three letters long!")]
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
					Name = "Nightmare",
					Description = "This is a nightmare!",
					Price = 180.50M
				},
				new Product() {
					ID = 2,
					Name = "Eggs",
					Description = "Delicious eggs",
					Price = 4.40M
				},
				new Product() {
					ID = 3,
					Name = "Bookshelf",
					Description = "For storing books",
					Price = 68.99M
				},
				new Product() {
					ID = 4,
					Name = "Jacket",
					Description = "For when it's chilly outside",
					Price = 18.99M
				},
				new Product() {
					ID = 5,
					Name = "Oranges",
					Description = "A bunch of oranges",
					Price = 4.0M
				},
				new Product() {
					ID = 6,
					Name = "Lipstick",
					Description = "It's a purple-ish color",
					Price = 5.99M
				},
				new Product() {
					ID = 7,
					Name = "Cave Story Switch",
					Description = "An excellent game",
					Price = 35.99M
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
