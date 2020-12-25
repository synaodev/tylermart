using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Product Model
	/// </summary>
	[Table("Products")]
	public class Product : Model {
		/// <summary>
		/// Primary key
		/// </summary>
		[Key]
		public int ProductID { get; set; }
		/// <summary>
		/// Name
		/// </summary>
		[MinLength(2, ErrorMessage = "Name must be at least two letters long!")]
		public string Name { get; set; }
		/// <summary>
		/// Description
		/// </summary>
		[MinLength(3, ErrorMessage = "Description must be at least three letters long!")]
		public string Description { get; set; }
		/// <summary>
		/// Price
		/// </summary>
		public decimal Price { get; set; }
		/// <summary>
		/// Navigation list of <see cref="TylerMart.Domain.Models.LocationProduct"/> Pairs
		/// </summary>
		public virtual List<LocationProduct> LocationProducts { get; set; }
		/// <summary>
		/// Navigation list of <see cref="TylerMart.Domain.Models.OrderProduct"/> Pairs
		/// </summary>
		public virtual List<OrderProduct> OrderProducts { get; set; }
		/// <summary>
		/// Get Product's primary key
		/// </summary>
		/// <returns>
		/// 32-bit integer
		/// </returns>
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
