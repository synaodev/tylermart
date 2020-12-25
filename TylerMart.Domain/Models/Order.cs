using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Order Model
	/// </summary>
	[Table("Orders")]
	public class Order : Model {
		/// <summary>
		/// Primary key
		/// </summary>
		[Key]
		public int OrderID { get; set; }
		/// <summary>
		/// Placement Date
		/// </summary>
		public DateTime PlacedAt { get; set; }
		/// <summary>
		/// Completed
		/// </summary>
		public bool Completed { get; set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Customer"/> primary key
		/// </summary>
		[ForeignKey("Customer")]
		public int CustomerID { get; set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Customer"/> navigation field
		/// </summary>
		public virtual Customer Customer { get; set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Location"/> primary key
		/// </summary>
		[ForeignKey("Location")]
		public int LocationID { get; set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Location"/> navigation field
		/// </summary>
		public virtual Location Location { get; set; }
		/// <summary>
		/// Navigation list of <see cref="TylerMart.Domain.Models.OrderProduct"/> Pairs
		/// </summary>
		public virtual List<OrderProduct> OrderProducts { get; set; }
		/// <summary>
		/// Get Order's primary key
		/// </summary>
		/// <returns>
		/// 32-bit integer
		/// </returns>
		public override int GetID() => OrderID;
		/// <summary>
		/// Generates Order array for seeding database
		/// </summary>
		/// <remarks>
		/// Only to be used in <c>DbContext.OnModelCreating()</c>
		/// </remarks>
		/// <returns>
		/// Array of Orders
		/// </returns>
		public static Order[] GenerateSeededData() {
			Customer[] customers = Customer.GenerateSeededData();
			Location[] locations = Location.GenerateSeededData();
			Order[] orders = new Order[] {
				new Order() {
					OrderID = 1,
					PlacedAt = DateTime.Now,
					Completed = false,
					CustomerID = customers[0].CustomerID,
					LocationID = locations[0].LocationID
				},
				new Order() {
					OrderID = 2,
					PlacedAt = DateTime.Now,
					Completed = false,
					CustomerID = customers[1].CustomerID,
					LocationID = locations[1].LocationID
				}
			};
			return orders;
		}
	}
}
