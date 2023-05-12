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
    public class CountryController : Controller
    {

        private readonly ILogger<CountryController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public CountryController(ILogger<CountryController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult> GetCountryNames()
        {
            try
            {
                var categories = new ResultDto<IEnumerable<SelectListItem>>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.GetAsync("Country/GetPackageNames");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    categories = JsonSerializer.Deserialize<ResultDto<IEnumerable<SelectListItem>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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
         
    }
}