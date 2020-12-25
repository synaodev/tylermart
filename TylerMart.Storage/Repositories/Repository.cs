using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using TylerMart.Domain.Models;

namespace TylerMart.Storage.Repositories {
	/// <summary>
	/// Generic repository interface for <see cref="TylerMart.Domain.Models.IModel"/>
	/// </summary>
	public interface IRepository<T> where T : IModel {
		/// <summary>
		/// Get all rows
		/// </summary>
		/// <returns>
		/// List of models
		/// </returns>
		List<T> All();
		/// <summary>
		/// Check for row with primary key
		/// </summary>
		/// <returns>
		/// 'true' if row exists
		/// </returns>
		bool Exists(int ID);
		/// <summary>
		/// Get a row with primary key
		/// </summary>
		/// <param name="ID">Primary Key</param>
		/// <returns>
		/// Single row or null
		/// </returns>
		T Get(int ID);
		/// <summary>
		/// Create new row using model data
		/// </summary>
		/// <param name="model">Model data</param>
		/// <returns>
		/// 'true' if successfully inserted into database
		/// </returns>
		bool Create(T model);
		/// <summary>
		/// Update existing row using model data
		/// </summary>
		/// <param name="model">Model data</param>
		/// <returns>
		/// 'true' if successfully updated in database
		/// </returns>
		bool Update(T model);
		/// <summary>
		/// Remove existing row with model's primary key
		/// </summary>
		/// <param name="model">Model data</param>
		/// <returns>
		/// 'true' if successfully removed from database
		/// </returns>
		bool Delete(T model);
	}
	/// <summary>
	/// Generic repository interface for <see cref="TylerMart.Domain.Models.Model"/>
	/// </summary>
	public abstract class Repository<T> : IRepository<T> where T : Model {
		protected DbContext Db;
		public Repository(DbContext db) {
			Db = db;
		}
		/// <summary>
		/// Get all rows
		/// </summary>
		/// <returns>
		/// List of models
		/// </returns>
		public virtual List<T> All() => Db.Set<T>().ToList();
		/// <summary>
		/// Check for row with primary key
		/// </summary>
		/// <returns>
		/// 'true' if row exists
		/// </returns>
		public virtual bool Exists(int ID) => Db.Set<T>().Any(m => m.GetID() == ID);
		/// <summary>
		/// Get a row with primary key
		/// </summary>
		/// <param name="ID">Primary Key</param>
		/// <returns>
		/// Single row or null
		/// </returns>
		public virtual T Get(int ID) => Db.Set<T>().SingleOrDefault(m => m.GetID() == ID);
		/// <summary>
		/// Update existing row using model data
		/// </summary>
		/// <param name="model">Model data</param>
		/// <returns>
		/// 'true' if successfully updated in database
		/// </returns>
		public virtual bool Create(T model) {
			Db.Set<T>().Add(model);
			return Db.SaveChanges() >= 1;
		}
		/// <summary>
		/// Update existing row using model data
		/// </summary>
		/// <param name="model">Model data</param>
		/// <returns>
		/// 'true' if successfully updated in database
		/// </returns>
		public virtual bool Update(T model) {
			Db.Set<T>().Update(model);
			return Db.SaveChanges() >= 1;
		}
		/// <summary>
		/// Remove existing row with model's primary key
		/// </summary>
		/// <param name="model">Model data</param>
		/// <returns>
		/// 'true' if successfully removed from database
		/// </returns>
		public virtual bool Delete(T model) {
			Db.Set<T>().Remove(model);
			return Db.SaveChanges() >= 1;
		}
	}
}
