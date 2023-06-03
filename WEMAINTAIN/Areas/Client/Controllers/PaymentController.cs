using BusinessEntities.Common;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Net.Http.Headers;
using WEMAINTAIN.Models;
using BusinessEntities.RequestDto;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Razorpay.Api;

namespace WEMAINTAIN.Areas.Client.Controllers
{

    public class PaymentController : Controller
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<PaymentController> _logger;
        //private readonly CustomIDataProtection protector;
        private readonly IHttpClientFactory _httpClientFactory;
        public PaymentController(/*IHttpContextAccessor httpContextAccessor,*/ILogger<PaymentController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            //this._httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/areas/client/views/Payment.cshtml");
        }
    }
}