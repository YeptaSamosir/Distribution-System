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
            if (scheduleFollowBy == "candidate")
            {
                ViewBag.Name = dataScheduleInterview.Candidate.Name;
                ViewBag.Followed = "customer";
            }
            else {
                ViewBag.Name = dataScheduleInterview.CustomerName;
                ViewBag.Followed = "candidate";
            }
           
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

        [HttpGet("feedback/{scheduleInterviewId}")]
        public IActionResult FeedbackView(string scheduleInterviewId)
        {
            if (scheduleInterviewId == null)
            {
                return Redirect("/error/404");
            }

            var dataScheduleInterview = repository.Get(scheduleInterviewId).Result;
            if (dataScheduleInterview.StatusId == "ITV-CN" || dataScheduleInterview.StatusId == "ITV-AC" || dataScheduleInterview.StatusId == "ITV-DN")
            {
                ViewBag.ScheduleInterviewId = scheduleInterviewId;
                return View();
            }

            return Redirect("/error/404");
        }

        [HttpPost("feedback")]
        public IActionResult Feedback(FeedbackVM feedbackVM)
        {
            var response = repository.Feedback(feedbackVM);

            return Json(response);
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
