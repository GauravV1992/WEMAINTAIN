using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Hosting;
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
using static System.Net.Mime.MediaTypeNames;

namespace WEMAINTAIN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubPackageController : Controller
    {
        private readonly ILogger<SubPackageController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public SubPackageController(ILogger<SubPackageController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View("~/areas/admin/views/SubPackage/SubPackage.cshtml");
        }

        [HttpGet]
        public ActionResult Create(SubPackageRequest request)
        {
            var categories = new SubPackageRequest();
            return PartialView("~/areas/admin/views/SubPackage/create.cshtml", categories);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var categories = new ResultDto<SubPackageResponse>();
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
            var httpResponseMessage = await httpClient.GetAsync("SubPackage/GetById/" + id + "");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                categories = JsonSerializer.Deserialize<ResultDto<SubPackageResponse>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return PartialView("~/areas/admin/views/SubPackage/edit.cshtml", categories?.Data);
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(SubPackageRequest request)
        {
            var file = Request.Form.Files != null && Request.Form.Files.Any() ? Request.Form.Files[0] : null;
            if (file != null)
                request.Ext = Common.GetExtention(file);
            var response = new ResultDto<long>();
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("SubPackage/Save", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    if (response != null && response.Data > 0 && file != null)
                        Common.UplaodFile(file, "subpackageImage", Convert.ToString(response.Data));
                }
            }
            else
            {
                var errros = Common.GetErrorListFromModelState(ModelState).FirstOrDefault();
                return Json(errros);
            }
            return Json(response);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(SubPackageRequest request)
        {
            var file = Request.Form.Files != null && Request.Form.Files.Any() ? Request.Form.Files[0] : null;
            if (file != null)
                request.Ext = Common.GetExtention(file);
            var response = new ResultDto<long>();
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("SubPackage/Update", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    if (response != null && response.Data > 0 && file != null)
                        Common.UplaodFile(file, "subpackageImage", Convert.ToString(response.Data));
                }
            }
            else
            {
                var errros = Common.GetErrorListFromModelState(ModelState).FirstOrDefault();
                return Json(errros);
            }
            return Json(response);
        }

        public async Task<ActionResult> Delete(int id,string ext)
        {
            var response = new ResultDto<long>();
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
            ValueRequest objValue = new ValueRequest();
            objValue.Id = id;
            var httpResponseMessage = await httpClient.PostAsJsonAsync("SubPackage/Delete", objValue);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                if (response?.Data > 0)
                {
                    var FolderName = @"wwwroot\subpackageImage\" + id + ext;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), FolderName);
                   // string ExitingFile = Path.Combine(webHostEnvironment.WebRootPath, "images", UpdatedCost.InvoiceImagePath);
                    System.IO.File.Delete(path);
                }
                 

            }
            return Json(response);
        }


        [HttpPost]
        public async Task<ActionResult> GetAll(SubPackageRequest request)
        {
            try
            {
                request.PageIndex = request.Start / request.Length + 1;
                var categories = new ResultDto<IEnumerable<SubPackageResponse>>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("SubPackage/GetAll", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    categories = JsonSerializer.Deserialize<ResultDto<IEnumerable<SubPackageResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    //categories = await JsonConvert.DeserializeObject<ResultDto<IEnumerable<SubPackageResponse>>>(contentStream);
                }
                return Json(new
                {
                    recordsFiltered = categories?.Data == null ? 0 : categories.Data.Select(x => x.TotalRecords).FirstOrDefault(),
                    data = categories?.Data.ToList()
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public async Task<ActionResult> GetSubPackageNames(int packageId)
        {
            try
            {
                var subPackages = new ResultDto<IEnumerable<SelectListItem>>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.GetAsync("SubPackage/GetSubPackageNames/" + packageId + "");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    subPackages = JsonSerializer.Deserialize<ResultDto<IEnumerable<SelectListItem>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
                return Json(new
                {
                    data = subPackages?.Data.ToList()
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetSubPackagePriceDetails(int packageId, string amcPeriod)
        {
            try
            {
                var subPackagePriceDetails = new ResultDto<SubPackagePriceDetailsResponse>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                var httpResponseMessage = await httpClient.GetAsync("SubPackage/GetSubPackagePriceDetails/" + packageId + "?amcPeriod=" + amcPeriod + "");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    subPackagePriceDetails = JsonSerializer.Deserialize<ResultDto<SubPackagePriceDetailsResponse>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
                return Json(new
                {
                    data = subPackagePriceDetails?.Data
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public FileResult Download(int id, string ext)
        {
            var FolderName = @"wwwroot\subpackageImage\" + id + ext;
            var path = Path.Combine(Directory.GetCurrentDirectory(), FolderName);
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes.ToArray(), "application/octet-stream");
        }


    }
}