using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        [Route("500")]
        public IActionResult AppError()
        {
            return View();
        }

        [Route("401")]
        public IActionResult Unauthorized()
        {
            return View();
        }

        [Route("403")]
        public IActionResult Forbidden()
        {
            return View();
        }

        [Route("404")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
