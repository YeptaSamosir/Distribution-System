using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using API.Models;
using Client.Repository.Data;

namespace Client.Base.Controllers
{
    public class OnboardController : BaseController<Onboard, OnboardRepository, int>
    {
        private readonly OnboardRepository repository;
        public OnboardController(OnboardRepository repository) : base(repository)
        {
             this.repository = repository;
        }
    }
}