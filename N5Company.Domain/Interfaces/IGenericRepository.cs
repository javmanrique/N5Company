using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace N5Company.Domain.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		T GetById(int id);
		List<T> GetAll();
		List<T> GetAll(params Expression<Func<T, object>>[] includeProperties);
		Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
		List<T> Find(Expression<Func<T, bool>> expression);
		List<T> FindWithIncludes(Expression<Func<T, bool>> expression, string[]? includes);
		T Create(T entity);
		int Count(Expression<Func<T, bool>>? predicate = null);
		void AddRange(IEnumerable<T> entities);
		void Remove(int id);
		void RemoveRange(IEnumerable<T> entities);
		T Update(T entity);
	}
}
