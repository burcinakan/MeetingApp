using MeetingApp.Api.Repositories;
using MeetingApp.DAL.Contexts;
using MeetingApp.Model.Models;
using System.Linq.Expressions;

namespace MeetingApp.Context.Repositories
{
	public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
	{
		private readonly SQLContext _context;

		public BaseRepository(SQLContext context)
		{
			_context = context;
		}

		public void Add(T entity)
		{
			_context.Set<T>().Add(entity);
			_context.SaveChanges();
		}

		public void Delete(T entity)
		{
			_context.Set<T>().Remove(entity);
			entity.Status = Model.Enums.DataStatus.Deleted;
			_context.SaveChanges();
		}

		public T Find(Guid id)
		{
			return _context.Set<T>().Find(id);
		}

		public T FirstOrDefault(Expression<Func<T, bool>> exp)
		{
			return _context.Set<T>().FirstOrDefault(exp);
		}

		public List<T> GetAll()
		{
			return _context.Set<T>().ToList();
		}

		public IQueryable<X> Select<X>(Expression<Func<T, X>> exp)
		{
			return _context.Set<T>().Select(exp);
		}

		public void Update(T entity)
		{
			entity.Status = Model.Enums.DataStatus.Updated;
			entity.ModifiedDate = DateTime.Now;
			T unchangedEntity = Find(entity.ID);
			_context.Entry(unchangedEntity).CurrentValues.SetValues(entity);
			_context.SaveChanges();
		}

		public List<T> Where(Expression<Func<T, bool>> exp)
		{
			return _context.Set<T>().Where(exp).ToList();
		}
	}
}
