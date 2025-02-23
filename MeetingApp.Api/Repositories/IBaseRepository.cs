using MeetingApp.Model.Models;
using System.Linq.Expressions;

namespace MeetingApp.Api.Repositories
{
	public interface IBaseRepository<T> where T : BaseEntity
	{
		List<T> GetAll();
		IQueryable<X> Select<X>(Expression<Func<T, X>> exp);
		List<T> Where(Expression<Func<T, bool>> exp);

		T Find(Guid id);
		T FirstOrDefault(Expression<Func<T, bool>> exp);
		void Add(T entity);
		void Delete(T entity);
		void Update(T entity);
	}
}
