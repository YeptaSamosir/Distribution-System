using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Client.Repository.Interface
{
    public interface IGenericRepository<TEntity, TKey>
    where TEntity : class
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> Get(TKey key);
        string Post(TEntity entity);
        string Put(TEntity entity);
        HttpStatusCode Delete(TKey key);
    }

}