using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEMAINTAIN.Models;

namespace WEMAINTAIN.Areas.Client.Controllers
{
    [Area("Client")]
    public class OrderDetailsController : Controller
    {
        private readonly ILogger<OrderDetailsController> _logger;

        public OrderDetailsController(ILogger<OrderDetailsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("~/views/client/orderdetails/orderdetails.cshtml");
        }

    }
}