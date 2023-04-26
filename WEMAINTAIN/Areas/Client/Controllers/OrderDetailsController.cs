using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEMAINTAIN.Models;

namespace WEMAINTAIN.Areas.Client.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly ILogger<OrderDetailsController> _logger;
        private readonly CustomIDataProtection protector;
        public OrderDetailsController(ILogger<OrderDetailsController> logger, CustomIDataProtection customIDataProtection)
        {
            _logger = logger;
            protector = customIDataProtection;
        }
        [HttpGet("{Id}")]
        public IActionResult Index(string Id)
        {
            ViewData["SCId"] = protector.Encode(Id);
            return View("~/areas/client/views/orderdetails.cshtml");
        }

    }
}