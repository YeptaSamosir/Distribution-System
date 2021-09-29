using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Base.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController<Account, AccountRepository, int>
    {
        public AccountController(AccountRepository repository) : base(repository)
        {
        }
    }
}