using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using WEMAINTAIN.Models;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace WEMAINTAIN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {

        private readonly ILogger<BannerController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public BannerController(ILogger<BannerController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;

        }
        public IActionResult Index()
        {
            return View("Banners");
        }

        [HttpGet]
        public ActionResult Create(BannerRequest request)
        {
            var categories = new BannerRequest();
            return PartialView("~/areas/admin/views/Banner/create.cshtml", categories);
        }
       

        [HttpPost]
        public async Task<ActionResult> Save(BannerRequest request)
        {
            var response = new ResultDto<long>();
            if (ModelState.IsValid)
            {
                var file = Request.Form.Files != null && Request.Form.Files.Any() ? Request.Form.Files[0] : null;
                if (file != null)
                    request.Ext = Common.GetExtention(file);
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("Banner/Save", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    if (response != null && response.Data > 0 && file != null)
                        Common.UplaodFile(file, "BannerImage", Convert.ToString(response.Data));
                }
            }
            else
            {
                var errros = Common.GetErrorListFromModelState(ModelState).FirstOrDefault();
                return Json(errros);
            }
            return Json(response);
        }

       

        public async Task<ActionResult> Delete(int id, string ext)
        {
            var response = new ResultDto<long>();
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
            ValueRequest objValue = new ValueRequest();
            objValue.Id = id;
            var httpResponseMessage = await httpClient.PostAsJsonAsync("Banner/Delete", objValue);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                if (response?.Data > 0)
                {
                    var FolderName = @"wwwroot\BannerImage\" + id + ext;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), FolderName);
                    System.IO.File.Delete(path);
                }

            }
            return Json(response);
        }


        [HttpPost]
        public async Task<ActionResult> GetAll(BannerRequest request)
        {
            try
            {
                request.PageIndex = request.Start / request.Length + 1;
                var categories = new ResultDto<IEnumerable<BannerResponse>>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("Banner/GetAll", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    categories = JsonSerializer.Deserialize<ResultDto<IEnumerable<BannerResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
                return Json(new
                {
                    recordsFiltered = categories?.Data == null ? 0 : categories?.Data.Select(x => x.TotalRecords).FirstOrDefault(),
                    data = categories?.Data.ToList()
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
            var FolderName = @"wwwroot\BannerImage\" + id + ext;
            var path = Path.Combine(Directory.GetCurrentDirectory(), FolderName);
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes.ToArray(), "application/octet-stream");
        }
    }
}