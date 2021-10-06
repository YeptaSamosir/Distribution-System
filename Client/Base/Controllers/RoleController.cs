using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;

namespace Client.Base.Controllers
{
    [Authorize(Roles = "Super Administrator")]
    public class RoleController : BaseController<Role, RoleRepository, string>
    {
        private readonly RoleRepository repository;
        public RoleController(RoleRepository repository) : base(repository)
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