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
        public AccountController(IConfiguration config, AccountRepository repository, MyContext myContext) : base(repository)
        {
            this.accountRepository = repository;
            this.configuration = config;
            this.myContext = myContext;
        }

        [HttpPost("login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var action = accountRepository.Login(loginVM);
            if (action == 1)
            {
                var data = (from a in myContext.Accounts
                            join b in myContext.AccountRoles on
                            a.AccountId equals b.AccountId
                            join c in myContext.Roles on
                            b.RoleId equals c.RoleId
                            where a.Username == $"{loginVM.Username}"
                            select new RoleVM
                            {
                                Username = a.Username,
                                Role = c.Name
                            }).ToList();

                var claim = new List<Claim>();
                claim.Add(new Claim("Username", data[0].Username));
                foreach (var d in data)
                {
                    claim.Add(new Claim("Roles", d.Role));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                                                 configuration["Jwt:Audience"],
                                                 claim, expires: DateTime.UtcNow.AddDays(1),
                                                 signingCredentials: signIn);

                return Ok(new ResponseVM
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Message = "Login Success !"
                });


            }
            else if (action == 2)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Bad Req",
                });
            }
            else
            {
                return Ok(new ResponseVM
                {
                    Message = "Data tidak ditemukan"
                });
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
                        message = "Username Sudah Digunakan"
                    });
                }
                else if (status == 202)
                {
                    return BadRequest(new
                    {

                        status = HttpStatusCode.BadRequest,
                        message = "Email Sudah Digunakan"
                    });
                }
                else
                {
                    return Ok(new
                    {

                        status = HttpStatusCode.BadRequest,
                        message = "Berhasil Mendaftar"
                    });
                }
            }
            catch
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Username Sudah Digunakan"
                });

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
                    return Ok(new
                    {
                        data = action,
                        status = HttpStatusCode.OK,
                        message = "Password Berhasil Diubah"
                    });
                }
                else if (action == 0)
                {
                    return NotFound(new
                    {
                        data = action,
                        status = HttpStatusCode.OK,
                        message = "Password Anda Salah"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        data = action,
                        status = HttpStatusCode.OK,
                        message = "Email tidak terdaftar"
                    });
                }
            }
            catch
            {

                return BadRequest(new
                {
                    status = HttpStatusCode.OK,
                    message = "Password Confirmasi anda tidak sama"
                });
            }
        }


        [HttpPost("forgotpassword")]
        public ActionResult ForgotPassword(ForgotPassword forgotPassword)
        {

            accountRepository.ForgotPassword(forgotPassword);
            return StatusCode((int)HttpStatusCode.Created, new
            {
                status = HttpStatusCode.OK,
                message = "Cek email untuk password baru"
            });

        }
    }
}

