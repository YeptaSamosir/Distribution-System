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
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Client.Handler;

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

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Admin");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost("check-login")]
        public async Task<IActionResult> CheckLogin(LoginVM loginVM)
        {
            
            var jwtToken = await repository.Auth(loginVM);
            var token = jwtToken.Token;
            var message = jwtToken.Message;
            if (token == null)
            {
                TempData["Message"] = message;
                return RedirectToAction("login");
            }

            //Reading claims
            string getEmail = JwtHandler.GetClaim(token, JwtRegisteredClaimNames.Email);
            string getName = JwtHandler.GetClaim(token, JwtRegisteredClaimNames.Name);
            string getAccountId = JwtHandler.GetClaim(token, "AccountId");
            
            //set session
            HttpContext.Session.SetString("JWToken", token);
            HttpContext.Session.SetString("LogEmail", getEmail);
            HttpContext.Session.SetString("LogID", getAccountId);
            HttpContext.Session.SetString("LogName", getName);
          
            return RedirectToAction("index", "Admin");
        }

        [HttpGet("logout")]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("login");
        }

        [AllowAnonymous]
        [HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("send-reset-password")]
        public IActionResult SendResetPassword(ForgotPassword forgotPassword)
        {
            var result = repository.SendResetPassword(forgotPassword);
            TempData["Message"] = result;
            return RedirectToAction("forgot-password");
        }

        [AllowAnonymous]
        [HttpPost("reset-password-account")]
        public IActionResult ResetPasswordAccount(ResetPasswordVM resetPasswordVM)
        {
            var result = repository.ResetPasswordAccount(resetPasswordVM);
            TempData["Message"] = result;
            return RedirectToAction("login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
