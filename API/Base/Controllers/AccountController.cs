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
using Microsoft.AspNetCore.Authorization;

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

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var accountData = accountRepository.FindUsernameOrEmail(loginVM.Username);
            if (accountData == null)
            {
                return BadRequest(new JWTokenVM { Message = "Account not found" });
            }

            if (accountData.IsActive == false)
            {
                return BadRequest(new JWTokenVM { Message = "Your account is blocked, contact the admin!" });
            }

            //check password BCrypt
            if (!BCrypt.Net.BCrypt.Verify(loginVM.Password, accountData.Password))
            {
                var updateAttemptCount = accountData.AttemptCount + 1;
                accountData.AttemptCount = updateAttemptCount;
                accountRepository.Update(accountData);
                                
                if (updateAttemptCount >= 4) {
                    accountData.AttemptCount = 0;
                    //daactive account login
                    if (accountRepository.DeactivateLoginAccount(accountData) == 1)
                    {
                        return BadRequest(new JWTokenVM { Message = "Your account is blocked, contact the admin!" });
                    }
                    else {
                        return StatusCode((int)HttpStatusCode.InternalServerError);
                    }
                }

                return BadRequest(new JWTokenVM { Message = $"Password wrong! ({4 - updateAttemptCount} more tries!)" });
            }

            //update attempt count if success login
            accountData.AttemptCount = 0;
            accountRepository.Update(accountData);

            //------Create Token----//


            // getRole
            var getRole = accountRepository.getRole(accountData.AccountId);
            if (getRole == null)
            {
                return BadRequest(new JWTokenVM { Message = "Role not found on this account" });
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
                    return Ok(new {Message = "Account registration successful" });
                };
                return BadRequest(new {Message = "Account registration failed" });
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
                    return Ok(new { Message = "Update Successful" });
                };
                return BadRequest(new { Message = "Update failed" });
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = e.Message });
            }
        }

        [HttpPut("profile/update")]
        public ActionResult ProfileUpdate(ProfileUpdateVM profileUpdate)
        {
            try
            {
                //insert user to database
                if (accountRepository.UpdateAccountProfile(profileUpdate) == 1)
                {
                    return Ok(new { Message = "Update Successful" });
                };
                return BadRequest(new { Message = "Update failed" });
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = e.Message });
            }
        }

        [HttpPut("changepassword")]
        public ActionResult ChangePassword(ChangePassword changePassword)
        {
            try
            {
                var action = accountRepository.ChangePassword(changePassword);
                if (action == 1)
                {
                    return Ok(new { Message = "Account password has been successfully updated" });
                }
                else if (action == 2)
                {
                    return BadRequest(new { Message = "Your password is wrong" });
                }
                else
                {
                    return BadRequest(new { Message = "Your email is not registered" });
                }
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,new { Message = e.Message });
            }
        }


        [AllowAnonymous]
        [HttpPost("forgotpassword")]
        public ActionResult ForgotPassword(ForgotPassword forgotPassword)
        {
            if (accountRepository.ForgotPassword(forgotPassword))
            {
                return Ok($"Check your email {forgotPassword.Email} for reset password");
            }
            return BadRequest("Change password Account failed");
        }

        [AllowAnonymous]
        [HttpPost("resetpasswordaccount")]
        public ActionResult ResetPasswordAccount(ResetPasswordVM resetPasswordVM)
        {
            if (accountRepository.ResetPasswordAccount(resetPasswordVM) == 1)
            {
                return Ok($"Account password has been successfully updated");
            }
            return BadRequest("Account password has failed");
        }
    }
}