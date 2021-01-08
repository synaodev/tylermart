using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Order Model
	/// </summary>
	[Table("Orders")]
	public class Order : Model, IValidatableObject {
		/// <summary>
		/// Creation date
		/// </summary>
		public DateTime CreatedAt { get; set; } = DateTime.UnixEpoch;
		/// <summary>
		/// Order has been completed
		/// </summary>
		public bool Complete { get; set; } = false;
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Customer"/> primary key
		/// </summary>
		[ForeignKey("Customer")]
		public int CustomerID { get; set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Customer"/> navigation field
		/// </summary>
		/// <remarks>
		/// This field only exists for using DbContext's Fluent API
		/// and it will generally be null when accessed via repository,
		/// so do not attempt to use this field for any reason.
		/// </remarks>
		public virtual Customer Customer { get; private set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Location"/> primary key
		/// </summary>
		[ForeignKey("Location")]
		public int LocationID { get; set; }
		/// <summary>
		/// <see cref="TylerMart.Domain.Models.Location"/> navigation field
		/// </summary>
		/// <remarks>
		/// This field only exists for using DbContext's Fluent API
		/// and it will generally be null when accessed via repository,
		/// so do not attempt to use this field for any reason.
		/// </remarks>
		public virtual Location Location { get; private set; }
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
					ID = 1,
					Complete = true,
					CreatedAt = DateTime.Now,
					CustomerID = customers[0].ID,
					LocationID = locations[0].ID
				}
			};
			return orders;
		}
		/// <summary>
		/// Order prettifier
		/// </summary>
		/// <returns>
		/// Order as a string
		/// </returns>
		public override string ToString() {
			string result = "";
			result += string.Format("\tOrder ID: {0}\n", ID);
			result += string.Format("\tCreated At: {0}\n", CreatedAt);
			result += string.Format("\tCompleted: {0}\n", Complete);
			if (Customer != null) {
				result += string.Format("\tCustomer: {0} {1}\n", Customer.FirstName, Customer.LastName);
			} else {
				result += string.Format("\tCustomer ID: {0}\n", CustomerID);
			}
			if (Location != null) {
				result += string.Format("\tLocation: {0}\n", Location.Name);
			} else {
				result += string.Format("\tLocation ID: {0}\n", LocationID);
			}
			if (OrderProducts != null) {
				result += "\tProducts:\n";
				int c = 1;
				decimal price = 0.0M;
				OrderProducts.ForEach(op => {
					if (op.Product != null) {
						result += string.Format("\t\t{0}: {1}\n", c++, op.Product.Name);
						price += op.Product.Price;
					}
				});
				result += string.Format("\tTotal Price: {0}\n", price);
			}
			return result;
		}
		/// <summary>
		/// Validates Order.CustomerID and Order.LocationID
		/// </summary>
		/// <param name="context">Validation context</param>
		/// <returns>
		/// IEnumerable containing validation errors
		/// </returns>
		public IEnumerable<ValidationResult> Validate(ValidationContext context) {
			if (CustomerID <= 0) {
				yield return new ValidationResult(
					"CustomerID cannot be less than or equal to zero!",
					new[] { nameof(CustomerID), nameof(Order) }
				);
			}
			if (LocationID <= 0) {
				yield return new ValidationResult(
					"LocationID cannot be less than or equal to zero!",
					new[] { nameof(LocationID), nameof(Order) }
				);
			}
		}
	}
}
