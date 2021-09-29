using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repository.Data;

namespace API.Base.Controllers
{
    public class ScheduleInterviewController : BaseController<ScheduleInterview, ScheduleInterviewRepository, string>
    {
        public ScheduleInterviewController(ScheduleInterviewRepository repository) : base(repository)
        {
        }
    }
}