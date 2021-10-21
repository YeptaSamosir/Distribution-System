using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Client.Repository.Data;

namespace Client.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly DashboardRepository dashboardRepository;
        private readonly ILogger<AdminController> _logger;
        public AdminController(ILogger<AdminController> logger, DashboardRepository dashboardRepository)
        {
            this.dashboardRepository = dashboardRepository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("login", "Auth");
            }
           
            ViewBag.Token = HttpContext.Session.GetString("JWToken");
            ViewBag.EmailLogin = HttpContext.Session.GetString("LogEmail");
            ViewBag.Name = HttpContext.Session.GetString("LogName");
            return View();
        }

        [HttpGet("dashboard/data-count")]
        public async Task<JsonResult> GetData()
        {
            //getdatacount
            var data = await dashboardRepository.GetDataCount();

            return Json(data);
        }

        [HttpGet("dashboard/data-for-calender")]
        public async Task<JsonResult> GetDataForCalender()
        {
            //getdatacount
            var data = await dashboardRepository.GetDataForCalender();

            return Json(data);
        }

        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}