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
        public ActionResult CreateScheduleInterview(SceduleInterviewVM sceduleInterviewVM)
        {
            try
            {
                scheduleInterviewRepository.CreateScheduleInterview(sceduleInterviewVM);
                return Ok(new { Message = "Success Create Schedule" });
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = e.Message });
            }
        }

        [HttpPut("confirmation-date")]
        public ActionResult Update(InterviewResponseVM interviewResponseVM)
        {
            try
            {
                var checkStatusInterview = scheduleInterviewRepository.GetStatusInterview(interviewResponseVM.ScheduleInterviewId);
                if (checkStatusInterview != "ITV-WD") {
                    return NotFound("404");
                }
                var updateStatusScheduleConfrimDate = scheduleInterviewRepository.ConfirmDateScheduleInterview(interviewResponseVM);

                return Ok(updateStatusScheduleConfrimDate);
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}