using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    public interface IGenericRepository<TEntity, TKey>
    where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(TKey key);

        int Insert(TEntity entity);
        int Update(TEntity entity);
        int Delete(TKey key);
    }
}