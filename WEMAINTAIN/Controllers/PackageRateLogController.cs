using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class PackageRateLogController : Controller
    {
        private readonly ILogger<PackageRateLogController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public PackageRateLogController(ILogger<PackageRateLogController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View("PackageRateLogs");
        }

        [HttpPost]
        public async Task<ActionResult> GetAll(PackageRateLogRequest request)
        {
            try
            {
                //var page = request.Start / request.Length + 1;
                var packagerateLog = new ResultDto<IEnumerable<PackageRateLogResponse>>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.GetAsync("PackageRateLog/GetAll");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    packagerateLog = JsonSerializer.Deserialize<ResultDto<IEnumerable<PackageRateLogResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
                return Json(new
                {
                    recordsTotal = 1,
                    data = packagerateLog.Data.ToList()
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}