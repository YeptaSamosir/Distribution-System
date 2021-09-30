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
using API.Helper;

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
        }

        [HttpPost("login")]
        public ActionResult Login(LoginVM loginVM)
        {
            try
            {
                //check data by username
                var accountData = accountRepository.FindUsername(loginVM.Username);
                if (accountData == null)
                {
                    return BadRequest("Pengguna tidak ditemukan!");
                }

                //check password BCrypt
                if (!BCrypt.Net.BCrypt.Verify(loginVM.Password, accountData.Password))
                {
                    return BadRequest("Password salah!");
                }

                //------Create Token----//

                // getRole
                var getRole = accountRepository.getRole(accountData.AccountId);
                if (getRole == null)
                {
                    return BadRequest("Role tidak ditemukan pada akun ini");
                }

                //create claims details based on the user information
                var identity = new ClaimsIdentity("JWT");
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]));
                identity.AddClaim(new Claim("email", accountData.Email));
                identity.AddClaim(new Claim("username", accountData.Username));
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
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost("register")]
        public ActionResult Register(AccountRegisterVM accountRegisterVM)
        {
            try
            {
                var status = accountRepository.Register(accountRegisterVM);
                if (status == 200)
                {
                    return Ok(new
                    {

                        status = HttpStatusCode.BadRequest,
                        message = "Pendaftaran Berhasil"
                    });
                }
                else if (status == 201)
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Username Sudah Terdaftar. Gunakan Username yang Lain"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Email Sudah Digunakan. Gunakan Email yang Lain"
                    });
                }
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost("resetPassword")]
        public ActionResult SendPasswordReset(LoginVM loginVM)
        {
            //validating
            if (string.IsNullOrEmpty(loginVM.Email))
            {
                return BadRequest(null);
            }
            try
            {
                //check email
                var account = accountRepository.FindEmail(loginVM.Email);
                if (account == null)
                {
                    return BadRequest("Email tidak terdaftar");
                }

                //Generate Reset password with random alphanumstring
                string passwordReset = Helper.Helper.GetRandomAlphanumericString(8);
                string subjectMail = "Reset Password [" + DateTime.Now + "]";
                //Reset password
                if (accountRepository.ChangePassword(account.AccountId, passwordReset))
                {
                    //send password to email
                    Helper.Helper.SendEmail(loginVM.Email, subjectMail, "Hello "
                                  + loginVM.Email + "<br><br>berikut reset password anda, jangan lupa ganti dengan password baru<br><br><b>"
                                  + passwordReset + "<b><br><br>Thanks<br>netcore-api.com");

                    return Ok("reset Password berhasil dikirim ke email " + loginVM.Email + ".");
                }

                return StatusCode((int)HttpStatusCode.InternalServerError, "Gagal reset password");
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost("changePassword")]
        public ActionResult ChangePassword(LoginVM loginVM)
        {
            //validating
            if (string.IsNullOrEmpty(loginVM.Email) || string.IsNullOrEmpty(loginVM.Password) || string.IsNullOrEmpty(loginVM.NewPassword))
            {
                return BadRequest(null);
            }

            try
            {
                //check email
                var account = accountRepository.FindEmail(loginVM.Email);

                if (account == null)
                {
                    return BadRequest("Email tidak terdaftar");
                }


                //check password match
                if (!BCrypt.Net.BCrypt.Verify(loginVM.Password, account.Password))
                {
                    return BadRequest("Password lama salah");
                }

                //change password
                if (accountRepository.ChangePassword(account.AccountId, loginVM.NewPassword))
                {
                    return Ok("Password berhasil diupdate");
                }
                return StatusCode((int)HttpStatusCode.InternalServerError, "Gagal Change Password");
            }
            catch (System.Exception e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}

