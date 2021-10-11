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
    public class ScheduleInterviewController : BaseController<ScheduleInterview, ScheduleInterviewRepository, string>
    {
        private readonly ScheduleInterviewRepository scheduleInterviewRepository;
        public ScheduleInterviewController(ScheduleInterviewRepository scheduleInterviewRepository) : base(scheduleInterviewRepository)
        {
            this.scheduleInterviewRepository = scheduleInterviewRepository;
        }

        [HttpPost("create")]
        public ActionResult CreateInterview(CreateInterviewVM createInterviewVM)
        {
            try
            {
               var response = scheduleInterviewRepository.CreateInterview(createInterviewVM);
                return Ok(new { Message = response });
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = e.Message });
            }
        }

        [HttpPost("create-date-option")]
        public ActionResult CreateDateOption(CreateDateOptionsVM createDateOptionsVM)
        {
            try
            {
                scheduleInterviewRepository.CreateDateOption(createDateOptionsVM);
                return Ok(new { message = "Success Created" });
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = e.Message });
            }
        }

        [HttpPut("response-confirmation-date")]
        public ActionResult ResponseConfirmationDate(InterviewResponseVM interviewResponseVM)
        {
            try
            {
                var checkStatusInterview = scheduleInterviewRepository.GetStatusInterview(interviewResponseVM.ScheduleInterviewId);

                if (checkStatusInterview == "ITV-WC")
                {
                    var updateStatusConfirmationDateAcceptedCandidate = scheduleInterviewRepository.ResponseConfirmationDate(interviewResponseVM);
                    return Ok(updateStatusConfirmationDateAcceptedCandidate);
                }
                return NotFound("404");
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut("confirmation-accepted-candidate")]
        public ActionResult ConfirmationAcceptedCandidate(InterviewResponseVM interviewResponseVM)
        {
            try
            {
                var checkStatusInterview = scheduleInterviewRepository.GetStatusInterview(interviewResponseVM.ScheduleInterviewId);
                if (checkStatusInterview == "ITV-DN" || checkStatusInterview == "ITV-CN") {
                    return Ok("Candidate has been confirmed");
                }
                if (checkStatusInterview == "ITV-OG")
                {
                    var updateStatusConfirmationDateAcceptedCandidate = scheduleInterviewRepository.ConfirmationAcceptedCandidate(interviewResponseVM);
                    return Ok(updateStatusConfirmationDateAcceptedCandidate);
                }
                return NotFound("404");
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}