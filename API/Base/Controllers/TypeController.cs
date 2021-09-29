using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repository.Data;

namespace API.Base.Controllers
{
    public class TypeStatusController : BaseController<TypeStatus, TypeStatusRepository, string>
    {
        public TypeStatusController(TypeStatusRepository repository) : base(repository)
        {
        }
    }
}