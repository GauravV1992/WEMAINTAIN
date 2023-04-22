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
    public class RequestLabourController : Controller
    {
        private readonly ILogger<RequestLabourController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public RequestLabourController(ILogger<RequestLabourController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            RequestLabourRequest obj = new RequestLabourRequest();
            return View("RequestLabour", obj);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var requestlabours= new ResultDto<RequestLabourResponse>();
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
            var httpResponseMessage = await httpClient.GetAsync("RequestLabour/GetById/" + id + "");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                requestlabours = JsonSerializer.Deserialize<ResultDto<RequestLabourResponse>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return PartialView("~/views/RequestLabour/edit.cshtml", requestlabours.Data);
        }

        [HttpPost]
        public async Task<ActionResult> GetAll(RequestLabourRequest request)
        {
            try
            {
                request.PageIndex = request.Start / request.Length + 1;
                var requestlabours = new ResultDto<IEnumerable<RequestLabourResponse>>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("RequestLabour/GetAll", request);
                //var httpResponseMessage = await httpClient.GetAsync("RequestLabour/GetAll");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    requestlabours = JsonSerializer.Deserialize<ResultDto<IEnumerable<RequestLabourResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
                return Json(new
                {
                    recordsFiltered = requestlabours.Data == null ? 0 : requestlabours.Data.Select(x => x.TotalRecords).FirstOrDefault(),
                    data = requestlabours.Data.ToList()
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}