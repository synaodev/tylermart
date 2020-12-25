
namespace TylerMart.Domain.Models {
	/// <summary>
	/// Generic model interface
	/// </summary>
	public interface IModel {
		/// <summary>
		/// Gets primary key
		/// </summary>
		/// <returns>
		/// 32-bit integer
		/// </returns>
		int GetID();
	}
	/// <summary>
	/// Generic model class
	/// </summary>
	public abstract class Model : IModel {
		/// <summary>
		/// Gets primary key
		/// </summary>
		/// <returns>
		/// 32-bit integer
		/// </returns>
		public abstract int GetID();
	}
}
