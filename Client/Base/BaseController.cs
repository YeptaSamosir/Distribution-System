using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Client.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Client.Base
{
    [Route("master/data/[controller]")]
    public class BaseController<TEntity, TRepository, TKey> : Controller
    where TEntity : class
    where TRepository : IGenericRepository<TEntity, TKey>
    {
        private readonly TRepository repository;

        public BaseController(TRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var result = await repository.GetAll();
            return Json(result);
        }

        [HttpGet("{key}")]
        public async Task<JsonResult> Get(TKey key)
        {
            var result = await repository.Get(key);
            return Json(result);
        }

        [HttpPost]
        public JsonResult Post(TEntity entity)
        {
            var result = repository.Post(entity);

            return Json(result);
        }

        [HttpPut]
        public JsonResult Put(TKey key, TEntity entity)
        {
            var result = repository.Put(key, entity);
            return Json(result);
        }

        [HttpDelete("{key}")]
        public JsonResult Delete(TKey key)
        {
            var result = repository.Delete(key);

            return Json(result);
        }

    }
}