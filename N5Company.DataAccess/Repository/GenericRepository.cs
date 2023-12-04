using Microsoft.EntityFrameworkCore;
using N5Company.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace N5Company.DataAccess.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		protected readonly ApplicationDbContext context;
		public GenericRepository(ApplicationDbContext _context)
		{
			this.context = _context;
		}

		/// <summary>
		/// Crea una entidad en base de datos
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public T Create(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			context.Set<T>().Add(entity);
			context.SaveChanges();
			return entity;

		}
		/// <summary>
		/// Creacion masiva de entidades
		/// </summary>
		/// <param name="entities"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public void AddRange(IEnumerable<T> entities)
		{
			if (entities == null)
			{
				throw new ArgumentNullException("entity");
			}
			context.Set<T>().AddRange(entities);
			context.SaveChanges();
		}

		/// <summary>
		/// Devulve la cantidad de registros, segun el predicado.
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public int Count(Expression<Func<T, bool>>? predicate = null)
		{
			var set = context.Set<T>();

			return predicate == null ? set.Count() : set.Count(predicate);

		}
		/// <summary>
		/// Actualizacion masiva de entidades
		/// </summary>
		/// <param name="entities"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public void UpdateRange(IEnumerable<T> entities)
		{
			if (entities == null)
			{
				throw new ArgumentNullException("entity");
			}
			context.Set<T>().UpdateRange(entities);
			context.SaveChanges();
		}
		/// <summary>
		/// Buscador con predicado generico
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public List<T> Find(Expression<Func<T, bool>> expression)
		{
			return context.Set<T>().Where(expression).ToList();
		}

		/// <summary>
		/// Buscador con predicado generico e includes
		/// </summary>
		/// <param name="expression"></param>
		/// <param name="includes"></param>
		/// <returns></returns>
		public List<T> FindWithIncludes(Expression<Func<T, bool>> expression, string[]? includes = null)
		{
			try
			{
				var query = (IQueryable<T>)context.Set<T>();
				if (includes != null)
				{
					foreach (var include in includes)
					{
						query = query.Include(include);
					}
				}

				return query.Where(expression).ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Obtiene todos los registros de una entidad
		/// </summary>
		/// <returns></returns>
		public List<T> GetAll()
		{
			return context.Set<T>().ToList();
		}

		/// <summary>
		/// Obtiene todos los registros de una entidad y sus includes
		/// </summary>
		/// <param name="includes"></param>
		/// <returns></returns>
		public List<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
		{
			try
			{
				IQueryable<T> query = context.Set<T>();

				foreach (var includeProperty in includeProperties)
				{
					query = query.Include(includeProperty);
				}
				return query.ToList();
			}
			catch (Exception)
			{

				throw;
			}

		}
		public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = context.Set<T>();

			if (includes != null)
			{
				query = includes.Aggregate(query, (current, include) => current.Include(include));
			}

			return await query.ToListAsync();
		}

		/// <summary>
		/// Obtiene una entidad por Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public T GetById(int id)
		{
			return context.Set<T>().Find(id);
		}
		public void Remove(int id)
		{
			var collection = context.Set<T>();
			var entity = collection.Find(id);

			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			collection.Remove(entity);
			context.SaveChanges();
		}

		/// <summary>
		/// Elimina de forma masiva
		/// </summary>
		/// <param name="entities"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public void RemoveRange(IEnumerable<T> entities)
		{
			if (entities == null)
			{
				throw new ArgumentNullException("entity");
			}
			context.Set<T>().RemoveRange(entities);
			context.SaveChanges();
		}

		/// <summary>
		/// Actualidad una entidad
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public T Update(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			context.Set<T>().Update(entity);
			context.SaveChanges();

			return entity;
		}
	}
}
