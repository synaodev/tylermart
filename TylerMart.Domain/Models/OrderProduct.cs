using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	[Table("OrderProducts")]
	public class OrderProduct : Model {
		[Key]
		public int OrderProductID { get; set; }
		public int OrderID { get; set; }
		public virtual Order Order { get; set; }
		public int ProductID { get; set; }
		public virtual Product Product { get; set; }
		public override int GetID() => OrderProductID;
		/// <summary>
		/// Generates order's product list for seeding database
		/// Only to be used in DbContext.OnModelCreating()
		/// </summary>
		/// <returns>
		/// Returns array of order to product links
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
