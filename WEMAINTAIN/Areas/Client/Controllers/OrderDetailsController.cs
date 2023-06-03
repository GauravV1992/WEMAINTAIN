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


namespace WEMAINTAIN.Areas.Client.Controllers
{

    public class OrderDetailsController : Controller
    {
        private readonly ILogger<OrderDetailsController> _logger;
        //private readonly CustomIDataProtection protector;
        private readonly IHttpClientFactory _httpClientFactory;
        public OrderDetailsController(ILogger<OrderDetailsController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int subPackageId, string amcPeriod = "Quarterly")
        {
            try
            {
                var subPackagePriceDetails = new ResultDto<SubPackagePriceDetailsResponse>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                var httpResponseMessage = await httpClient.GetAsync("SubPackage/GetSubPackagePriceDetails/" + subPackageId + "/" + amcPeriod + "");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    subPackagePriceDetails = JsonSerializer.Deserialize<ResultDto<SubPackagePriceDetailsResponse>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
                return View("~/areas/client/views/orderdetails.cshtml", subPackagePriceDetails?.Data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

       

        [HttpGet]

            
        public async Task<IActionResult> Billing(int subPackageId, string amcPeriod, string servicesIds)
        {
            try
            {
                //var subPackagePriceDetails = new ResultDto<SubPackagePriceDetailsResponse>();
                //var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                //var httpResponseMessage = await httpClient.GetAsync("SubPackage/GetSubPackagePriceDetails/" + subPackageId + "/" + amcPeriod + "");
                //if (httpResponseMessage.IsSuccessStatusCode)
                //{
                //    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                //    subPackagePriceDetails = JsonSerializer.Deserialize<ResultDto<SubPackagePriceDetailsResponse>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                //}
                return PartialView("~/areas/client/views/billing.cshtml");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}