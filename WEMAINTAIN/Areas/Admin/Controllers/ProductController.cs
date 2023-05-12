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
    public class ProductController : Controller
    {

        private readonly ILogger<ProductController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductController(ILogger<ProductController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;

        }
        public IActionResult Index()
        {
            return View("Product");
        }

        [HttpGet]
        public ActionResult Create(ProductRequest request)
        {
            var categories = new ProductRequest();
            return PartialView("~/areas/admin/views/Product/create.cshtml", categories);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var categories = new ResultDto<ProductResponse>();
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
            var httpResponseMessage = await httpClient.GetAsync("Product/GetById/" + id + "");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                categories = JsonSerializer.Deserialize<ResultDto<ProductResponse>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return PartialView("~/areas/admin/views/Product/edit.cshtml", categories?.Data);
        }

        [HttpPost]
        public async Task<ActionResult> Save(ProductRequest request)
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
                var httpResponseMessage = await httpClient.PostAsJsonAsync("Product/Save", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    if (response != null && response.Data > 0 && file != null)
                        Common.UplaodFile(file, "ProductImage", Convert.ToString(response.Data));
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
        public async Task<ActionResult> Update(ProductRequest request)
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
                var httpResponseMessage = await httpClient.PostAsJsonAsync("Product/Update", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    if (response != null && response.Data > 0 && file != null)
                        Common.UplaodFile(file, "ProductImage", Convert.ToString(response.Data));
                }
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
            var httpResponseMessage = await httpClient.PostAsJsonAsync("Product/Delete", objValue);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                if (response?.Data > 0)
                {
                    var FolderName = @"wwwroot\ProductImage\" + id + ext;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), FolderName);
                    System.IO.File.Delete(path);
                }

            }
            return Json(response);
        }


        [HttpPost]
        public async Task<ActionResult> GetAll(ProductRequest request)
        {
            try
            {
                request.PageIndex = request.Start / request.Length + 1;
                var categories = new ResultDto<IEnumerable<ProductResponse>>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("Product/GetAll", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    categories = JsonSerializer.Deserialize<ResultDto<IEnumerable<ProductResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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
        public async Task<ActionResult> GetVendorNames()
        {
            try
            {
                var vendors = new ResultDto<IEnumerable<SelectListItem>>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.GetAsync("Product/GetVendorName");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    vendors = JsonSerializer.Deserialize<ResultDto<IEnumerable<SelectListItem>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
                return Json(new
                {
                    data = vendors?.Data.ToList()
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
            var FolderName = @"wwwroot\productimage\" + id + ext;
            var path = Path.Combine(Directory.GetCurrentDirectory(), FolderName);
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes.ToArray(), "application/octet-stream");
        }
    }
}