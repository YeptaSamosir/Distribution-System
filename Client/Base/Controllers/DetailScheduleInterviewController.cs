using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using API.Models;
using Client.Repository.Data;

namespace Client.Base.Controllers
{
    public class DetailScheduleInterviewController : BaseController<DetailScheduleInterview, DetailScheduleInterviewRepository, int>
    {
        private readonly DetailScheduleInterviewRepository repository;
        public DetailScheduleInterviewController(DetailScheduleInterviewRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}