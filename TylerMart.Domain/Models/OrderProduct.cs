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
		public virtual Order Order { get; set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Product"/> primary key
		/// </summary>
		public int ProductID { get; set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Product"/> navigation field
		/// </summary>
		public virtual Product Product { get; set; }
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
	}
}
