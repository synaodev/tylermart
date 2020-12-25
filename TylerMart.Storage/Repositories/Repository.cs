using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using TylerMart.Domain.Models;

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
		/// <summary>
		/// Get all rows for model
		/// </summary>
		/// <returns>
		/// Returns list of rows
		/// </returns>
		public virtual List<T> All() => Db.Set<T>().ToList();
		/// <summary>
		/// Check for row with a particular key
		/// </summary>
		/// <returns>
		/// Returns boolean value
		/// </returns>
		public virtual bool Exists(int ID) => Db.Set<T>().Any(m => m.GetID() == ID);
		/// <summary>
		/// Get one row using key
		/// </summary>
		/// <returns>
		/// Returns single row or null
		/// </returns>
		public virtual T Get(int ID) => Db.Set<T>().SingleOrDefault(m => m.GetID() == ID);
		/// <summary>
		/// Add row to database
		/// </summary>
		/// <returns>
		/// Returns 'true' if input data is valid
		/// </returns>
		public virtual bool Create(T model) {
			Db.Set<T>().Add(model);
			return Db.SaveChanges() >= 1;

		}
		/// <summary>
		/// Updates row in database
		/// </summary>
		/// <returns>
		/// Returns 'true' if updated columns are valid
		/// </returns>
		public virtual bool Update(T model) {
			Db.Set<T>().Update(model);
			return Db.SaveChanges() >= 1;
		}
		/// <summary>
		/// Removes row from database
		/// </summary>
		/// <returns>
		/// Returns 'true' if row has been removed
		/// </returns>
		public virtual bool Delete(T model) {
			Db.Set<T>().Remove(model);
			return Db.SaveChanges() >= 1;
		}
	}
}
