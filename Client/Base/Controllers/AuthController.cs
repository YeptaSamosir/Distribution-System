using API.Models;
using API.Models.ViewModels;
using Client.Models;
using Client.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Base.Controllers
{
    [Route("[controller]")]
    public class AuthController : BaseController<Account, AuthRepository, int>
    {
        private readonly AuthRepository repository;
        public AuthController(AuthRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("login")]
        public IActionResult login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Admin");
            }

            return View();
        }

       [HttpPost("check-login")]
        public async Task<IActionResult> checkLogin(LoginVM loginVM)
        {
            var jwtToken = await repository.Auth(loginVM);
            var token = jwtToken.Token;

            if (token == null)
            {
                return RedirectToAction("login");
            }

            HttpContext.Session.SetString("JWToken", token);
            // HttpContext.Session.SetString("Name", jwtHandler.GetName(token));
            // HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");

            return RedirectToAction("index", "Admin");
        }

        [HttpGet("logout")]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
