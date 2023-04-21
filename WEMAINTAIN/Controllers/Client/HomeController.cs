using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEMAINTAIN.Models;

namespace WEMAINTAIN.User.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("~/views/client/home.cshtml");
        }
        public IActionResult Index2()
        {
            return View("~/views/client/home.cshtml");
        }

    }
}