using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TylerMart.Client.Models {
	/// <summary>
	/// Search View Model
	/// </summary>
	public class SearchViewModel : IValidatableObject {
		/// <summary>
		/// First Name
		/// </summary>
		[DataType(DataType.Text)]
		[Display(Name = "First name")]
		public string FirstName { get; set; }
		/// <summary>
		/// Last Name
		/// </summary>
		[DataType(DataType.Text)]
		[Display(Name = "Last name")]
		public string LastName { get; set; }
		/// <summary>
		/// Validates SearchViewModel.FirstName and SearchViewModel.LastName
		/// </summary>
		/// <param name="context">Validation context</param>
		/// <returns>
		/// IEnumerable containing validation errors
		/// </returns>
		public IEnumerable<ValidationResult> Validate(ValidationContext context) {
			if (string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName)) {
				yield return new ValidationResult(
					"Must enter either first name, last name, or both!",
					new[] {
						nameof(FirstName),
						nameof(LastName),
						nameof(SearchViewModel)
					}
				);
			}
		}
	}
}
