using SP_ASPNET_1.DbFiles.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace SP_ASPNET_1.DbFiles.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }
        void Remove(T entity);
        void Insert(T entity);
        void Update(T entityToUpdate);
        T GetByID(object ID);
        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
    }

    public class BaseRepository<T>: IRepository<T> where T : class
    {
        protected readonly IceCreamBlogContext _context;

        protected readonly DbSet<T> _dbSet;

        public BaseRepository(IceCreamBlogContext context)
        {
            this._context = context;
            this._dbSet = this._context.Set<T>();
        }
        public IQueryable<T> Entities => this._dbSet;

        public void Insert(T entity)
        {
            this._dbSet.Add(entity);
        }

        public T GetByID(object ID)
        {
            return this._dbSet.Find(ID);
        }

        /// <summary>
        /// Gets specific entities and their navigation properties.
        /// </summary>
        /// <param name="filter">Query filter</param>
        /// <param name="orderBy">Entity property</param>
        /// <param name="includeProperties">Comma separated property names.
        /// <code>"prop1, prop2"</code>
        /// </param>
        /// <returns>Database entities</returns>
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T> , IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        /// <summary>
        /// Gets specific entities and their navigation properties.
        /// </summary>
        /// <param name="filter">Query filter</param>
        /// <param name="orderBy">Entity property</param>
        /// <param name="includeProperties">Comma separated property names.
        /// <code>"prop1, prop2"</code>
        /// </param>
        /// <returns>Database entities</returns>
        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public void Update(T entity)
        {
            this._dbSet.Attach(entity);
            this._context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(object ID)
        {
            T entitydeletionSubject = this._dbSet.Find(ID);
            this._dbSet.Remove(entitydeletionSubject);
        }

        public void Remove(T entity)
        {
            if (this._context.Entry(entity).State == EntityState.Detached)
            {
                this._dbSet.Attach(entity);
            }
            this._dbSet.Remove(entity);
        }
    }
}