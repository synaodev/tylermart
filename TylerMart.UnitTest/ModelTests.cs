using Xunit;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using TylerMart.Domain.Models;

namespace TylerMart.UnitTest {
	/// <summary>
	/// Tests for <see cref="TylerMart.Domain.Models.Model"/>
	/// </summary>
	/// <remarks>
	/// Derived classes are also tested
	/// </remarks>
	public class ModelsTests {
		/// <summary>
		/// Customer validation for [Required]
		/// </summary>
		[Fact]
		public void TestValidateCustomerRequired() {
			Customer customer = new Customer();
			ValidationContext context = new ValidationContext(customer);
			List<ValidationResult> results = new List<ValidationResult>();

			bool valid = Validator.TryValidateObject(customer, context, results);
			Assert.False(valid);
			Assert.Equal("First name is required!", results[0].ErrorMessage);
			Assert.Equal("Last name is required!", results[1].ErrorMessage);
			Assert.Equal("Email address is required!", results[2].ErrorMessage);
			Assert.Equal("Password is required!", results[3].ErrorMessage);
			Assert.Equal("Real address is required!", results[4].ErrorMessage);
		}
		/// <summary>
		/// Location validation for [Required]
		/// </summary>
		[Fact]
		public void TestValidateLocationRequired() {
			Location location = new Location();
			ValidationContext context = new ValidationContext(location);
			List<ValidationResult> results = new List<ValidationResult>();

			bool valid = Validator.TryValidateObject(location, context, results);
			Assert.False(valid);
			Assert.Equal("Name is required!", results[0].ErrorMessage);
		}
		/// <summary>
		/// Product validation for [Required]
		/// </summary>
		[Fact]
		public void TestValidateProductRequired() {
			Product product = new Product();
			ValidationContext context = new ValidationContext(product);
			List<ValidationResult> results = new List<ValidationResult>();

			bool valid = Validator.TryValidateObject(product, context, results);
			Assert.False(valid);
			Assert.Equal("Name is required!", results[0].ErrorMessage);
			Assert.Equal("Description is required!", results[1].ErrorMessage);
		}
		/// <summary>
		/// Product validation for Price
		/// </summary>
		[Fact]
		public void TestValidateProductPrice() {
			Product product = new Product();
			ValidationContext context = new ValidationContext(product);

			List<ValidationResult> results = product.Validate(context).ToList();
			Assert.NotEmpty(results);
			Assert.Equal("Price cannot be less than or equal to zero!", results[0].ErrorMessage);
		}
		/// <summary>
		/// Order validation for CustomerID + LocationID
		/// </summary>
		[Fact]
		public void TestValidateOrderCustomerLocation() {
			Order order = new Order();
			ValidationContext context = new ValidationContext(order);

			List<ValidationResult> results = order.Validate(context).ToList();
			Assert.NotEmpty(results);
			Assert.Equal("CustomerID cannot be less than or equal to zero!", results[0].ErrorMessage);
			Assert.Equal("LocationID cannot be less than or equal to zero!", results[1].ErrorMessage);
		}
		/// <summary>
		/// LocationProduct validation for LocationID + ProductID
		/// </summary>
		[Fact]
		public void TestValidateLocationProductLocationIDProductID() {
			LocationProduct lp = new LocationProduct();
			ValidationContext context = new ValidationContext(lp);

			List<ValidationResult> results = lp.Validate(context).ToList();
			Assert.NotEmpty(results);
			Assert.Equal("LocationID cannot be less than or equal to zero!", results[0].ErrorMessage);
			Assert.Equal("ProductID cannot be less than or equal to zero!", results[1].ErrorMessage);
		}
		/// <summary>
		/// OrderProduct validation for OrderID + ProductID
		/// </summary>
		[Fact]
		public void TestValidateOrderProductOrderIDProductID() {
			OrderProduct op = new OrderProduct();
			ValidationContext context = new ValidationContext(op);

			List<ValidationResult> results = op.Validate(context).ToList();
			Assert.NotEmpty(results);
			Assert.Equal("OrderID cannot be less than or equal to zero!", results[0].ErrorMessage);
			Assert.Equal("ProductID cannot be less than or equal to zero!", results[1].ErrorMessage);
		}
	}
}
