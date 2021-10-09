using API.Models;
using API.Models.ViewModels;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Base.Controllers
{
    [AllowAnonymous]
    [Route("interview")]
    public class ConfirmationController : BaseController<ScheduleInterview, ScheduleInterviewRepository, string>
    {
        private readonly ScheduleInterviewRepository repository;
        public ConfirmationController(ScheduleInterviewRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("confirmation/{keyinterviev}/{keydate}")]
        public IActionResult ConfirmationDate(string keyinterviev,int keydate)
        {
            InterviewResponseVM interviewResponseVM = new InterviewResponseVM();
            interviewResponseVM.ScheduleInterviewId = keyinterviev;
            interviewResponseVM.ScheduleDateConfirmId = keydate;
            
            var response = repository.ConfirmationDate(interviewResponseVM);
            if (response == "404") {
                return Redirect("/error/404");
            }

            ViewBag.Response = response;
            return View();
        }
    }
}
