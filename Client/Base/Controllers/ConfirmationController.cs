using API.Helper;
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

        [HttpGet("confirmation/{scheduleFollowBy}")]
        public IActionResult ConfirmationDateView(string s, string scheduleFollowBy)
        {
            if (s == null)
            {
                return Redirect("/error/404");
            }

            string result = s.Replace(" ", "+");

            RsaHelper rsaHelper = new RsaHelper();
            var decrypt = rsaHelper.Decrypt(result);

            var dataScheduleInterview = repository.Get(decrypt).Result;
          
            if (dataScheduleInterview.StatusId != "ITV-WD" || dataScheduleInterview == null) {
                return Redirect("/error/404");
            }

            
            ViewBag.ScheduleInterviewId = dataScheduleInterview.ScheduleInterviewId;
            ViewBag.ScheduleFollowBy = scheduleFollowBy;
            return View();
        }

        [HttpPost("confirmation/create-date-option")]
        public JsonResult CreateDateOption(CreateDateOptionsVM confirmDateOptionsVM)
        {
            var response = repository.CreateDateOption(confirmDateOptionsVM);

           return Json(response);
        }

        [HttpGet("confirmation/{scheduleInterviewId}/{scheduleDateConfirmId}")]
        public IActionResult ResponseConfirmationDate(string scheduleInterviewId,int scheduleDateConfirmId)
        {
            if (scheduleDateConfirmId < 0) {
                return Redirect("/error/404");
            }
            var dataScheduleInterview = repository.Get(scheduleInterviewId).Result;

            if (dataScheduleInterview.StatusId != "ITV-WC" || dataScheduleInterview == null)
            {
                return Redirect("/error/404");
            }

            InterviewResponseVM interviewResponseVM = new InterviewResponseVM();
            interviewResponseVM.ScheduleDateConfirmId = scheduleDateConfirmId;
            interviewResponseVM.ScheduleInterviewId = scheduleInterviewId;

            var response = repository.ResponseConfirmationDate(interviewResponseVM);
         
            ViewBag.Response = response;
            return View();
        }

        [HttpGet("{keyinterviev}/{isaccepted}")]
        public IActionResult ConfirmationDate(string keyinterviev, string isaccepted, string e)
        {
            if (e == null)
            {
                return Redirect("/error/404");
            }

            string result = e.Replace(" ", "+");

            RsaHelper rsaHelper = new RsaHelper();
            var decrypt = rsaHelper.Decrypt(result);

            InterviewResponseVM interviewResponseVM = new InterviewResponseVM();
            interviewResponseVM.ScheduleInterviewId = keyinterviev;
            interviewResponseVM.CandidateAccepted = isaccepted;
            interviewResponseVM.EmailCustomer = decrypt;

            var response = repository.ConfirmationAcceptedCandidate(interviewResponseVM);

            if (response == "404")
            {
                return Redirect("/error/404");
            }

            if (response == "401")
            {
                return Redirect("/error/401");
            }

            ViewBag.Response = response;
            return View();
        }
    }
}
