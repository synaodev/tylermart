using System.ComponentModel.DataAnnotations;

namespace TylerMart.Client.Models {
	/// <summary>
	/// Login View Model
	/// </summary>
	public class LoginViewModel {
		/// <summary>
		/// Email
		/// </summary>
		[Required(ErrorMessage = "Email is required!")]
		[EmailAddress(ErrorMessage = "Email must be in a proper format!")]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email")]
		public string Email { get; set; }
		/// <summary>
		/// Password
		/// </summary>
		[Required(ErrorMessage = "Password is required!")]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }
		/// <summary>
		/// Login View Model prettifier
		/// </summary>
		/// <returns>
		/// Model as a string
		/// </returns>
		public override string ToString() {
			string result = "Login = {";
			if (!string.IsNullOrEmpty(Email)) {
				result += $"\n\tEmail = {Email}";
			}
			if (!string.IsNullOrEmpty(Password)) {
				result += $"\n\tPassword = {Password}";
			}
			result += "\n}";
			return result;
		}
	}
}
