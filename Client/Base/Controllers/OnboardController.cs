using System;
using System.Collections.Generic;
using System.Linq;
using API.Models;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Client.Base.Controllers
{
    [Authorize]
    public class OnboardController : BaseController<Onboard, OnboardRepository, int>
    {
        private readonly OnboardRepository repository;
        public OnboardController(OnboardRepository repository) : base(repository)
        {
             this.repository = repository;
        }

        [HttpPost("create")]
        public JsonResult CreateOnBoard(Onboard onboard)
        {
            var result = repository.CreateOnBoard(onboard);
            return Json(result);
        }

        [HttpPost("update")]
        public JsonResult UpdateOnBoard(Onboard onboard)
        {
            var result = repository.UpdateOnBoard(onboard);
            return Json(result);
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
