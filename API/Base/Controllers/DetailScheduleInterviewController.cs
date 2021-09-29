using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repository.Data;

namespace API.Base.Controllers
{
    public class DetailScheduleInterviewController : BaseController<DetailScheduleInterview, DetailScheduleInterviewRepository, int>
    {
        public DetailScheduleInterviewController(DetailScheduleInterviewRepository repository) : base(repository)
        {
        }
    }
}