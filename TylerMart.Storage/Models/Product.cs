using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Storage.Models {
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
