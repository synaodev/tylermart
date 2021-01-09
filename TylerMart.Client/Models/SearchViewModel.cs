using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TylerMart.Client.Models {
	public class SearchViewModel : IValidatableObject {
		[DataType(DataType.Text)]
		[Display(Name = "First name")]
		public string FirstName { get; set; }
		[DataType(DataType.Text)]
		[Display(Name = "Last name")]
		public string LastName { get; set; }
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
