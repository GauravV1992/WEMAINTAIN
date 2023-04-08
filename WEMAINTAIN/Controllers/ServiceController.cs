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
    public class ServiceController : Controller
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public ServiceController(ILogger<ServiceController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View("Service");
        }

        [HttpGet]
        public ActionResult Create(ServiceRequest request)
        {
            var Service = new ServiceRequest();
            return PartialView("~/views/Service/create.cshtml", Service);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var Service = new ResultDto<ServiceResponse>();
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            var httpResponseMessage = await httpClient.GetAsync("Service/GetById/" + id + "");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                Service = JsonSerializer.Deserialize<ResultDto<ServiceResponse>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return PartialView("~/views/Service/edit.cshtml", Service.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(ServiceRequest request)
        {
            var response = new ResultDto<long>();
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("Service/Save", request);
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
        public async Task<ActionResult> Update(ServiceRequest request)
        {
            var response = new ResultDto<long>();
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("Service/Update", request);
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
            ValueRequest objValue = new ValueRequest();
            objValue.Id = id;
            var httpResponseMessage = await httpClient.PostAsJsonAsync("Service/Delete", objValue);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return Json(response);
        }


        [HttpPost]
        public async Task<ActionResult> GetAll(ServiceRequest request)
        {
            try
            {
                //var page = request.Start / request.Length + 1;
                var Service = new ResultDto<IEnumerable<ServiceResponse>>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                var httpResponseMessage = await httpClient.GetAsync("Service/GetAll");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    Service = JsonSerializer.Deserialize<ResultDto<IEnumerable<ServiceResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    //Service = await JsonConvert.DeserializeObject<ResultDto<IEnumerable<ServiceResponse>>>(contentStream);
                }
                return Json(new
                {
                    recordsTotal = 1,
                    data = Service.Data.ToList()
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}