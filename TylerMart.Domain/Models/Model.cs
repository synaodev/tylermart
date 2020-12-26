using System.ComponentModel.DataAnnotations;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Generic model interface
	/// </summary>
	public abstract class Model {
		/// <summary>
		/// Primary key
		/// </summary>
		[Key]
		public int ID { get; set; }
	}
}
