using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repository.Data;

namespace API.Base.Controllers
{
    public class CompanyController : BaseController<Company, CompanyRepository, int>
    {
        public CompanyController(CompanyRepository repository) : base(repository)
        {
        }
    }
}