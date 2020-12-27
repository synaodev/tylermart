using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Order-Product Pair Model
	/// </summary>
	[Table("OrderProducts")]
	public class OrderProduct : Model, IValidatableObject {
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Order"/> primary key
		/// </summary>
		public int OrderID { get; set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Order"/> navigation field
		/// </summary>
		/// <remarks>
		/// This field only exists for using DbContext's Fluent API
		/// and it will generally be null when accessed via repository,
		/// so do not attempt to use this field for any reason.
		/// </remarks>
		public virtual Order Order { get; private set; }
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
		/// Generates Order-Product Pair array for seeding database
		/// </summary>
		/// <remarks>
		/// Only to be used in <c>DbContext.OnModelCreating()</c>
		/// </remarks>
		/// <returns>
		/// Array of Order-Product Pairs
		/// </returns>
		public static OrderProduct[] GenerateSeededData() {
			Order[] orders = Order.GenerateSeededData();
			Product[] products = Product.GenerateSeededData();
			OrderProduct[] orderProducts = new OrderProduct[] {
				new OrderProduct() {
					ID = 1,
					OrderID = orders[0].ID,
					ProductID = products[0].ID
				},
				new OrderProduct() {
					ID = 2,
					OrderID = orders[0].ID,
					ProductID = products[1].ID
				},
				new OrderProduct() {
					ID = 3,
					OrderID = orders[1].ID,
					ProductID = products[0].ID
				},
				new OrderProduct() {
					ID = 4,
					OrderID = orders[1].ID,
					ProductID = products[1].ID
				}
			};
			return orderProducts;
		}
		/// <summary>
		/// Validates OrderID and ProductID
		/// </summary>
		/// <param name="context">Validation context</param>
		/// <returns>
		/// IEnumerable containing validation errors
		/// </returns>
		public IEnumerable<ValidationResult> Validate(ValidationContext context) {
			if (OrderID <= 0) {
				yield return new ValidationResult(
					"OrderID cannot be less than or equal to zero!",
					new[] { nameof(OrderID), nameof(OrderProduct) }
				);
			}
			if (ProductID <= 0) {
				yield return new ValidationResult(
					"ProductID cannot be less than or equal to zero!",
					new[] { nameof(ProductID), nameof(OrderProduct) }
				);
			}
		}
	}
}
