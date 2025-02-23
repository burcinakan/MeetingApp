using MeetingApp.Model.Models;
using System.Linq.Expressions;

namespace MeetingApp.Api.Repositories
{
	public interface IBaseRepository<T> where T : BaseEntity
	{
		List<T> GetAll();
		IQueryable<X> Select<X>(Expression<Func<T, X>> exp);
		List<T> Where(Expression<Func<T, bool>> exp);

		Task<T> FindAsync(Guid id);
		Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> exp);
		Task AddAsync(T entity);
		Task DeleteAsync(T entity);
		Task UpdateAsync(T entity);
	}
}
