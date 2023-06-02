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

    public class BillingController : Controller
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<BillingController> _logger;
        //private readonly CustomIDataProtection protector;
        private readonly IHttpClientFactory _httpClientFactory;
        public BillingController(/*IHttpContextAccessor httpContextAccessor,*/ILogger<BillingController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            //this._httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int subPackageId = 16, string amcPeriod = "Quarterly", string servicesIds = "20,19")
        {
            try
            {
                CartRequest request = new CartRequest
                {
                    SubPackageId = subPackageId,
                    AMCPeriod = amcPeriod,
                    ServicesIds = servicesIds
                };
                var response = new ResultDto<BillingAndCartDetailsResponse>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("SubPackage/GetBillingAndCartDetails", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonSerializer.Deserialize<ResultDto<BillingAndCartDetailsResponse>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
                PaymentSuccessRequest billingDetails = new PaymentSuccessRequest();
                billingDetails.subPackageId = subPackageId;
                billingDetails.AMCPeriod = amcPeriod;
                billingDetails.ServicesIds = servicesIds;
                ViewData["BillingDetails"] = billingDetails;
                await CreateOrderId(response?.Data.TotalPackageAmount);
                return View("~/areas/client/views/billing.cshtml", response?.Data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<string> CreateOrderId(decimal? amount)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new System.Net.Http.HttpMethod("POST"), "https://api.razorpay.com/v1/orders"))
                {
                    var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes("" + Constants.TestKeyId + ":" + Constants.TestKeySecret + ""));
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");
                    request.Content = new StringContent("{\"amount\": " + amount * 100 + ",\"currency\":\"INR\"}");
                    request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var contentStream = await response.Content.ReadAsStringAsync();
                        var deptList = JsonSerializer.Deserialize<OrderRequest>(contentStream);


                        ViewBag.OrderId = deptList.id;
                        ViewBag.KeyId = Constants.TestKeyId;

                    }
                }
                return "";
            }

        }

        [HttpPost]
        [ActionName("Success")]
        public async Task<IActionResult> Post(PaymentSuccessRequest request)
        {
            RazorpayClient client = new RazorpayClient(Constants.TestKeyId, Constants.TestKeySecret);

            Dictionary<string, string> attributes = new Dictionary<string, string>();

            attributes.Add("razorpay_payment_id", request.razorpay_payment_id);
            attributes.Add("razorpay_order_id", request.sOrderId);
            attributes.Add("razorpay_signature", request.razorpay_signature);
            Utils.verifyPaymentSignature(attributes);
            var response = new ResultDto<long>();
            PurchaseDetailsRequest request1 = new PurchaseDetailsRequest
            {
                PackageAmount = request.PackageAmount,
                SubPackageId = request.subPackageId,
                AMCPeriod = request.AMCPeriod,
                ServicesIds = request.ServicesIds
            };
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            httpClient.DefaultRequestHeaders.Add(
         HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
            var httpResponseMessage = await httpClient.PostAsJsonAsync("PurchaseDetails/Save", request1);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return RedirectToAction("Index", "Home");
        }

    }
}