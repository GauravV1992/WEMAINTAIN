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
using static System.Net.WebRequestMethods;

namespace WEMAINTAIN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly ILogger<CategoryController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryController(ILogger<CategoryController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;

        }
        public IActionResult Index()
        {
            return View("Categories");
        }

        [HttpGet]
        public ActionResult Create(CategoryRequest request)
        {
            var categories = new CategoryRequest();
            return PartialView("~/areas/admin/views/category/create.cshtml", categories);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var categories = new ResultDto<CategoryResponse>();
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
            var httpResponseMessage = await httpClient.GetAsync("Category/GetById/" + id + "");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                categories = JsonSerializer.Deserialize<ResultDto<CategoryResponse>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return PartialView("~/areas/admin/views/category/edit.cshtml", categories?.Data);
        }

        [HttpPost]
        public async Task<ActionResult> Save(CategoryRequest request)
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
                var httpResponseMessage = await httpClient.PostAsJsonAsync("Category/Save", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    if (response != null && response.Data > 0 && file != null)
                        Common.UplaodFile(file, "categoryImage", Convert.ToString(response.Data));
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
        public async Task<ActionResult> Update(CategoryRequest request)
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
                var httpResponseMessage = await httpClient.PostAsJsonAsync("Category/Update", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    if (response != null && response.Data > 0 && file != null)
                        Common.UplaodFile(file, "categoryImage", Convert.ToString(response.Data));
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
            var httpResponseMessage = await httpClient.PostAsJsonAsync("Category/Delete", objValue);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return Json(response);
        }


        [HttpPost]
        public async Task<ActionResult> GetAll(CategoryRequest request)
        {
            try
            {
                request.PageIndex = request.Start / request.Length + 1;
                var categories = new ResultDto<IEnumerable<CategoryResponse>>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("Category/GetAll", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    categories = JsonSerializer.Deserialize<ResultDto<IEnumerable<CategoryResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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
        public async Task<ActionResult> GetPackageNames()
        {
            try
            {
                var categories = new ResultDto<IEnumerable<SelectListItem>>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.GetAsync("Category/GetPackageNames");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    categories = JsonSerializer.Deserialize<ResultDto<IEnumerable<SelectListItem>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    //categories = await JsonConvert.DeserializeObject<ResultDto<IEnumerable<CategoryResponse>>>(contentStream);
                }
                return Json(new
                {
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
            var FolderName = @"wwwroot\categoryImage\" + id + ext;
            var path = Path.Combine(Directory.GetCurrentDirectory(), FolderName);
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes.ToArray(), "application/octet-stream");
        }
    }
}