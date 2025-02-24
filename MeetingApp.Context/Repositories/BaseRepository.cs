using MeetingApp.Api.Repositories;
using MeetingApp.DAL.Contexts;
using MeetingApp.Model.Models;
using Microsoft.EntityFrameworkCore;
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

		public async Task AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(T entity)
		{
			_context.Set<T>().Remove(entity);
			entity.Status = Model.Enums.DataStatus.Deleted;
			await _context.SaveChangesAsync();
		}

		public async Task<T> FindAsync(Guid id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> exp)
		{
			return await _context.Set<T>().FirstOrDefaultAsync(exp);
		}

		public List<T> GetAll()
		{
			return _context.Set<T>().ToList();
		}

		public async Task<List<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public IQueryable<X> Select<X>(Expression<Func<T, X>> exp)
		{
			return _context.Set<T>().Select(exp);
		}

		public async Task UpdateAsync(T entity)
		{


			entity.Status = Model.Enums.DataStatus.Updated;
			entity.ModifiedDate = DateTime.Now;
			var unchangedEntity = await FindAsync(entity.ID);
			_context.Entry(unchangedEntity).CurrentValues.SetValues(entity);
			await _context.SaveChangesAsync();
		}

		public List<T> Where(Expression<Func<T, bool>> exp)
		{
			return _context.Set<T>().Where(exp).ToList();
		}

		public async Task<List<T>> WhereAsync(Expression<Func<T, bool>> exp)
		{
			return await _context.Set<T>().Where(exp).ToListAsync();
		}
	}
}
