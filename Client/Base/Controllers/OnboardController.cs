using System;
using System.Collections.Generic;
using System.Linq;
using API.Models;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
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
    }
}