﻿using BusinessEntities.Common;
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
namespace WEMAINTAIN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public UserController(ILogger<UserController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View("~/areas/admin/views/User/Users.cshtml");
        }

        [HttpGet]
        public ActionResult Create(UserRequest request)
        {
            var Users = new UserRequest();
            return PartialView("~/areas/admin/views/User/create.cshtml", Users);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var Users = new ResultDto<UserResponse>();
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            httpClient.DefaultRequestHeaders.Add(
              HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
            var httpResponseMessage = await httpClient.GetAsync("User/GetById/" + id + "");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                Users = JsonSerializer.Deserialize<ResultDto<UserResponse>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return PartialView("~/areas/admin/views/User/edit.cshtml", Users?.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(UserRequest request)
        {
            var response = new ResultDto<long>();
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
              HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("User/Save", request);
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
        public async Task<ActionResult> Update(UserRequest request)
        {
            var response = new ResultDto<long>();
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
              HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("User/Update", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
            return Json(response);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var response = new ResultDto<long>();
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            httpClient.DefaultRequestHeaders.Add(
             HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
            ValueRequest objValue = new ValueRequest();
            objValue.Id = id;
            var httpResponseMessage = await httpClient.PostAsJsonAsync("User/Delete", objValue);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return Json(response);
        }
        //[HttpPost]
        //public async Task<ActionResult> GetAll(UserRequest request)
        //{
        //    try
        //    {
        //        //var page = request.Start / request.Length + 1;
        //        var Users = new ResultDto<IEnumerable<UserResponse>>();
        //        var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
        //        httpClient.DefaultRequestHeaders.Add(
        //          HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
        //        var httpResponseMessage = await httpClient.GetAsync("User/GetAll");
        //        if (httpResponseMessage.IsSuccessStatusCode)
        //        {
        //            var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
        //            Users = JsonSerializer.Deserialize<ResultDto<IEnumerable<UserResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        //            //Users = await JsonConvert.DeserializeObject<ResultDto<IEnumerable<UserResponse>>>(contentStream);
        //        }
        //        return Json(new
        //        {
        //            recordsTotal = 1,
        //            data = Users.Data.ToList()
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult> GetAll(UserRequest request)
        {
            try
            {
                request.PageIndex = request.Start / request.Length + 1;
                var user = new ResultDto<IEnumerable<UserResponse>>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("User/GetAll", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    user = JsonSerializer.Deserialize<ResultDto<IEnumerable<UserResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                }
                return Json(new
                {
                    recordsFiltered = user?.Data == null ? 0 : user.Data.Select(x => x.TotalRecords).FirstOrDefault(),
                    data = user?.Data.ToList()
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}