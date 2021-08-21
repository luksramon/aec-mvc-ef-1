using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using aec_mvc_entity_framework.Models;
using aec_mvc_entity_framework.Services;

namespace aec_mvc_entity_framework.Controllers
{
    [Route("[controller]")]
    public class ApisController : Controller
    {
        private readonly ILogger<ApisController> _logger;

        public ApisController(ILogger<ApisController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Api = await ApiService.GetApi();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
