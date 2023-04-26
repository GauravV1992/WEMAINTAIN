using BusinessEntities.Common;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Net.Http.Headers;
using WEMAINTAIN.Models;
using BusinessEntities.RequestDto;
using Microsoft.AspNetCore.Authorization;

namespace WEMAINTAIN.Areas.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var user = new UserResponse();
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
            var httpResponseMessage = await httpClient.GetAsync("User/GetUserDetails");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                user = JsonSerializer.Deserialize<UserResponse>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return View("~/areas/client/views/home.cshtml", user);
        }
       
        public async Task<IActionResult> GetCategorySection()
        {
            var categories = new ResultDto<IEnumerable<CategoryResponse>>();
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            var httpResponseMessage = await httpClient.GetAsync("Category/GetPackageSection");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                categories = JsonSerializer.Deserialize<ResultDto<IEnumerable<CategoryResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return PartialView("~/areas/client/views/_category.cshtml", categories?.Data.ToList());
        }
        public async Task<IActionResult> GetSubPackageSection(int packageId)
        {
            var categories = new ResultDto<IEnumerable<SubPackageResponse>>();
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            var httpResponseMessage = await httpClient.GetAsync("SubPackage/GetSubPackageSection/" + packageId + "");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                categories = JsonSerializer.Deserialize<ResultDto<IEnumerable<SubPackageResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return PartialView("~/areas/client/views/_subpackage.cshtml", categories?.Data.ToList());
        }

    }
}