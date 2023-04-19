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
    public class PackageRateController : Controller
    {
        private readonly ILogger<PackageRateController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public PackageRateController(ILogger<PackageRateController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            PackageRateRequest obj = new PackageRateRequest();
            return View("PackageRates", obj);
        }

        [HttpGet]
        public ActionResult Create(PackageRateRequest request)
        {
            var PackageRates = new PackageRateRequest();
            return PartialView("~/views/PackageRate/create.cshtml", PackageRates);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var PackageRates = new ResultDto<PackageRateResponse>();
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
            var httpResponseMessage = await httpClient.GetAsync("PackageRate/GetById/" + id + "");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                PackageRates = JsonSerializer.Deserialize<ResultDto<PackageRateResponse>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return PartialView("~/views/PackageRate/edit.cshtml", PackageRates.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(PackageRateRequest request)
        {
            var response = new ResultDto<long>();
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("PackageRate/Save", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(PackageRateRequest request)
        {
            var response = new ResultDto<long>();
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("PackageRate/Update", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
            return Json(response);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var response = new ResultDto<long>();
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
            ValueRequest objValue = new ValueRequest();
            objValue.Id = id;
            var httpResponseMessage = await httpClient.PostAsJsonAsync("PackageRate/Delete", objValue);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return Json(response);
        }


        [HttpPost]
        public async Task<ActionResult> GetAll(PackageRateRequest request)
        {
            try
            {
                request.PageIndex = request.Start / request.Length + 1;
                var PackageRates = new ResultDto<IEnumerable<PackageRateResponse>>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("PackageRate/GetAll", request);
                //var httpResponseMessage = await httpClient.GetAsync("PackageRate/GetAll");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    PackageRates = JsonSerializer.Deserialize<ResultDto<IEnumerable<PackageRateResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    //PackageRates = await JsonConvert.DeserializeObject<ResultDto<IEnumerable<PackageRateResponse>>>(contentStream);
                }
                return Json(new
                {
                    recordsFiltered = PackageRates.Data == null ? 0 : PackageRates.Data.Select(x => x.TotalRecords).FirstOrDefault(),
                    data = PackageRates.Data.ToList()
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}