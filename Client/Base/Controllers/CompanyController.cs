using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace Client.Base.Controllers
{
    public class CompanyController : BaseController<Company, CompanyRepository, int>
    {
        private readonly CompanyRepository repository;
        public CompanyController(CompanyRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}