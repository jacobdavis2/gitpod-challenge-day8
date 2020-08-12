using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Challenge;
using Challenge.Models;

namespace Challenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ChallengeContext _context;

        public HomeController(ILogger<HomeController> logger, ChallengeContext context)
        {
            _logger = logger;
            _context = context;

            // Note that you may have to do this for every controller, if you
            // do make more controllers! This is due to the in-memory database
            context.InitDatabaseAsync();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Transactions()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
