using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repository.Data;

namespace API.Base.Controllers
{
    public class StatusController : BaseController<Status, StatusRepository, string>
    {
        public StatusController(StatusRepository repository) : base(repository)
        {
        }
    }
}