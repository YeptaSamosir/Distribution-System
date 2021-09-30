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
        public AccountController(IConfiguration config, AccountRepository repository) : base(repository)
        {
            this.accountRepository = repository;
            this.configuration = config;
        }

        [HttpPost("login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var action = accountRepository.Login(loginVM);
            if(action == 100)
            {
                return Ok(new ResponseVM
                {
                    Message = "Data tidak ditemukan"
                });

            }
            else if (action == 200)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Bad Req",
                });
            }
            else
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
        }

        [HttpPost("register")]
        public ActionResult Register (AccountRegisterVM accountRegisterVM)
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
            catch
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Telah terdaftar"
                });
            }
        }
    }
}

