using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using API.Models.ViewModels;
using System.Net;

namespace API.Base.Controllers
{
    public class OnboardController : BaseController<Onboard, OnboardRepository, int>
    {
        private readonly OnboardRepository onboardRepository;
        public OnboardController(OnboardRepository onboardRepository) : base(onboardRepository)
        {
            this.onboardRepository = onboardRepository;
        }

        [HttpPost("create")]
        public ActionResult CreateOnboard(Onboard onboard)
        {
            try
            {
                if (onboardRepository.CreateOnboard(onboard) == 1)
                {
                    return Ok(new { Message = "Success Created" });
                };
                return BadRequest(new { Message = "Create onboard failed" });
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = e.Message });
            }
        }

        [HttpPut("update")]
        public ActionResult UpdateOnboard(Onboard onboard)
        {
            try
            {
                onboardRepository.UpdateOnBoard(onboard);
                return Ok(new { Message = "Success Update" });
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = e.Message });
            }
        }
    }
}