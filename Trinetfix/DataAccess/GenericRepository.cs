using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GenericRepository<T> where T : class
    {
        DbContext context;
        DbSet<T> dbSet;

        public GenericRepository(DbContext cntxt)
        {
            context = cntxt;
            dbSet = context.Set<T>();
        }

        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            if (context.Entry(entity).State == EntityState.Deleted)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        public virtual IQueryable<T> GetQueryable()
        {
            return dbSet.AsQueryable();
        }
    }
}
