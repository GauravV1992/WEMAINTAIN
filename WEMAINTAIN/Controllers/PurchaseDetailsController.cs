using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
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
        private readonly ILogger<PurchaseDetailsController> _logger;
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
                httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
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
                request.PageIndex = request.Start / request.Length + 1;
                var purchaseDetails = new ResultDto<PurchaseDetailsWithServicesResponse>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("PurchaseDetails/GetAll", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    purchaseDetails = JsonSerializer.Deserialize<ResultDto<PurchaseDetailsWithServicesResponse>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
                return Json(new
                {
                    recordsFiltered = purchaseDetails.Data.PurchaseDetails == null ? 0 : purchaseDetails.Data.PurchaseDetails.Select(x => x.TotalRecords).FirstOrDefault(),
                    data = purchaseDetails
                }); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public async Task<ActionResult> PurchaseServicesById(long id)
        {
            try
            {
                var purchaseDetails = new ResultDto<IEnumerable<PurchaseServicesResponse>>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.GetAsync("PurchaseDetails/PurchaseServicesById/" + id + "");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    purchaseDetails = JsonSerializer.Deserialize<ResultDto<IEnumerable<PurchaseServicesResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
                return PartialView("~/views/PurchaseDetails/purchaseservicedetails.cshtml", purchaseDetails.Data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}