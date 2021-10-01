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
            var accountData = accountRepository.FindUsername(loginVM.Username);
            if (accountData == null) {
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
            if (getRole == null) {
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

        [HttpPost("register")]
        public ActionResult Register(AccountRegisterVM accountRegisterVM)
        {
            try
            {
                //check validate
                var response = accountRepository.ValidationUnique(accountRegisterVM.Username, accountRegisterVM.Email);
                if (response != null)
                {
                    return BadRequest(response);
                }

                //insert user to database
                if (accountRepository.Register(accountRegisterVM) == 1)
                {
                    return Ok("Berhasil Register");
                };
                return BadRequest("Gagal Register");
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
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
            if (accountRepository.ForgotPassword(forgotPassword)) {
                return Ok("Reset Password Reset berhasil, Check email ${forgotPassword.Email}");
            }
            return BadRequest("Reset Password Gagal, Hubungi admin!");
        }
    }
}

