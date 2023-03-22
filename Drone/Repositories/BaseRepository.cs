using DronesLoad.DB;
using DronesLoad.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DronesLoad.Repositories
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		protected DronesDBContext _context;
		public BaseRepository(DronesDBContext context)
		{
			_context = context;
		}
		public IEnumerable<T> GetAll()
		{
			return _context.Set<T>().ToList();
		}
		public T GetByID(int id)
		{
			return _context.Set<T>().Find(id);
		}
		public void DeleteByID(int id)
		{
			_context.Remove(_context.Set<T>().Find(id));
			//  return _context.SaveChanges();
		}

		public void DeleteRange(Expression<Func<T, bool>> match)
		{
			_context.RemoveRange(_context.Set<T>().Where(match));
		}
		public T Find(Expression<Func<T, bool>> match)
		{
			return _context.Set<T>().SingleOrDefault(match);
		}
		public IEnumerable<T> FindAll(Expression<Func<T, bool>> match)
		{
			return _context.Set<T>().Where(match).ToList();
		}
		public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] includes = null)
		{
			IQueryable<T> query = _context.Set<T>();
			if (includes != null)
				foreach (var _include in includes)
					query = query.Include(_include);

			return query.Where(match);
		}
		public T Add(T entity)
		{
			_context.Set<T>().Add(entity);
			//   _context.SaveChanges();
			return entity;
		}
		public IEnumerable<T> AddRange(IEnumerable<T> entities)
		{
			_context.Set<T>().AddRange(entities);
			//     _context.SaveChanges();
			return entities;
		}
		public T Update(T entity)
		{
			_context.Update(entity);
			return entity;
		}

	}

}
