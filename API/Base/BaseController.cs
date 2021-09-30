using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Models.ViewModels;
using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Base
{
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
            if (Entity == null)
            {
                return Ok(new ResponseVM
                {
                    Result = Entity,
                    Message = "Data tidak ditemukan"
                });
            }
            return Ok(new ResponseVM
            {
                Result = Entity,
                Message = "Data ditemukan"
            });
        }

        [HttpGet("{key}")]
        public ActionResult Get(TKey key)
        {
            var Entity = repository.Get(key);
            if (Entity == null)
            {
                return Ok(new ResponseVM
                {
                    Result = Entity,
                    Message = "Data tidak ditemukan"
                });
            }
            return Ok(new ResponseVM
            {
                Result = Entity,
                Message = "Data Not Found"
            });
        }

        [HttpPost]
        public ActionResult Insert(TEntity entity)
        {
            try
            {
                repository.Insert(entity);
                return Ok(new ResponseVM
                {
                    Message = "Success created"
                });
            } 
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseVM
                {
                    Message = e.Message
                });
            }
        }

        [HttpPut]
        public ActionResult Update(TEntity entity)
        {
            try
            {
                repository.Update(entity);
                return Ok(new ResponseVM
                {
                    Message = "Success updated"
                });
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseVM
                {
                    Message = e.Message
                });
            }
        }

        [HttpDelete]
        public ActionResult Delete(TKey key)
        {
            try
            {
                repository.Delete(key);
                return Ok(new ResponseVM
                {
                    Message = "Success Deleted"
                });
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseVM
                {
                    Message = e.Message
                });
            }
        }
    }
}