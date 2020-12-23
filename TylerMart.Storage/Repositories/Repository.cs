using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using TylerMart.Storage.Models;

namespace TylerMart.Storage.Repositories {
	public interface IRepository<T> where T : IModel {
		List<T> All();
		bool Exists(int ID);
		T Get(int ID);
		bool Create(T model);
		bool Update(T model);
		bool Delete(T model);
	}
	public abstract class Repository<T> : IRepository<T> where T : Model {
		protected DbContext Db;
		public Repository(DbContext db) {
			Db = db;
		}
		public virtual List<T> All() => Db.Set<T>().ToList();
		public virtual bool Exists(int ID) => Db.Set<T>().Any(m => m.GetID() == ID);
		public virtual T Get(int ID) => Db.Set<T>().SingleOrDefault(m => m.GetID() == ID);
		public virtual bool Create(T model) {
			Db.Set<T>().Add(model);
			return Db.SaveChanges() >= 1;
		}
		public virtual bool Update(T model) {
			Db.Set<T>().Update(model);
			return Db.SaveChanges() >= 1;
		}
		public virtual bool Delete(T model) {
			Db.Set<T>().Remove(model);
			return Db.SaveChanges() >= 1;
		}
	}
}
