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
    public class RoleController : BaseController<Role, RoleRepository, string>
    {
        private readonly RoleRepository repository;
        public RoleController(RoleRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}