using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repository.Data;

namespace API.Base.Controllers
{
    public class OnboardController : BaseController<Onboard, OnboardRepository, int>
    {
        public OnboardController(OnboardRepository repository) : base(repository)
        {
        }
    }
}