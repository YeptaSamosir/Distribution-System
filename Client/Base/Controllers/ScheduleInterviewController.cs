using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace Client.Base.Controllers
{
    public class ScheduleInterviewController : BaseController<ScheduleInterview, ScheduleInterviewRepository, string>
    {
        private readonly ScheduleInterviewRepository repository;
        public ScheduleInterviewController(ScheduleInterviewRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}