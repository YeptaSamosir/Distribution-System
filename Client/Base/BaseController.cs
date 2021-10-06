using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Client.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Base
{
    [Authorize]
    [Route("admin/[controller]")]
    public class BaseController<TEntity, TRepository, TKey> : Controller
    where TEntity : class
    where TRepository : IGenericRepository<TEntity, TKey>
    {
        private readonly TRepository repository;

        public BaseController(TRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("get")]
        public async Task<JsonResult> GetAll()
        {
            var result = await repository.GetAll();
            return Json(result);
        }

        [HttpGet("get/{key}")]
        public async Task<JsonResult> Get(TKey key)
        {
            var result = await repository.Get(key);
            return Json(result);
        }

        [HttpPost("post")]
        public JsonResult Post(TEntity entity)
        {
            var result = repository.Post(entity);

            return Json(result);
        }

        [HttpPut("update")]
        public JsonResult Put(TEntity entity)
        {
            var result = repository.Put(entity);
            return Json(result);
        }

        [HttpDelete("delete/{key}")]
        public JsonResult Delete(TKey key)
        {
            var result = repository.Delete(key);

            return Json(result);
        }

    }
}