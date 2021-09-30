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
    public class CompanyController : BaseController<Company, CompanyRepository, int>
    {
        private readonly CompanyRepository repository;
        public CompanyController(CompanyRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}