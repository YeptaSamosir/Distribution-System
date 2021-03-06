using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Models.ViewModels;
using API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Base
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<TEntity, TRepository, TKey> : ControllerBase
    where TEntity : class
    where TRepository : IGenericRepository<TEntity, TKey>
    {
        private readonly TRepository repository;

        public BaseController(TRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var Entity = repository.GetAll();
            return Ok(Entity);
        }

        [HttpGet("{key}")]
        public ActionResult Get(TKey key)
        {
            var Entity = repository.Get(key);
            return Ok(Entity);
        }

        [HttpPost]
        public ActionResult Insert(TEntity entity)
        {
            try
            {
                repository.Insert(entity);
                return Ok(new { Message = "Success Created" });
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = e.Message } );
            }
        }

        [HttpPut]
        public ActionResult Update(TEntity entity)
        {
            try
            {
                repository.Update(entity);
                return Ok(new {Message = "Success Update" });
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = e.Message });
            }
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(TKey key)
        {
            try
            {
                repository.Delete(key);
                return Ok(new {Message = "Success Delete" });
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = e.Message });
            }
        }
    }
}