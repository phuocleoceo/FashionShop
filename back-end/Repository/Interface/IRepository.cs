using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace back_end.Repository.Interface
{
	public interface IRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null,
							 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
							 string includeProperties = null);

		Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter = null,
							  string includeProperties = null);

		Task<T> Get(int id);

		Task Add(T entity);

		Task Update(T entity);

		Task Remove(int id);

		Task Remove(T entity);

		Task RemoveRange(IEnumerable<T> entity);
	}
}