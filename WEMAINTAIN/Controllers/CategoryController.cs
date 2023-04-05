using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEMAINTAIN.Models;

namespace WEMAINTAIN.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("Login");
        }
    }
}