
namespace TylerMart.Storage.Models {
	public interface IModel {
		int GetID();
	}
	public abstract class Model : IModel {
		public abstract int GetID();
	}
}
