using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class GenericRepository<TContext, TEntity, TKey> : IGenericRepository<TEntity, TKey>
    where TEntity : class
    where TContext : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(MyContext myContext)
        {
            this.myContext = myContext;
            dbSet = myContext.Set<TEntity>();
        }

        IEnumerable<TEntity> IGenericRepository<TEntity, TKey>.GetAll()
        {
            if (dbSet.ToList().Count == 0)
            {
                return null;
            }
            return dbSet.ToList();
        }

        TEntity IGenericRepository<TEntity, TKey>.Get(TKey key)
        {
            var data = dbSet.Find(key);
            if (data != null)
            {
                return data;
            }
            throw new ArgumentNullException();
        }

        int IGenericRepository<TEntity, TKey>.Insert(TEntity entity)
        {
            dbSet.Add(entity);
            return myContext.SaveChanges();
        }

        int IGenericRepository<TEntity, TKey>.Update(TEntity entity)
        {
            myContext.Entry(entity).State = EntityState.Modified;
            return myContext.SaveChanges();
        }

        int IGenericRepository<TEntity, TKey>.Delete(TKey key)
        {
            var data = dbSet.Find(key);
            if (data != null)
            {
                dbSet.Remove(data);
                return myContext.SaveChanges();
            }
            throw new ArgumentNullException();
        }
        public int Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception();
            }
            myContext.Entry(entity).State = EntityState.Modified;
            var update = myContext.SaveChanges();

            return update;
        }
    }
}