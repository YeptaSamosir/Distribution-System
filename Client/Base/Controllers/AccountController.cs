using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.ViewModels;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Base.Controllers
{
    [Authorize(Roles = "Super Administrator")]
    public class AccountController : BaseController<Account, AccountRepository, int>
    {
        private readonly AccountRepository repository;
        public AccountController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public JsonResult InsertRegister(AccountRegisterVM accountregister)
        {
            var result = repository.InsertRegister(accountregister);
            return Json(result);
        }

        [HttpPut("register/update")]
        public JsonResult UpdateAccount(AccountUpdateWithRole accountUpdateWithRole)
        {
            var result = repository.UpdateAccount(accountUpdateWithRole);
            return Json(result);
        }

    }
}