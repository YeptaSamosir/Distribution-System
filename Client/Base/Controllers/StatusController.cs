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
    public class StatusController : BaseController<Status, StatusRepository, string>
    {
        private readonly StatusRepository repository;
        public StatusController(StatusRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}