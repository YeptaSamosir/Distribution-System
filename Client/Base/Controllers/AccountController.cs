using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace Client.Base.Controllers
{
    public class AccountController : BaseController<Account, AccountRepository, int>
    {
        private readonly AccountRepository repository;
        public AccountController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}