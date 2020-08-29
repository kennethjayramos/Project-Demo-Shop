using Demo_Shop.Core.Contracts;
using Demo_Shop.Core.Models;
using System.Data.Entity;
using System.Linq;

namespace Demo_Shop.DataAccess.Sql
{
    public class SqlRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal ProductContext _context;

        internal DbSet<T> _dbSet;

        #region Constructor
        public SqlRepository(ProductContext context)
        {
            this._context = context;

            this._dbSet = context.Set<T>();
        }
        #endregion

        public IQueryable<T> Collection()
        {
            return _dbSet;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var t = Find(id);

            if (_context.Entry(t).State == EntityState.Detached) _dbSet.Attach(t);

            _dbSet.Remove(t);
        }

        public T Find(string id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T t)
        {
            _dbSet.Add(t);
        }

        public void Update(T t)
        {
            _dbSet.Attach(t);

            _context.Entry(t).State = EntityState.Modified;
        }
    }
}