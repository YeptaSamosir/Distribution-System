using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Base.Controllers
{
    public class RoleController : BaseController<Role, RoleRepository, string>
    {
        private readonly RoleRepository roleRepository;
        public RoleController(RoleRepository repository, RoleRepository roleRepository) : base(repository)
        {
            this.roleRepository = roleRepository;
        }
    }
}