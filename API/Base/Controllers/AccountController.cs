using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.Models.ViewModels;
using API.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace API.Base.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController<Account, AccountRepository, int>
    {
        private readonly MyContext myContext;
        private readonly AccountRepository accountRepository;
        public IConfiguration configuration;
        public AccountController(IConfiguration config, AccountRepository accountRepository) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this.configuration = config;
            this.myContext = myContext;
        }

        [HttpPost("login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var accountData = accountRepository.FindUsernameOrEmail(loginVM.Username);
            if (accountData == null)
            {
                return BadRequest(new JWTokenVM { Message = "Pengguna tidak ditemukan!" });
            }

            if (accountData.IsActive == false)
            {
                return BadRequest(new JWTokenVM { Message = "Account anda terblokir, hubungi admin!" });
            }

            //check password BCrypt
            if (!BCrypt.Net.BCrypt.Verify(loginVM.Password, accountData.Password))
            {
                var login = Convert.ToInt32(HttpContext.Session.GetInt32("LoginCount")) + 1;
               
                HttpContext.Session.SetInt32("LoginCount", login);

                var loginCountCurrent = 3 - HttpContext.Session.GetInt32("LoginCount");

                if (loginCountCurrent == 0) {
                    //daactive account login
                    if (accountRepository.DeactivateLoginAccount(accountData) == 1)
                    {
                        return BadRequest(new JWTokenVM { Message = "Account anda terblokir, hubungi admin!" });
                    }
                    else {
                        return StatusCode((int)HttpStatusCode.InternalServerError);
                    }
                }

                return BadRequest(new JWTokenVM { Message = $"Password salah! (tersisa {loginCountCurrent} kali lagi!)" });
            }


           

            //------Create Token----//


            // getRole
            var getRole = accountRepository.getRole(accountData.AccountId);
            if (getRole == null)
            {
                return BadRequest(new JWTokenVM { Message = "Role tidak ditemukan pada akun ini" });
            }

            //create claims details based on the user information
            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, accountData.Email));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, accountData.Name));
            identity.AddClaim(new Claim("AccountId", accountData.AccountId.ToString()));
            foreach (var item in getRole)
            {
                identity.AddClaim(new Claim("role", item.Role));
            }
          
            //create token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"], configuration["Jwt:Audience"],
                identity.Claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signIn
            );
            
            return Ok(new JWTokenVM
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            });

        }

        [HttpPost("register")]
        public ActionResult Register(AccountRegisterVM accountRegisterVM)
        {
            try
            {
                //check validate
                var response = accountRepository.ValidationUnique(accountRegisterVM.Username, accountRegisterVM.Email);
                if (response != null)
                {
                    return BadRequest(new { Message = response });
                }

                //insert user to database
                if (accountRepository.Register(accountRegisterVM) == 1)
                {
                    return Ok(new {Message = "Berhasil Register" });
                };
                return BadRequest(new { Message = "Gagal Register" });
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = e.Message });
            }
        }

        [HttpPut("register/update")]
        public ActionResult AccountUpdate(AccountUpdateWithRole accountUpdateWithRole)
        {
            try
            {
                 //insert user to database
                if (accountRepository.UpdateAccount(accountUpdateWithRole) == 1)
                {
                    return Ok(new { Message = "Berhasil Update" });
                };
                return BadRequest(new { Message = "Gagal Update" });
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = e.Message });
            }
        }

        [HttpPost("changepassword")]
        public ActionResult ChangePassword(ChangePassword changePassword)
        {
            try
            {
                var action = accountRepository.ChangePassword(changePassword);
                if (action == 1)
                {
                    return Ok("Password Berhasil Diubah");
                }
                else if (action == 2)
                {
                    return BadRequest("Password Anda Salah");
                }
                else
                {
                    return BadRequest("Email tidak terdaftar");
                }
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [HttpPost("forgotpassword")]
        public ActionResult ForgotPassword(ForgotPassword forgotPassword)
        {
            if (accountRepository.ForgotPassword(forgotPassword))
            {
                return Ok($"Reset Password berhasil, Check email {forgotPassword.Email}");
            }
            return BadRequest("Reset Password Gagal!");
        }
    }
}

