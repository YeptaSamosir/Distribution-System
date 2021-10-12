using API.Models;
using API.Models.ViewModels;
using Client.Models;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Base.Controllers
{
    public class SettingController : BaseController<Account,AccountRepository , int>
    {
        private readonly AccountRepository repository;
        public SettingController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectToAction("profile");
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            string getAccountId = HttpContext.Session.GetString("LogID");
            var account = await repository.Get(int.Parse(getAccountId));

            ViewBag.AccountId = account.AccountId;
            ViewBag.AccountName = account.Name;
            ViewBag.AccountUsername = account.Username;
            ViewBag.AccountEmail = account.Email;
            ViewBag.AccountPassword = account.Password;

            return View();
        }

        [HttpGet("change-password")]
        public async Task<IActionResult> ChangePassword()
        {
            ViewBag.AccountEmail = HttpContext.Session.GetString("LogEmail");

            return View();
        }

        [HttpPut("change-password")]
        public JsonResult ChangePassword(ChangePassword changePassword)
        {
            var result = repository.ChangePassword(changePassword);

            return Json(result);
        }

        [HttpPut("account/update")]
        public JsonResult UpdateAccount(ProfileUpdateVM profileUpdateVM)
        {
            var result = repository.UpdateProfile(profileUpdateVM);
            return Json(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
