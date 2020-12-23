using System.ComponentModel.DataAnnotations;

namespace TylerMart.Storage.Models {
	public interface IModel {
		int GetID();
	}
	public abstract class Model : IModel {
		[Key]
		public int ID { get; set; }
		public virtual int GetID() => ID;
	}
}
