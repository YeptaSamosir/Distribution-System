using API.Context;
using API.Models.ViewModels;
using API.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Base.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly MyContext myContext;
        private readonly DashboardRepository dashboardRepository;
        public IConfiguration configuration;
        public DashboardController(IConfiguration config, DashboardRepository dashboardRepository)
        {
            this.dashboardRepository = dashboardRepository;
            this.configuration = config;
            this.myContext = myContext;
        }

        [HttpGet("data-count")]
        public ActionResult GetDataCount()
        {
            var dashboardData = dashboardRepository.GetDataCount();

            return Ok(dashboardData);
        }
        
        [HttpGet("data-for-calender")]
        public ActionResult FullCalender()
        {
            var Entity = dashboardRepository.FullCalender();
            return Ok(Entity);
        }
    }
}
