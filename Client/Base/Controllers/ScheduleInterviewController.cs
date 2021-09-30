using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using API.Models;
using Client.Repository.Data;

namespace Client.Base.Controllers
{
    [Route("[controller]")]
    public class ScheduleInterviewController : BaseController<ScheduleInterview, ScheduleInterviewRepository, string>
    {
        private readonly ScheduleInterviewRepository repository;
        public ScheduleInterviewController(ScheduleInterviewRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}