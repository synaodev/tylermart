using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using TylerMart.Domain.Models;

namespace TylerMart.Client.Models {
	public class RegisterViewModel : IValidatableObject {
		[Required(ErrorMessage = "First name is required!")]
		[MinLength(2, ErrorMessage = "First name must be at least two characters long!")]
		[DataType(DataType.Text)]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last name is required!")]
		[MinLength(2, ErrorMessage = "Last name must be at least two characters long!")]
		[DataType(DataType.Text)]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Email is required!")]
		[EmailAddress(ErrorMessage = "Email must be in a proper format!")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is required!")]
		[MinLength(8, ErrorMessage = "Passsword must be at least eight characters long!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Please confirm your password!")]
		[DataType(DataType.Password)]
		public string PasswordConfirmation { get; set; }
		[Required(ErrorMessage = "Address is required!")]
		[MinLength(5, ErrorMessage = "Address must be at least five characters long!")]
		[DataType(DataType.Text)]
		public string Address { get; set; }
		public Customer Create() {
			return new Customer() {
				FirstName = this.FirstName,
				LastName = this.LastName,
				Email = this.Email,
				Password = this.Password,
				Address = this.Address
			};
		}
		public IEnumerable<ValidationResult> Validate(ValidationContext context) {
			if (Password != PasswordConfirmation) {
				yield return new ValidationResult(
					"Password has been entered inconsistently!",
					new[] {
						nameof(PasswordConfirmation),
						nameof(Password),
						nameof(RegisterViewModel)
					}
				);
			}
		}
		public override string ToString() {
			string result = "Register = {";
			if (!string.IsNullOrEmpty(FirstName)) {
				result += $"\n\tFirstName = {FirstName}";
			}
			if (!string.IsNullOrEmpty(LastName)) {
				result += $"\n\tLastName = {LastName}";
			}
			if (!string.IsNullOrEmpty(Email)) {
				result += $"\n\tEmail = {Email}";
			}
			if (!string.IsNullOrEmpty(Password)) {
				result += $"\n\tPassword = {Password}";
			}
			if (!string.IsNullOrEmpty(PasswordConfirmation)) {
				result += $"\n\tPasswordConfirmation = {PasswordConfirmation}";
			}
			if (!string.IsNullOrEmpty(Address)) {
				result += $"\n\tAddress = {Address}";
			}
			result += "}";
			return result;
		}
	}
}
