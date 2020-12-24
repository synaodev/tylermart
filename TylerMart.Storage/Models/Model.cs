
namespace TylerMart.Storage.Models {
	public interface IModel {
		int GetID();
	}
	public abstract class Model : IModel {
		/// <summary>
		/// Abstract method that gets ID
		/// </summary>
		/// <returns>
		/// Returns 32-bit ID
		/// </returns>
		public abstract int GetID();
	}
}
