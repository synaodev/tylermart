using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Order-Product Pair Model
	/// </summary>
	[Table("OrderProducts")]
	public class OrderProduct : Model {
		/// <summary>
		/// Primary key
		/// </summary>
		[Key]
		public int OrderProductID { get; set; }
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
		/// Get Order-Product Pairs's primary key
		/// </summary>
		/// <returns>
		/// 32-bit integer
		/// </returns>
		public override int GetID() => OrderProductID;
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
					OrderProductID = 1,
					OrderID = orders[0].OrderID,
					ProductID = products[0].ProductID
				},
				new OrderProduct() {
					OrderProductID = 2,
					OrderID = orders[0].OrderID,
					ProductID = products[1].ProductID
				},
				new OrderProduct() {
					OrderProductID = 3,
					OrderID = orders[1].OrderID,
					ProductID = products[0].ProductID
				},
				new OrderProduct() {
					OrderProductID = 4,
					OrderID = orders[1].OrderID,
					ProductID = products[1].ProductID
				}
			};
			return orderProducts;
		}
		/// <summary>
		/// Validates OrderProduct.OrderID and OrderProduct.ProductID
		/// </summary>
		/// <param name="context">Validation context</param>
		/// <returns>
		/// IEnumerable containing validation errors
		/// </returns>
		public IEnumerable<ValidationResult> Validate(ValidationContext context) {
			if (OrderID <= 0) {
				yield return new ValidationResult(
					"OrderProduct.OrderID cannot be less than or equal to zero!",
					new[] { nameof(OrderID), nameof(OrderProduct) }
				);
			}
			if (ProductID <= 0) {
				yield return new ValidationResult(
					"OrderProduct.ProductID cannot be less than or equal to zero!",
					new[] { nameof(ProductID), nameof(OrderProduct) }
				);
			}
		}
	}
}
