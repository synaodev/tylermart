using System.ComponentModel.DataAnnotations;

namespace TylerMart.Client.Models {
	public class LoginViewModel {
		[Required(ErrorMessage = "Email is required!")]
		[EmailAddress(ErrorMessage = "Email must be in a proper format!")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is required!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public override string ToString() {
			string result = "Login = {";
			if (!string.IsNullOrEmpty(Email)) {
				result += $"\n\tEmail = {Email}";
			}
			if (!string.IsNullOrEmpty(Password)) {
				result += $"\n\tPassword = {Password}";
			}
			result += "}";
			return result;
		}
	}
}
