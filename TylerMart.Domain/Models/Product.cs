using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Product Model contains:
	/// <list>
	/// <item>- Product ID</item>
	/// <item>- Name</item>
	/// <item>- Description</item>
	/// <item>- Price</item>
	/// <item>- List of <see cref="TylerMart.Domain.Models.LocationProduct"/> Pairs</item>
	/// <item>- List of <see cref="TylerMart.Domain.Models.OrderProduct"/> Pairs</item>
	/// </list>
	/// </summary>
	[Table("Products")]
	public class Product : Model {
		[Key]
		public int ProductID { get; set; }
		[MinLength(3)]
		public string Name { get; set; }
		[MinLength(5)]
		public string Description { get; set; }
		[Required]
		public decimal Price { get; set; }
		public virtual List<LocationProduct> LocationProducts { get; set; }
		public virtual List<OrderProduct> OrderProducts { get; set; }
		public override int GetID() => ProductID;
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
					ProductID = 1,
					Name = "Bag",
					Description = "You can carry stuff around",
					Price = 3.50M
				},
				new Product() {
					ProductID = 2,
					Name = "Glasses",
					Description = "Helpful for those with poor eyesight",
					Price = 20.00M
				}
			};
			return products;
		}
	}
}
