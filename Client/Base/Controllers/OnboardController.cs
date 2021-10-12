using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace Client.Base.Controllers
{
    public class OnboardController : BaseController<Onboard, OnboardRepository, int>
    {
        private readonly OnboardRepository repository;
        public OnboardController(OnboardRepository repository) : base(repository)
        {
             this.repository = repository;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("detail/{id}")]
        public IActionResult DetailOnboard(string id)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}
