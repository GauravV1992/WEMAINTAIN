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
namespace WEMAINTAIN.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            return View("~/areas/admin/views/PackageRateLog/PackageRateLogs.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult> GetAll(PackageRateLogRequest request)
        {
            try
            {
                request.PageIndex = request.Start / request.Length + 1;
                var packagerateLog = new ResultDto<IEnumerable<PackageRateLogResponse>>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("PackageRateLog/GetAll", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    packagerateLog = JsonSerializer.Deserialize<ResultDto<IEnumerable<PackageRateLogResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
                return Json(new
                {
                    recordsFiltered = packagerateLog?.Data == null ? 0 : packagerateLog.Data.Select(x => x.TotalRecords).FirstOrDefault(),
                    data = packagerateLog?.Data.ToList()
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}