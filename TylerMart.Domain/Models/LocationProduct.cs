using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	[Table("LocationProducts")]
	public class LocationProduct : Model {
		[Key]
		public int LocationProductID { get; set; }
		public int LocationID { get; set; }
		public virtual Location Location { get; set; }
		public int ProductID { get; set; }
		public virtual Product Product { get; set; }
		public override int GetID() => LocationProductID;
		/// <summary>
		/// Generates location's inventory for seeding database
		/// Only to be used in DbContext.OnModelCreating()
		/// </summary>
		/// <returns>
		/// Returns array of location to product links
		/// </returns>
		public static LocationProduct[] GenerateSeededData() {
			Location[] locations = Location.GenerateSeededData();
			Product[] products = Product.GenerateSeededData();
			LocationProduct[] locationProducts = new LocationProduct[] {
				new LocationProduct() {
					LocationProductID = 1,
					LocationID = locations[0].LocationID,
					ProductID = products[0].ProductID
				},
				new LocationProduct() {
					LocationProductID = 2,
					LocationID = locations[0].LocationID,
					ProductID = products[0].ProductID
				},
				new LocationProduct() {
					LocationProductID = 3,
					LocationID = locations[0].LocationID,
					ProductID = products[1].ProductID
				},
				new LocationProduct() {
					LocationProductID = 4,
					LocationID = locations[0].LocationID,
					ProductID = products[1].ProductID
				},
				new LocationProduct() {
					LocationProductID = 5,
					LocationID = locations[1].LocationID,
					ProductID = products[0].ProductID
				},
				new LocationProduct() {
					LocationProductID = 6,
					LocationID = locations[1].LocationID,
					ProductID = products[0].ProductID
				},
				new LocationProduct() {
					LocationProductID = 7,
					LocationID = locations[1].LocationID,
					ProductID = products[1].ProductID
				},
				new LocationProduct() {
					LocationProductID = 8,
					LocationID = locations[1].LocationID,
					ProductID = products[1].ProductID
				}
			};
			return locationProducts;
		}
	}
}
