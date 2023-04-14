using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using WEMAINTAIN.Models;



namespace WEMAINTAIN.Controllers
{
    public class PurchaseDetailsController : Controller
    {
        private readonly ILogger<PackageRateController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public PurchaseDetailsController(ILogger<PurchaseDetailsController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View("PurchaseDetails");
        }

        [HttpGet]
        public ActionResult Create(PurchaseDetailsRequest request)
        {
            var PurchaseDetails = new PurchaseDetailsRequest();
            return PartialView("~/views/PurchaseDetails/create.cshtml", PurchaseDetails);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(PurchaseDetailsRequest request)
        {
            var response = new ResultDto<long>();
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("PurchaseDetails/Save", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
            return Json(response);
        }

        [HttpPost]
        public async Task<ActionResult> GetAll(PurchaseDetailsRequest request)
        {
            try
            {
                //var page = request.Start / request.Length + 1;
                var purchaseDetails = new ResultDto<IEnumerable<PurchaseDetailsResponse>>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                var httpResponseMessage = await httpClient.GetAsync("PackageRate/GetAll");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    purchaseDetails = JsonSerializer.Deserialize<ResultDto<IEnumerable<PurchaseDetailsResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
               return Json(new
                {
                    recordsTotal = 1,
                    data = purchaseDetails.Data.ToList()
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}