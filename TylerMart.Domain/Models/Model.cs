using System.ComponentModel.DataAnnotations;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Generic abstract model class
	/// </summary>
	public abstract class Model {
		/// <summary>
		/// Primary key
		/// </summary>
		[Key]
		public int ID { get; set; }
	}
}
