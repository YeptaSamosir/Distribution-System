using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using API.Models;
using Client.Repository.Data;

namespace Client.Base.Controllers
{
    [Route("[controller]")]
    public class TypeStatusController : BaseController<TypeStatus, TypeStatusRepository, string>
    {
        private readonly TypeStatusRepository repository;
        public TypeStatusController(TypeStatusRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}